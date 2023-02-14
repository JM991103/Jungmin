using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;         // static로 언제 어디서든 불러올 수 있다.

    [SerializeField]
    string csv_FileName;

    Dictionary<int, Dialogue> dialogDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;            // 데이터 저장이 완료 되었는지 알려주는 변수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogParser theParser = GetComponent<DialogParser>();
            Dialogue[] dialogues = theParser.parse(csv_FileName);   // csv_FileName 파일을 불러와서 배열에 저장

            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogDic.Add(i + 1, dialogues[i]);                
            }
            isFinish = true;
        }
    }
    private void Start()
    {
        Dialogue[] arry = GetDialogue(1,1);

        Debug.Log(arry);
    }

    /// <summary>
    /// 저장된 대사를 불러오는 함수
    /// </summary>
    /// <param name="startNum">시작하는 라인</param>
    /// <param name="endNum">끝나는 라인</param>
    /// <returns></returns>
    public Dialogue[] GetDialogue(int startNum, int endNum)
    {
        List<Dialogue> dialogList = new List<Dialogue>();

        for (int i = 0; i <= (endNum - startNum); i++)
        {
            dialogList.Add(dialogDic[startNum + i]);
        }

        return dialogList.ToArray();
    }
}
