using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public NPCTalkManager npcTalkManager;

    /// <summary>
    /// 대사 패널
    /// </summary>
    public GameObject talkPanel;

    /// <summary>
    /// 대사 Text
    /// </summary>
    public TextMeshProUGUI talkText;

    /// <summary>
    /// 플레이어가 레이캐스트로 확인한 게임 오브젝트
    /// </summary>
    public GameObject scanObject;

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
    public int talkIndex;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        npcTalkManager = FindObjectOfType<NPCTalkManager>();
    }

    /// <summary>
    /// 오브젝트와 상호작용 하는 함수
    /// </summary>
    /// <param name="scanObj">레이캐스트로 확인한 오브젝트의 게임오브젝트</param>
    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj;
        // 게임 오브젝트의 ObjectData 컴포넌트를 가져온다
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        // ObjectData의 ID와 bool값을 Talk함수에 넣는다.
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    /// <summary>
    /// 해당 id와 bool 값에 맞는 
    /// </summary>
    /// <param name="id">오브젝트데이터의 id값</param>
    /// <param name="isNpc">NPC인지 아닌지 확인용</param>
    void Talk(int id, bool isNpc)
    {
        //id, talkIndex번째에 맞는 문자열을 talkData에 저장한다.
        string talkData = npcTalkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            player.OnMoveController(true);
            talkIndex = 0;
            return;
        }

        if (isNpc)  // npc일 때
        {
            talkText.text = talkData;
        }
        else        // npc가 아닐 때
        {
            talkText.text = talkData;
        }

        player.OnMoveController(false);
        
        isAction = true;
        talkIndex++;
    }
}
