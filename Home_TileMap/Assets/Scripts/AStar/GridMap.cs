using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    /// ������ �׸��� ��ǥ (�� ���� �Ʒ� �� �κ��� �׸��� ��ǥ)
    /// </summary>
    Vector2Int origin;

    /// <summary>
    /// ���� Ÿ�ϸ�
    /// </summary>
    Tilemap background;
    
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
    /// �׸��� ���� Tilemap�� ����� �����ϴ� ������
    /// </summary>
    /// <param name="background">�׸������ ��ü ũ�⸦ ������ Ÿ�ϸ�(����,����,���� ����)</param>
    /// <param name="obstacle">�׸���ʿ��� ������ ������ Ÿ���� ������ Ÿ�ϸ�(�� ��ġ ����)</param>
    public GridMap(Tilemap background, Tilemap obstacle)
    {
        // background�� ũ�⸦ ������� nodes �����ϱ�
        width = background.size.x;      // background�� ũ�� �޾ƿͼ� ���� ���� ���̷� ���
        height = background.size.y;

        nodes = new Node[width * height];   // ��ü ��尡 �� �迭 ����

        // ���� �����ϴ� Node�� x,y��ǥ�� Ÿ�ϸʿ����� ��ǥ�� ���ƾ� �Ѵ�.
        origin = (Vector2Int)background.origin; // Ÿ�ϸʿ��� ��ϵ� ���� ����
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = GridToIndex(origin.x + x, origin.y + y);
                nodes[index] = new Node(origin.x + x, origin.y + y);        // ��� ���� �����ؼ� �迭�� �ֱ�
            }
        }

        // �� �� ���� ���� ǥ��(obstacle�� Ÿ���� �ִ� �κ��� Wall�� ǥ��)
        List<Vector2Int> movable = new List<Vector2Int>(width * height);
        for (int y = background.cellBounds.yMin; y < background.cellBounds.yMax; y++)
        {
            for (int x = background.cellBounds.xMin; x < background.cellBounds.xMax; x++)
            {
                // background �������� �ִ� obstacle�� Ȯ��
                TileBase tile = obstacle.GetTile(new(x, y));
                if (tile != null)   // Ÿ���� ������ �� �����̴�.
                {
                    Node node = GetNode(x, y);
                    node.gridType = Node.GridType.wall; // ������ ǥ��
                }
                else
                {
                    movable.Add(new Vector2Int(x, y));
                }
            }
        }
        

        // ��游 ���
        this.background = background;
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
        return x >= origin.x && y >= origin.y && x < (width + origin.x) && y < (height + origin.y);
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
    /// �ش� ��ġ�� ������ �ƴ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="x">Ȯ���� ��ġ�� x</param>
    /// <param name="y">Ȯ���� ��ġ�� y</param>
    /// <returns>���̸� true, �ƴϸ� false</returns>
    public bool IsWall(int x, int y)
    {
        Node node = GetNode(x, y);
        return node != null && node.gridType == Node.GridType.wall;
    }

    /// <summary>
    /// �ش� ��ġ�� ������ �ƴ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="pos">Ȯ���� ��ġ�� ��ǥ</param>
    /// <returns>���̸� true, �ƴϸ� false</returns>
    public bool IsWall(Vector2Int pos)
    {
        return IsWall(pos.x, pos.y);
    }

    /// <summary>
    /// ���� ��ǥ�� �׸��� ��ǥ�� �������ִ� �Լ�
    /// </summary>
    /// <param name="pos">���� ��ǥ</param>
    /// <returns>��ȯ�� �׸��� ��ǥ</returns>
    public Vector2Int WorldToGrid(Vector3 pos)
    {
        if (background != null)
        {
            return (Vector2Int)background.WorldToCell(pos);
        }
        else
        {
            return new Vector2Int(Mathf.FloorToInt(pos.x), (int)pos.y);
        }
    }

    /// <summary>
    /// �׸��� ��ǥ�� ���� ��ǥ�� �������ִ� �Լ�
    /// </summary>
    /// <param name="gridPos">�׸��� ��ǥ</param>
    /// <returns>����� ���� ��ǥ</returns>
    public Vector2 GridToWorld(Vector2Int gridPos)
    {
        if (background != null)
        {
            return background.CellToWorld((Vector3Int)gridPos) + new Vector3(0.5f, 0.5f);
        }
        else
        {
            return new Vector2(gridPos.x + 0.5f, gridPos.y + 0.5f);
        }
    }


    /// <summary>
    /// Grid��ǥ�� Index�� �����ϱ� ���� �Լ� (GetNode���� ����ϱ� ���� �Լ�)
    /// </summary>
    /// <param name="x">�׸��� ��ǥ X</param>
    /// <param name="y">�׸��� ��ǥ Y</param>
    /// <returns>�׸��� ��ǥ�� ����� �ε��� ��(nodes�� Ư�� ��带 ��� ���� �ε���)</returns>
    private int GridToIndex(int x, int y)
    {
        // (x,y) = x + y * �ʺ�;              // ������ �������� ���� ��
        // (x,y) = x + (����-1)-y) * �ʺ�     // ������ ���ʾƷ��� ���� ��

        return (x - origin.x) + ((height - 1) - y + origin.y) * width;  // ���� �Ʒ��� (0, 0)�̰� x+�� ������, y+�� �����̱� ������ �̷��� ��ȯ
    }
}
