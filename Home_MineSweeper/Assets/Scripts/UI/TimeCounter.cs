using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    ImageNumber imageNumber;

    private void Awake()
    {
        imageNumber = GetComponent<ImageNumber>();
        GameManager gameManager = GameManager.Inst;
        gameManager.onTimeCountChange += Refresh;
    }

    private void Refresh(int Count)
    {
        imageNumber.Number = Count;
    }
}
