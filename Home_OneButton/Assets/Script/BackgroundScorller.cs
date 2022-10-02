using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScorller : MonoBehaviour
{
    public float scorlSpeed = 5.0f;     // ���ȭ���� ������ �ӵ�

    float width = 7.2f;     // ���ȭ���� ���� ����
    float edgPoint;         // �̵��ϸ鼭 �������� ����

    Transform[] bgSlots;    // Ʈ������ �迭�� ������ ���� (�迭�� ������ �󸶳����� �������� ����)
    // ���� ������ �޸� �Ҵ�

    private void Awake()
    {
        // �� ������ �����͸� ������ �� �ֵ��� �޸� �Ҵ�
        bgSlots = new Transform[transform.childCount];  // bgSlots�� Transform�� �ڽ��� ������ŭ �迭�� �־��ش�.

        for (int i = 0; i < transform.childCount; i++)  //i�� �ڽ��� ������ Ŭ������ �ݺ��ϴ� �ݺ���
        {
            bgSlots[i] = transform.GetChild(i);         // bgSlots[i]��° �迭�� i��° �ڽ��� �־���
        }

    }

    private void Start()
    {
        edgPoint = transform.position.x - width * 2.0f; //
    }

    private void Update()
    {
        foreach (var slot in bgSlots)
        {
            slot.Translate(scorlSpeed * Time.deltaTime * -transform.right);
            if (slot.position.x < edgPoint)
            {
                slot.Translate(width * bgSlots.Length * transform.right);
            }
        }


    }

}
