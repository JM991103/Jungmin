using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkTypeEffect : MonoBehaviour
{
    /// <summary>
    /// 실제로 출력되는 변수
    /// </summary>
    TextMeshProUGUI msgText;

    /// <summary>
    /// 
    /// </summary>
    public int charPerSeconds;

    /// <summary>
    /// 대사가 끝나면 움직이는 오브젝트
    /// </summary>
    GameObject EndCursor;

    /// <summary>
    /// 
    /// </summary>
    string targetMsg;

    int index;

    float interval;

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        Transform parent = transform.parent;
        EndCursor = parent.GetChild(1).gameObject;
    }
    
    /// <summary>
    /// 대사를 받아오는 함수
    /// </summary>
    /// <param name="msg">받아오는 대사</param>
    public void SetMsg(string msg)
    {
        targetMsg = msg;    // 파라메터를 string변수에 넣어줌
        EffectStart();
    }

    void EffectStart()
    {
        msgText.text = "";                  // 출력되어있는 문자 초기화
        index = 0;                          // index 초기화
        EndCursor.SetActive(false);         

        interval = 1.0f / charPerSeconds;   
        Invoke("Effecting", interval);      
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        EndCursor.SetActive(true);
    }

}
