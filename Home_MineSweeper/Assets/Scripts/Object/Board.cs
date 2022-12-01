using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    /// <summary>
    /// ������ ������
    /// </summary>
    public GameObject cellPrefab;

    /// <summary>
    /// ���尡 ������ ���� ���� ���� (���� ���� �� ����)
    /// </summary>
    private int width = 16;

    /// <summary>
    /// ���尡 ������ ���� ���� ���� (���� ���� �� ����)
    /// </summary>
    private int height = 16;

    /// <summary>
    /// �� �� ���� ���� (���� �������)
    /// </summary>
    const float Distance = 1.0f;    // 1�� �� ī�޶� ũ�� 9

    /// <summary>
    /// �� ���尡 ������ ��� ��
    /// </summary>
    Cell[] cells;

    //private void Start()
    //{
    //    Initialize(width, height, 10);
    //}

    /// <summary>
    /// �� ���尡 ���� ��� ���� �����ϰ� ��ġ�ϴ� �Լ� 
    /// </summary>
    public void Initialize(int newWidth, int newHeight, int mineCount)
    {
        ClearCells();

        width = newWidth;
        height = newHeight;

        Vector3 basePos = transform.position;      // ���� ��ġ ���� (������ ��ġ)

        // ������ �Ǻ��� �߽����� ���� �����ǰ� �ϱ� ���� ���� ������ ������ ��� �뵵�� ���ϱ�
        Vector3 offset = new(-(width - 1) * Distance * 0.5f, (height - 1) * Distance * 0.5f);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cellObj = Instantiate(cellPrefab, transform);    // �� ���带 �θ�� ��� �������� �����Ѵ�.
                Cell cell = cellObj.GetComponent<Cell>();                   // ������ ������Ʈ���� Cell ������Ʈ ã��
                cell.name = $"Cell_{cell.ID}_{x}_{y}";                      // ������Ʈ �̸� ����
                cell.transform.position = basePos + offset + new Vector3(x * Distance, -y * Distance);  // ������ ��ġ�� ��ġ
                cells[cell.ID] = cell;                                      // cells �迭�� ����
            }
        }        
    }

    /// <summary>
    /// ������ ��� ���� �����ϴ� �Լ�
    /// </summary>
    public void ClearCells()
    {
        if (cells != null)      // ������ ������� ���� ������  
        {
            // �̹� ������� �� ������Ʈ�� ��� �����ϱ�
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
            cells = null;   // ���� ������ �� �����ߴٰ� ǥ��
        }
    }
}
