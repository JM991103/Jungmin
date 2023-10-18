using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        NextDongle();
    }

    /// <summary>
    /// 프리펩을 새로 생성하는 함수
    /// </summary>
    /// <returns></returns>
    Dongle GetDongle()
    {
        GameObject instant = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongle = instant.GetComponent<Dongle>();
        return instantDongle;
    }

    /// <summary>
    /// 다음 게임오브젝트를 생성하는 함수
    /// </summary>
    void NextDongle()
    {
        Dongle newDongle = GetDongle();
        lastDongle = newDongle;
        lastDongle.level = Random.Range(0, 5);
        lastDongle.gameObject.SetActive(true);

        StartCoroutine("WaitNext");
    }

    /// <summary>
    /// NextDongle함수 후 n초간 기다렸다가 NextDongle함수를 실행하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitNext()
    {
        while (lastDongle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        NextDongle();
    }

    /// <summary>
    /// Dongle에서 클릭하는 함수를 GameManager에서 관리함
    /// </summary>
    public void TouchDown()
    {
        if (lastDongle == null) // lastDongle이 없을때
        { 
            return;
        }
        lastDongle.Drag();
    }

    /// <summary>
    /// Dongle에서 드랍하는 함수를 GameManager에서 관리함
    /// </summary>
    public void TouchUp()
    {
        if (lastDongle == null) // lastDongle이 없을때
        {
            return;
        }
        lastDongle.Drop();
        lastDongle = null;
    }
}
