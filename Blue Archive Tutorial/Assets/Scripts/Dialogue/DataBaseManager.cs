using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField]
    string csv_FileName;

    Dictionary<int, Dialogue> dialogDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;    // 데이터 저장이 완료 되었는지 알려주는 변수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
    }
}
