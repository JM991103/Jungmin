using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
            player.OnMoveController(true);
        }
        else
        {
            player.OnMoveController(false);
            isAction = true;            
            scanObject = scanObj;
            talkText.text = $"이것의 이름은 {scanObject.name} 입니다.";
        }
        
        talkPanel.SetActive(isAction);
    }
}
