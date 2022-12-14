using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// 타이머가 시작된 이후의 경과 시간
    /// </summary>
    float elapsedTime = 0.0f;

    /// <summary>
    /// 타이머가 재생 중인지 여부
    /// </summary>
    bool isPlay = false;

    /// <summary>
    /// 타이머가 측정한 시간 읽기용 프로퍼티
    /// </summary>
    public float ElapsedTime => elapsedTime;

    private void Update()
    {
        if (isPlay)     // isPlay가 true일때만 실행
        {
            elapsedTime += Time.deltaTime;  // isPlay가 true일때 false가 되기 전까지 시간 누적
        }
    }

    /// <summary>
    /// 타이머 시작
    /// </summary>
    public void Play()
    {
        isPlay = true;
    }

    /// <summary>
    /// 타이머 시간 측정 정지
    /// </summary>
    public void Stop()
    {
        isPlay = false;
    }

    /// <summary>
    /// 타이머 시간 초기화 후 타이머 정지
    /// </summary>
    public void TimerReset()
    {
        elapsedTime = 0.0f;
        isPlay = false;
    }
}
