using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;

    [Tooltip("현재 몇 번째 대사인지")]
    public int number;

    [Tooltip("대사 내용")]
    public string[] context;
}

//[System.Serializable]
//public class DialogueEvent
//{

//}