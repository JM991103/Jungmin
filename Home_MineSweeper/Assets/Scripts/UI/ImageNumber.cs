using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    /// <summary>
    /// ���� ��������Ʈ�� ��� ���� �迭
    /// </summary>
    public Sprite[] numbers;

    /// <summary>
    /// ���ڰ� ������ �̹����� �迭
    /// </summary>
    private Image[] numberImages;

    /// <summary>
    /// ǥ���� ����
    /// </summary>
    int number;

    /// <summary>
    /// ���ڸ� �����ϰ� ������ ������Ƽ
    /// </summary>
    public int Number
    {
        get => number;
        set
        {
            if (number != value)                        // ���� ����Ǹ�
            {
                number = Mathf.Clamp(value, -99, 999);  // ���� ������ -99 ~ 999������ �����Ѵ�.
                RefreshNumberImage();                   // �̹��� ���� �Լ� ȣ��
            }
        }
    }

    /// <summary>
    /// �������� ���� 0�� ǥ���ϴ� ��������Ʈ�� �����ִ� ������Ƽ
    /// </summary>
    Sprite zeroSprite => numbers[0];

    /// <summary>
    /// �������� ���� -�� ǥ���ϴ� ��������Ʈ�� �����ִ� ������Ƽ
    /// </summary>
    Sprite MinusSprite => numbers[10];

    /// <summary>
    /// �������� ���� ��ĭ�� ǥ���ϴ� ��������Ʈ�� �����ִ� ������Ƽ
    /// </summary>
    Sprite EmptySprite => numbers[11];

    private void Awake()
    {
        // ���ڸ� ������ ������Ʈ ��� ��������
        numberImages = GetComponentsInChildren<Image>();
    }

    private void RefreshNumberImage()
    {
        int tempNum = Mathf.Abs(number);        // ���밪���� ��ȯ�� ��ȣ�� ����, ������ +�� �����Ѵ�.
        Queue<int> digitsQ = new Queue<int>(3);  // �� �ڸ��� ���ڸ� ������ ť ����� ex) 3�̸� 3�ڸ� �� (���Լ��� ���)

        while (tempNum > 0)     // �� �ڸ��� ���� ���ڸ� �߶� digitQ�� �����ϱ�
        {
            digitsQ.Enqueue(tempNum % 10);
            tempNum /= 10;
        }

        int index = 0;              // ������ �ڸ��� Ȯ�� 3�ڸ��� 3�� ����.
        while (digitsQ.Count > 0)    // �ڸ� �� ���� �˸��� �̹����� ����
        {
            int num = digitsQ.Dequeue();                     // num�� ť�� ����ִ� ���ڸ� ������.
            numberImages[index].sprite = numbers[num];      // Image[index]��° �̹��� ��������Ʈ�� ���� ��������Ʈ�� ��Ƴ��� Sprite[num]�� �ִ´�.

            index++;    // �ڸ��� ����
        }

        for (int i = index; i < numberImages.Length; i++)   // ���� �����ϰ� ���� �ڸ������� ���ڸ� ��ĭ���� ä���
        {
            numberImages[i].sprite = EmptySprite;
        }

        if (number < 0) // ���ڰ� �������� ���
        {
            numberImages[numberImages.Length - 1].sprite = MinusSprite; // ���� ����ĭ�� - ǥ���ϱ�
        }

    }
}
