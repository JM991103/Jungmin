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

    /// <summary>
    /// CSV파일에 들어있는 라인의 숫자
    /// </summary>
    int lineCount = 0;

    /// <summary>
    /// 해당 lineCount에 들어있는 대사 배열의 카운트 숫자
    /// </summary>
    int contextCount = 0;

    /// <summary>
    /// 이름과 대사가 들어있는 배열
    /// </summary>
    Dialogue[] dialogue;

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

    /// <summary>
    /// 오브젝트와 상호작용 하는 함수
    /// </summary>
    /// <param name="scanObj">레이캐스트로 확인한 오브젝트의 게임오브젝트</param>
    public void Action(GameObject scanObj)
    {
        InteractionEvent interaction = scanObj.GetComponent<InteractionEvent>();
        //Debug.Log($"들어갈 대사{interaction.GetDialogues()}");
        dialogue = interaction.GetDialogues();

        isAction = true;
                
        talkPanel.SetActive(isAction);  // 토크 패널 열기

        Talks(dialogue);                // dialogue에 있는 대사 가져오기
    }

    /// <summary>
    /// 다이얼로그 배열에 있는 대사를 불러오는 함수
    /// </summary>
    /// <param name="dialogue">대사가 저장 되어있는 배열</param>
    void Talks(Dialogue[] dialogue)
    {
        // 대사를 가져와서 string 변수에 넣음
        string talkData = GetTalk(dialogue, talkIndex);

        // 가져온 GetTalk가 null이면 
        if (talkData == null)
        {
            isAction = false;               // 패널을 false로 변경
            talkIndex = 0;                  // talkIndex초기화
            player.OnMoveController(true);  // 플레이어 이동 연결
            talkPanel.SetActive(false);     // 패널 닫기
            return;
        }

        isAction = true;

        talkData = talkData.Replace("'", ",");  // 저장되어있는 대사에 '를 ,로 변환한다
        talk.SetMsg(talkData);              // 출력되는 대사가 한글자씩 나오게 하는 효과

        player.OnMoveController(false);     // 플레이어 이동 연결 해제(이동 불가)
        talkIndex++;                        // 다음 대사를 위해 talkIndex증가
    }

    /// <summary>
    /// talkIndex랑 dialogues의 contexts.Length와 다르면 다음 대사를 리턴하는 함수
    /// </summary>
    /// <param name="dialogues">저장되어있는 대사 배열</param>
    /// <param name="talkindex">대사를 출력할때마다 1증가</param>
    /// <returns>true면 null, false면 다음 대사 출력</returns>
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
