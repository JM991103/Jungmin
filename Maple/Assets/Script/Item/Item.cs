using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 1개를 표현할 클래스
/// </summary>
public class Item : MonoBehaviour
{
    // 아이템데이터 클래스를 가져온다.
    public ItemData data;

    private void Start()
    {
        // 아이템 데이터에 있는 프리펩을 소환한다.
        Instantiate(data.modelPrefab, transform.position, transform.rotation, transform);
    }
}
