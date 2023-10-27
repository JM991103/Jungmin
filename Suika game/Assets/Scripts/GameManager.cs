using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;
    public List<Dongle> donglePool;

    [Range(1, 30)]
    public int poolSize;
    public int poolCursor;

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;
    public enum sfx { LevelUp, Next, Attach, Button, Over };
    int sfxCursor;

    public int score;
    public bool isOver;

    public GameObject startGroup;
    public GameObject endGroup;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI subScoreText;

    public GameObject line;
    public GameObject bottom;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        donglePool = new List<Dongle>();

        for (int index = 0; index < poolSize; index++)
        {
            MakeDongle();
        }

        if (!PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }

        maxScoreText.text = PlayerPrefs.GetInt("MaxScore").ToString();
    }

    public void GameStart()
    {
        // 오브젝트 활성화
        line.SetActive(true);
        bottom.SetActive(true);
        scoreText.gameObject.SetActive(true);
        maxScoreText.gameObject.SetActive(true);
        startGroup.SetActive(false);

        // 사운드 시작
        bgmPlayer.Play();
        sfxPlay(sfx.Button);

        // 게임 시작
        Invoke("NextDongle", 0.5f);
    }

    Dongle MakeDongle()
    {
        GameObject instantDongleObj = Instantiate(donglePrefab, dongleGroup);
        instantDongleObj.name = "Dongle " + donglePool.Count;
        Dongle instantDongle = instantDongleObj.GetComponent<Dongle>();
        instantDongle.manager = this;

        donglePool.Add(instantDongle);

        return instantDongle;
    }

    /// <summary>
    /// 프리펩을 새로 생성하는 함수
    /// </summary>
    /// <returns></returns>
    Dongle GetDongle()
    {
        for (int index = 0; index < donglePool.Count; index++)
        {
            poolCursor = (poolCursor + 1) % donglePool.Count;

            if (!donglePool[poolCursor].gameObject.activeSelf)
            {
                return donglePool[poolCursor];
            }
        }

        return MakeDongle();
    }

    /// <summary>
    /// 다음 게임오브젝트를 생성하는 함수
    /// </summary>
    void NextDongle()
    {
        if (isOver)
        {
            return;
        }

        lastDongle = GetDongle();
        lastDongle.level = Random.Range(0, 5);
        lastDongle.gameObject.SetActive(true);

        sfxPlay(sfx.Next);
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

    public void GameOver()
    {
        if (isOver)
        {
            return;
        }
        isOver = true;

        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        // 1. 장면 안에 활성화 되어있는 모든 동글 가져오기
        Dongle[] dongles = FindObjectsOfType<Dongle>();

        // 2. 지우기 전에 모든 동글의 물리효과를 비활성화
        for (int index = 0; index < dongles.Length; index++)
        {
            dongles[index].rigid.simulated = false;
        }

        // 3. 1번의 목록을 하나씩 접근해서 지우기
        for (int index = 0; index < dongles.Length; index++)
        {
            dongles[index].Hide(Vector3.up * 100);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.0f);

        // 최고 점수 갱신
        int maxScore = Mathf.Max(score, PlayerPrefs.GetInt("MaxScore"));
        PlayerPrefs.SetInt("MaxScore", maxScore);

        // 게임 오버 표시
        subScoreText.text = "score : " + scoreText.text;
        endGroup.SetActive(true);

        bgmPlayer.Stop();
        sfxPlay(sfx.Over);
    }

    public void Reset()
    {
        sfxPlay(sfx.Button);
        StartCoroutine("ResetCoroutine");
    }

    IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }

    public void sfxPlay(sfx type)
    {
        switch (type)
        {
            case sfx.LevelUp:
                sfxPlayer[sfxCursor].clip = sfxClip[Random.Range(0, 3)];
                break;
            case sfx.Next:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case sfx.Attach:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                break;
            case sfx.Button:
                sfxPlayer[sfxCursor].clip = sfxClip[5];
                break;
            case sfx.Over:
                sfxPlayer[sfxCursor].clip = sfxClip[6];
                break;            
        }

        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        scoreText.text = score.ToString();
    }
}
