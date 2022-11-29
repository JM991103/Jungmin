using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// Ÿ�̸Ӱ� ���۵� ������ ��� �ð�
    /// </summary>
    float elapsedTime = 0.0f;

    /// <summary>
    /// Ÿ�̸Ӱ� ��� ������ ����
    /// </summary>
    bool isPlay = false;

    /// <summary>
    /// Ÿ�̸Ӱ� ������ �ð� �б�� ������Ƽ
    /// </summary>
    public float ElapsedTime => elapsedTime;

    private void Update()
    {
        if (isPlay)     // isPlay�� true�϶��� ����
        {
            elapsedTime += Time.deltaTime;  // isPlay�� true�϶� false�� �Ǳ� ������ �ð� ����
        }
    }

    /// <summary>
    /// Ÿ�̸� ����
    /// </summary>
    public void Play()
    {
        isPlay = true;
    }

    /// <summary>
    /// Ÿ�̸� �ð� ���� ����
    /// </summary>
    public void Stop()
    {
        isPlay = false;
    }

    /// <summary>
    /// Ÿ�̸� �ð� �ʱ�ȭ �� Ÿ�̸� ����
    /// </summary>
    public void TimerReset()
    {
        elapsedTime = 0.0f;
        isPlay = false;
    }
}
