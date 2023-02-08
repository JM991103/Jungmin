using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    public Dialog[] parse(string _CSVFileName)
    {
        List<Dialog> dialogList = new List<Dialog>();   // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    // CSV파일 가져옴

        string[] data = csvData.text.Split(new char[] { '\n' });

        // 0번 줄은 의미없는 데이터이기 때문
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialog dialog = new Dialog();   // 대사 리스트 생성

            dialog.name = row[1];
            Debug.Log(row[1]);
            List<string> contextList = new List<string>();

            do{
                contextList.Add(row[2]);
                Debug.Log(row[2]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialog.contexts = contextList.ToArray();

            dialogList.Add(dialog);
        }
        return dialogList.ToArray();
    }
}
