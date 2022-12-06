using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCounter : MonoBehaviour
{
    ImageNumber imageNumber;

    private void Awake()
    {
        imageNumber = GetComponent<ImageNumber>();
        GameManager gameManager = GameManager.Inst;
        gameManager.onFlagCountChange += Refresh;
        Refresh(gameManager.FlagCount);
    }

    private void Refresh(int Count)
    {
        imageNumber.Number = Count;
    }
}
