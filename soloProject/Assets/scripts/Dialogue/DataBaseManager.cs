using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;         // static로 언제 어디서든 불러올 수 있다.

    [SerializeField]
    string csv_FileName;

    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();

    public static bool isFinish = false;            // 데이터 저장이 완료 되었는지 알려주는 변수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogParser theParser = GetComponent<DialogParser>();
            Dialog[] dialogs = theParser.parse(csv_FileName);   // csv_FileName 파일을 불러와서 배열에 저장

            for (int i = 0; i < dialogs.Length; i++)
            {
                dialogDic.Add(i + 1, dialogs[i]);
            }
            isFinish = true;
        }
    }

    /// <summary>
    /// 저장된 대사를 불러오는 함수
    /// </summary>
    /// <param name="startNum">시작하는 라인</param>
    /// <param name="endNum">끝나는 라인</param>
    /// <returns></returns>
    public Dialog[] GetDialogs(int startNum, int endNum)
    {
        List<Dialog> dialogList = new List<Dialog>();

        for (int i = 0; i < endNum - startNum; i++)
        {
            dialogList.Add(dialogDic[startNum + i]);
        }
        return dialogList.ToArray();
    }
}
