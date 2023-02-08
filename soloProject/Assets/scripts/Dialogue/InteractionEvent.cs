using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialog[] GetDialogs()
    {
        dialogue.dialogs = DataBaseManager.instance.GetDialogs((int)dialogue.line.x, (int)dialogue.line.y);

        return dialogue.dialogs;
    }
}
