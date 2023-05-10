using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        // 0번 줄은 의미없는 데이터이기 때문에
        for (int i = 1; i < data.Length - 1;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();

            dialogue.number = int.Parse(row[1]);
            dialogue.name = row[2];
            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(row[3]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.context = contextList.ToArray();

            dialogList.Add(dialogue);            
        }

        return dialogList.ToArray();
    }

}
