using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DialogueDB2 dialogueDB;
    TotalCharacter totalCharacter;

    public DialogueDB2 DialogueDB => dialogueDB;
    public TotalCharacter TotalCharacter => totalCharacter;

    protected override void Initialize()
    {
        base.Initialize();
        totalCharacter = transform.GetComponent<TotalCharacter>();
    }
}
