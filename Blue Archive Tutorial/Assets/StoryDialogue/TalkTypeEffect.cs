using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkTypeEffect : MonoBehaviour
{
    TextMeshProUGUI msgText;

    public int charperSeconds;

    GameObject EndCursor;

    string targetMsg;

    int index;

    float interval;

    private void Awake()
    {
        msgText = transform.GetComponent<TextMeshProUGUI>();
        //EndCursor = 
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        //EndCursor.SetActive(false);

        interval = 1.0f / charperSeconds;
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
        //EndCursor.SetActive(true);
    }
}
