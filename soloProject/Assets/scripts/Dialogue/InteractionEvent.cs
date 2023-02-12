using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] 
    DialogueEvent dialogue;

    /// <summary>
    /// line.x 부터 line.y 까지 저장된 대사를 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Dialog[] GetDialogs()
    {
        dialogue.dialogs = DataBaseManager.instance.GetDialogs((int)dialogue.line.x, (int)dialogue.line.y);

        return dialogue.dialogs;
    }
}
