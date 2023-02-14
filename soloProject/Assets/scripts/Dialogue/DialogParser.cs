using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    public Dialogue[] parse(string _CSVFileName)
    {
        List<Dialogue> dialogList = new List<Dialogue>();               // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    // 에셋 Resources폴더에서 CSV파일 가져옴

        string[] data = csvData.text.Split(new char[] { '\n' });        // 엔터를 기준으로 쪼개서 변수에 저장

        // 0번 줄은 의미없는 데이터이기 때문
        for (int i = 1; i < data.Length - 2;)
        {
            string[] row = data[i].Split(new char[] { ',' });           // row 배열에 data[i]번째에 들어있는 문자열을 ',' 기준으로 쪼개서 변수에 저장

            Dialogue dialogue = new Dialogue();                               

            dialogue.name = row[1];                                       // 쪼개진 이름을 넣어줌(row[0] ID, row[1]이름, row[2]대사) 
            List<string> contextList = new List<string>();              // 대사를 저장할 List생성

            do{
                contextList.Add(row[2]);                                // 리스트에 대사를 넣어줌
                if (++i < data.Length)                                  // for문의 i를 증가 시키고 data의 길이보다 작으면 실행
                {
                    row = data[i].Split(new char[] { ',' });            // row 배열에 data[i]번째에 들어있는 문자열을 ',' 기준으로 쪼개서 변수에 저장
                }
                else
                {
                    break;                                              // i가 data보다 커지면 반복문을 빠져나옴
                }
            } while (row[0].ToString() == "");                          // row[0] ID가 공백이면 반복하는 반복문
            
            dialogue.contexts = contextList.ToArray();                    // dialog.contexts 배열에 contextList의 내용을 배열로 변환해 넣어준다

            dialogList.Add(dialogue);                                     // dialogList에 이름과 대사를 넣어준다.

        }
                
        return dialogList.ToArray();                                    // dialogList를 배열로 변환해 반환한다.
    }
}
