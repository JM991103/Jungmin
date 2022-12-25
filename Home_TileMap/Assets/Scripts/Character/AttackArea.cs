using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    /// <summary>
    /// Slime�� �� ���� �ȿ� ������ ����Ǵ� ��������Ʈ
    /// </summary>
    public Action<Slime> onTarget;

    /// <summary>
    /// Slime�� �� ���� ������ ������ ����Ǵ� ��������Ʈ
    /// </summary>
    public Action<Slime> onTargetRelease;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))                  // �±� ���� Ȯ��(������ ���̶� ����Ǿ)
        {
            Slime slime = collision.GetComponent<Slime>();  // ������ �޾� ����
            if (slime != null)                              // �������� ������
            {
                onTarget?.Invoke(slime);                    // �������� ���Դٰ� ��ȣ ������
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))                  // �±� Ȯ��
        {
            Slime slime = collision.GetComponent<Slime>();  // ������ �޾� ����
            if (slime != null)                              // �������� ������
            {
                onTargetRelease?.Invoke(slime);             // �������� �����ٰ� ��ȣ ������
            }
        }
    }
}
