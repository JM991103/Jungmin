using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ 1���� ǥ���� Ŭ����
/// </summary>
public class Item : MonoBehaviour
{
    // �����۵����� Ŭ������ �����´�.
    public ItemData data;

    private void Start()
    {
        // ������ �����Ϳ� �ִ� �������� ��ȯ�Ѵ�.
        Instantiate(data.modelPrefab, transform.position, transform.rotation, transform);
    }
}
