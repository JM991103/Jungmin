using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkTypeEffect : MonoBehaviour
{
    TextMeshProUGUI msgText;

    public int charPerSeconds;
    GameObject EndCursor;
    string targetMsg;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        Transform parent = transform.parent;
        EndCursor = parent.GetChild(1).gameObject;
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