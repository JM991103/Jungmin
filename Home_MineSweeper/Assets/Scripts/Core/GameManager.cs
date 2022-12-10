#define TEST_CODE
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // ���� ���� ���� -----------------------------------------------------------------------------------------

    enum GameState
    {
        Ready = 0,  // ���� ���� ��(ù ��° ���� �ȿ��� ��Ȳ)
        Play,       // ���� ���� ��
        GameClear,  // ��� ���ڸ� ã���� ��
        GameOver    // ���ڰ� �ִ� ���� ������ ��
    }

    GameState state = GameState.Ready;

    /// <summary>
    /// ������ ���������� Ȯ���ϴ� ������Ƽ
    /// </summary>
    public bool IsPlaying => state == GameState.Play;

    /// <summary>
    /// Play ���·� ���ư��� �� ����� ��������Ʈ
    /// </summary>
    public Action onGameStart;

    /// <summary>
    /// ������ ����� �� �� ����� ��������Ʈ(���� ��ư�� ������ ��. ���� �ʱ�ȭ �� ��. Ready ���·� ����)
    /// </summary>
    public Action onGameReset;

    /// <summary>
    /// GameClear ���·� ���� �� ����� ��������Ʈ
    /// </summary>
    public Action onGameClear;

    /// <summary>
    /// GameOver ���·� ���� �� ����� ��������Ʈ
    /// </summary>
    public Action onGameOver;

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

    public void GameStart()
    {
        if (state == GameState.Ready)
        {
            state = GameState.Play;
            onGameStart?.Invoke();
            Debug.Log("Play ����");
        }
    }

    public void GameReset()
    {
        state = GameState.Ready;
        onGameReset?.Invoke();
        Debug.Log("Ready ����");
    }

    public void GameClear()
    {
        state = GameState.GameClear;
        onGameClear?.Invoke();
        Debug.Log("Clear ����");
    }

    public void GameOver()
    {
        state = GameState.GameOver;
        onGameOver?.Invoke();
        Debug.Log("GameOver ����");
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
