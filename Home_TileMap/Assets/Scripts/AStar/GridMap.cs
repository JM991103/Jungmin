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
                int index = GridToIndex(x, y);
                nodes[index] = new Node(x, y);      // ��� ���� �����ؼ� �迭�� �ֱ�
            }
        }
    }

    /// <summary>
    /// �׸��� �ʿ��� Ư�� �׸��� ��ǥ�� �����ϴ� ��� ã�� �Լ�
    /// </summary>
    /// <param name="x">Ÿ�ϸ� ���� x ��ǥ</param>
    /// <param name="y">Ÿ�ϸ� ���� y ��ǥ</param>
    /// <returns></returns>
    public Node GetNode(int x, int y)
    {
        if (IsValidPosition(x, y))
        {
            return nodes[GridToIndex(x, y)];
        }
        return null;
    }

    /// <summary>
    /// �׸��� �ʿ��� Ư�� �׸��� ��ǥ�� �����ϴ� ��� ã�� �Լ�
    /// </summary>
    /// <param name="pos">Ÿ�ϸ� �������� �� ��ǥ</param>
    /// <returns>ã�� ��� (������ null)</returns>
    public Node GetNode(Vector2Int pos)
    {
        return GetNode(pos.x, pos.y);
    }


    /// <summary>
    /// ���� ������ ��� ������ A* ������ �ʱ�ȭ
    /// </summary>
    public void ClearAStarData()
    {
        foreach (var node in nodes)
        {
            node.ClearAStarData();
        }
    }

    /// <summary>
    /// �Է� ���� ��ǥ�� �� �������� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="x">Ȯ���� ��ġ�� x</param>
    /// <param name="y">Ȯ���� ��ġ�� y</param>
    /// <returns>���̸� true, �ƴϸ� false</returns>
    public bool IsValidPosition(int x, int y)
    {

        return x >= 0 && y >= 0 && x < width && y < height;
    }

    /// <summary>
    /// �Է� ���� ��ǥ�� �� �������� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="pos">Ȯ���� ��ġ�� ��ǥ</param>
    /// <returns>�ʾ��̸� true, �ƴϸ� false</returns>
    public bool IsValidPosition(Vector2Int pos)
    {
        return IsValidPosition(pos.x, pos.y);
    }


    /// <summary>
    /// Grid��ǥ�� Index�� �����ϱ� ���� �Լ� (GetNode���� ����ϱ� ���� �Լ�)
    /// </summary>
    /// <param name="x">�׸��� ��ǥ X</param>
    /// <param name="y">�׸��� ��ǥ Y</param>
    /// <returns>�׸��� ��ǥ�� ����� �ε��� ��(nodes�� Ư�� ��带 ��� ���� �ε���)</returns>
    private int GridToIndex(int x, int y)
    {
        return x + ((height - 1) - y) * width; 
    }
}
