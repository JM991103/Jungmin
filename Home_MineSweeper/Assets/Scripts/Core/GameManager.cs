#define TEST_CODE
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 게임 상태 관련 -----------------------------------------------------------------------------------------

    enum GameState
    {
        Ready = 0,  // 게임 시작 전(첫 번째 셀이 안열린 상황)
        Play,       // 게임 진행 중
        GameClear,  // 모든 지뢰를 찾았을 때
        GameOver    // 지뢰가 있는 셀을 열었을 때
    }

    GameState state = GameState.Ready;

    /// <summary>
    /// 게임이 진행중인지 확인하는 프로퍼티
    /// </summary>
    public bool IsPlaying => state == GameState.Play;

    /// <summary>
    /// Play 상태로 돌아갔을 때 실행될 델리게이트
    /// </summary>
    public Action onGameStart;

    /// <summary>
    /// 게임이 재시작 될 때 실행될 델리게이트(리셋 버튼을 눌렀을 때. 보드 초기화 될 때. Ready 상태로 변경)
    /// </summary>
    public Action onGameReset;

    /// <summary>
    /// GameClear 상태로 들어갔을 때 실행될 델리게이트
    /// </summary>
    public Action onGameClear;

    /// <summary>
    /// GameOver 상태로 들어갔을 때 실행될 델리게이트
    /// </summary>
    public Action onGameOver;

    // 타이머 관련 --------------------------------------------------------------------------------------------
    // Timer 클래스를 가져온다.
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


    // 깃발 갯수 관련 --------------------------------------------------------------------------------------------
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

    // 난이도 관련 -----------------------------------------------------------------------------------------------
    public int mineCount = 10;
    public int boardWidth = 8;
    public int boardHeight = 8;
    private Board board;

    public Board Board => board;

    // 함수 -----------------------------------------------------------------------------------------------------
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
            Debug.Log("Play 상태");
        }
    }

    public void GameReset()
    {
        state = GameState.Ready;
        onGameReset?.Invoke();
        Debug.Log("Ready 상태");
    }

    public void GameClear()
    {
        state = GameState.GameClear;
        onGameClear?.Invoke();
        Debug.Log("Clear 상태");
    }

    public void GameOver()
    {
        state = GameState.GameOver;
        onGameOver?.Invoke();
        Debug.Log("GameOver 상태");
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
