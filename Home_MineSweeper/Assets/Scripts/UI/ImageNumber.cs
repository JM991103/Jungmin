using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    /// <summary>
    /// 숫자 스프라이트를 모아 놓은 배열
    /// </summary>
    public Sprite[] numbers;

    /// <summary>
    /// 숫자가 보여질 이미지의 배열
    /// </summary>
    private Image[] numberImages;

    /// <summary>
    /// 표현할 숫자
    /// </summary>
    int number;

    /// <summary>
    /// 숫자를 변경하고 적용할 프로퍼티
    /// </summary>
    public int Number
    {
        get => number;
        set
        {
            if (number != value)                        // 값이 변경되면
            {
                number = Mathf.Clamp(value, -99, 999);  // 숫자 범위를 -99 ~ 999까지로 조정한다.
                RefreshNumberImage();                   // 이미지 갱신 함수 호출
            }
        }
    }

    /// <summary>
    /// 가독성을 위해 0을 표시하는 스프라이트를 돌려주는 프로퍼티
    /// </summary>
    Sprite zeroSprite => numbers[0];

    /// <summary>
    /// 가독성을 위해 -를 표시하는 스프라이트를 돌려주는 프로퍼티
    /// </summary>
    Sprite MinusSprite => numbers[10];

    /// <summary>
    /// 가독성을 위해 빈칸을 표시하는 스프라이트를 돌려주는 프로퍼티
    /// </summary>
    Sprite EmptySprite => numbers[11];

    private void Awake()
    {
        // 숫자를 가져올 컴포넌트 모두 가져오기
        numberImages = GetComponentsInChildren<Image>();
    }

    private void RefreshNumberImage()
    {
        int tempNum = Mathf.Abs(number);        // 절대값으로 변환해 부호를 제거, 무조건 +로 변경한다.
        Queue<int> digitsQ = new Queue<int>(3);  // 각 자리수 숫자를 저장할 큐 만들기 ex) 3이면 3자리 수 (선입선출 방식)

        while (tempNum > 0)     // 각 자리수 별로 숫자를 잘라서 digitQ에 저장하기
        {
            digitsQ.Enqueue(tempNum % 10);
            tempNum /= 10;
        }

        int index = 0;              // 적용할 자리수 확인 3자리면 3번 돈다.
        while (digitsQ.Count > 0)    // 자리 수 별로 알맞은 이미지로 변경
        {
            int num = digitsQ.Dequeue();                     // num에 큐에 들어있는 숫자를 꺼낸다.
            numberImages[index].sprite = numbers[num];      // Image[index]번째 이미지 스프라이트를 숫자 스프라이트를 모아놓은 Sprite[num]을 넣는다.

            index++;    // 자리수 증가
        }

        for (int i = index; i < numberImages.Length; i++)   // 숫자 적용하고 남은 자리수들의 숫자를 빈칸으로 채우기
        {
            numberImages[i].sprite = EmptySprite;
        }

        if (number < 0) // 숫자가 음수였을 경우
        {
            numberImages[numberImages.Length - 1].sprite = MinusSprite; // 제일 왼쪽칸에 - 표시하기
        }

    }
}
