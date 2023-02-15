using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    //NPCTalkManager npcTalkManager;

    /// <summary>
    /// 대사 패널
    /// </summary>
    public GameObject talkPanel;

    /// <summary>
    /// 대사 Text
    /// </summary>
    TalkTypeEffect talk;

    /// <summary>
    /// 플레이어가 레이캐스트로 확인한 게임 오브젝트
    /// </summary>
    public Dialogue[] dialogs;

    /// <summary>
    /// 대사 패널이 열려있는지 닫혀있는지 알려주는 bool변수
    /// </summary>
    public bool isAction;

    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// id에 포함 되어있는 Index 번째의 대사
    /// </summary>
    public int talkIndex = 0;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        //npcTalkManager = FindObjectOfType<NPCTalkManager>();
        talk = FindObjectOfType<TalkTypeEffect>();
    }

    private void Start() 
    {
        talkPanel.SetActive(false);
    }

    int lineCount = 0;
    int contextCount = 0;
    Dialogue[] dialogue;
    /// <summary>
    /// 오브젝트와 상호작용 하는 함수
    /// </summary>
    /// <param name="scanObj">레이캐스트로 확인한 오브젝트의 게임오브젝트</param>
    public void Action(GameObject scanObj)
    {
        InteractionEvent interaction = scanObj.GetComponent<InteractionEvent>();
        Debug.Log($"들어갈 대사{interaction.GetDialogues()}");
        dialogue = interaction.GetDialogues();

        isAction = true;

        talkPanel.SetActive(isAction);
            
        Talks(dialogue);        
    }


    IEnumerator TypeWriter()
    {
        talkPanel.SetActive(isAction);

        string ReplaceText = dialogs[lineCount].contexts[contextCount];
        ReplaceText = ReplaceText.Replace("'", ",");

        talk.SetMsg(ReplaceText);

        yield return null;
    }

    void Talks(Dialogue[] dialogue)
    {
        string talkData = GetTalk(dialogue, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            player.OnMoveController(true);
            talkPanel.SetActive(false);
            return;
        }

        isAction = true;

        talkData = talkData.Replace("'", ",");
        talk.SetMsg(talkData);

        player.OnMoveController(false);
        talkIndex++;        
    }

    public string GetTalk(Dialogue[] dialogues, int talkindex)
    {
        if (talkIndex == dialogues[lineCount].contexts.Length)   // talkIndex가 talkData[id]의 길이와 같아지면
        {
            return null;                        // 대사 종료
        }
        else
        {
            //talkIndex가 talkData[id]의 길이보다 작으면
            return dialogues[lineCount].contexts[talkindex];     // 다음 대사 실행
        }
    }
}
