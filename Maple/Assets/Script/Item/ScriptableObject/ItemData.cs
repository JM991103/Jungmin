using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
// ScriptableObject : ���� �����͸� ����Ƽ�󿡼� ���� �����Ҽ� �ִ� ����Ÿ���� ���� �����
public class ItemData : ScriptableObject
{
    public uint id = 0;                     // ������ ID
    public string itemName = "������";      // �������� �̸�
    public GameObject modelPrefab;          // �������� ������ ǥ�� �� ������
    public uint value;                      // �������� ����
}
