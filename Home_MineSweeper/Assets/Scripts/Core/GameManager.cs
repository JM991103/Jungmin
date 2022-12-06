#define TEST_CODE
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Ÿ�̸� ���� --------------------------------------------------------------------------------------------
    // Timer Ŭ������ �����´�.
    private Timer timer;
    private int timeCount = 0;
    public int TimeCount
    {
        get => timeCount;
        private set
        {
            if (timeCount != value)
            {
                timeCount = value;
                onTimeCountChange?.Invoke(timeCount);
            }
        }
    }
    public Action<int> onTimeCountChange;


    // ��� ���� ���� --------------------------------------------------------------------------------------------
    private int flagCount = 0;
    public int FlagCount
    {
        get => flagCount;
        private set
        {
            flagCount = value;
            onFlagCountChange?.Invoke(flagCount);
        }
    }
    public Action<int> onFlagCountChange;

    // ���̵� ���� -----------------------------------------------------------------------------------------------
    public int mineCount = 10;
    public int boardWidth = 8;
    public int boardHeight = 8;
    private Board board;

    public Board Board => board;

    // �Լ� -----------------------------------------------------------------------------------------------------
    protected override void Initialize()
    {
        base.Initialize();
        timer = GetComponent<Timer>();

        FlagCount = mineCount;

        board = FindObjectOfType<Board>();
        board.Initialize(boardWidth, boardHeight, mineCount);
    }

    public void IncreaseFlagCount()
    {
        FlagCount++;
    }

    public void DecreaseFlagCount()
    {
        FlagCount--;
    }

    private void Update()
    {
        TimeCount = (int)timer.ElapsedTime;
    }

#if TEST_CODE
    public void TestTimer_Play()
    {
        timer.Play();
    }
    public void TestTimer_Stop()
    {
        timer.Stop();
    }
    public void TestTimer_Reset()
    {
        timer.TimerReset();
    }
    public void TestFlag_Increase()
    {
        FlagCount++;
    }
    public void TestFlag_Decrease()
    {
        FlagCount--;
    }
#endif
}
