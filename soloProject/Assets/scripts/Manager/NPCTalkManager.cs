using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkManager : MonoBehaviour
{
    ///// <summary>
    ///// Dictionary는 Key를 통해 데이터에 접근할 수 있도록 하는 자료구조.
    ///// </summary>
    //Dictionary<int, string[]> talkData;

    //void Awake()
    //{
    //    talkData = new Dictionary<int, string[]>();
    //    GenerateData();
    //}

    //void GenerateData()
    //{
    //    // .Add로 데이터를 추가 할 수 있다.
    //    talkData.Add(999, new string[] { "안녕?", "이 곳에 처음 왔다고?", "앞으로 쭉 가봐 마을이 있을거야" });
    //    talkData.Add(1000, new string[] { "이 마을에는 슬픈 전설이 있어...", "아마도?" });
    //    talkData.Add(1100, new string[] { "우와", "이 석상은 뭘까??" });
    //    talkData.Add(1200, new string[] { "배고파..." });
    //    talkData.Add(1300, new string[] { "저 석상은 아주 오래전부터 이 마을에 있었대", "누가 만들었는지는 몰라" });
    //    talkData.Add(5000, new string[] { "여기는 들어갈 수 없어" });
    //    //talkData.Add(100, new string[] { "아무것도 없다." });
    //}

    /////// <summary>
    /////// 다음 대사를 출력하도록 talkIndex와 talkData[id].Length를 비교하는 함수 
    /////// </summary>
    /////// <param name="id">오브젝트와 NPC의 ID</param>
    /////// <param name="talkIndex">대사의 문단 갯수</param>
    /////// <returns></returns>
    //public string GetTalk(int id, int talkIndex)
    //{
    //    if (talkIndex == talkData[id].Length)   // talkIndex가 talkData[id]의 길이와 같아지면
    //    {
    //        return null;                        // 대사 종료
    //    }
    //    else
    //    {
    //        //talkIndex가 talkData[id]의 길이보다 작으면
    //        return talkData[id][talkIndex];     // 다음 대사 실행
    //    }
    //}
}
