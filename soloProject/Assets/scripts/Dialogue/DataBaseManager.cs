using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField]
    string csv_FileName;

    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();

    public static bool isFinish = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogParser theParser = GetComponent<DialogParser>();
            Dialog[] dialogs = theParser.parse(csv_FileName);

            for (int i = 0; i < dialogs.Length; i++)
            {
                dialogDic.Add(i + 1, dialogs[i]);
            }
            isFinish = true;
        }
    }

    public Dialog[] GetDialogs(int startNum, int endNum)
    {
        List<Dialog> dialogList = new List<Dialog>();

        for (int i = 0; i < endNum - startNum; i++)
        {
            dialogList.Add(dialogDic[startNum + 1]);
        }
        return dialogList.ToArray();
    }
}
