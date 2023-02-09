using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog 
{
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // 이벤트 이름을 정해줄 수 있다 (확인하고 관리하기 편하다)

    public Vector2 line;    // x ~ y까지 대화를 추출할 수 있다.

    public Dialog[] dialogs;
}
