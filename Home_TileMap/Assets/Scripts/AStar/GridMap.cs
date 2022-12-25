using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridMap 
{
    /// <summary>
    /// �� �ʿ� �ִ� ��ü ������ �迭
    /// </summary>
    Node[] nodes;

    /// <summary>
    /// ���� ���� ũ��
    /// </summary>
    int width;

    /// <summary>
    /// ���� ���� ũ��
    /// </summary>
    int height;
    
    /// <summary>
    /// �׸������ ����� ���� ������
    /// </summary>
    /// <param name="width">������ ���� ���� ũ��</param>
    /// <param name="height">������ ���� ���� ũ��</param>
    public GridMap(int width, int height)
    {
        // �� ���� : ���� �Ʒ��� ����
        // �� ���� : ���������� �� ���� x+, ���� �� ���� y+

        this.width = width;         // ���� ���� ���� ���
        this.height = height;       

        nodes = new Node[width * height];   // ��� �迭 ����

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                nodes[x + y * width] = new Node(x, y);      // ��� ���� �����ؼ� �迭�� �ֱ�
            }
        }
    }
}
