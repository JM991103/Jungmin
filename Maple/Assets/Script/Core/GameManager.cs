using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 아이템 데이터를 관리하는 매니저 
    /// </summary>
    ItemDataManager itemData;

    /// <summary>
    /// 아이템 데이터 매니저(읽기전용) 프로퍼티
    /// </summary>
    public ItemDataManager ItemData => itemData;


    /// <summary>
    /// 게임 매니저가 새로 만들어지거나 씬이 로드 되었을 때 실행될 초기화 함수
    /// </summary>
    protected override void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

    }
}
