using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar 
{
    /// <summary>
    /// ���� Ž���ϴ� �Լ�
    /// </summary>
    /// <param name="gridMap">��ã�⸦ ������ ��</param>
    /// <param name="start">���� ��ġ(�׸��� ��ǥ)</param>
    /// <param name="goal">���� ��ġ(�׸��� ��ǥ)</param>
    /// <returns>������ġ���� ������ġ������ ���. (��ã�⿡ ������ ��� null)</returns>
    public static List<Vector2Int> PathFind(GridMap gridMap, Vector2Int start, Vector2Int goal)
    {
        gridMap.ClearAStarData();       // ���� ���� ��ã�⸦ �ϸ鼭 ������ �ִ� ������ �ʱ�ȭ
        List<Vector2Int> path = null;   // ���� ��ΰ� ����� ����Ʈ ����

        // ���������� ���������� �ʾȿ� ���� ���� ��ã�� ����
        if (gridMap.IsValidPosition(start) && gridMap.IsValidPosition(goal))
        {
            List<Node> open = new List<Node>();     // A*�� ���� ����Ʈ
            List<Node> close = new List<Node>();    // A*�� Ŭ���� ����Ʈ

            Node current = gridMap.GetNode(start);  // current�� ���� ��ġ�� ��带 ã��
            current.G = 0;
            current.H = GetHeuristic(current, goal);
            open.Add(current);                      // ���� ����Ʈ�� �߰�

            // A* �ٽ� ��ƾ ������ ����
            while(open.Count > 0)       // ���� ����Ʈ�� ����� ������ ��� ����. (���� ����Ʈ�� ������� ��θ� �� ã�� ��)
            {
                open.Sort();            // F���� �������� ����
                current = open[0];      // F���� ���� ���� ��带 current�� ����
                open.RemoveAt(0);       // ���� ����Ʈ���� current ������

                if (current != goal)    // ���� current�� ������������ Ȯ��
                {
                    // ���������� �ƴϸ� ��� �˰��� ����

                    close.Add(current); // ���� current Ŭ���� ����Ʈ�� �ֱ�

                    // current �ֺ� 8ĭ�� ���¸���Ʈ�� �ְų� �̹� ���¸���Ʈ�� ������ G�� ���� �õ�
                    for (int y = -1; y < 2; y++)
                    {
                        for (int x = -1; x < 2; x++)
                        {
                            Node node = gridMap.GetNode(current.x + x, current.y + y);  // �ֺ� ������ �ϳ��� ��󳻱�

                            // ��ŵ�� �͵��� ��ŵ
                            if (node == null)       // �� ���� ���
                                continue;
                            if (node == current)    // current�� Ȯ���� �ʿ䰡 ����. (�ڱ� �ڽ��̶�)
                                continue;
                            if (node.gridType == Node.GridType.wall)    // ���� ���
                                continue;
                            if (close.Exists( (x) => x == node))        // Ŭ���� ����Ʈ�� �ִ� ���
                                continue;

                            bool isDiagonal = Mathf.Abs(x) == Mathf.Abs(y); // �밢������ Ȯ��. true�� �밢��

                            // �밢���ε� ���� �ɸ��� ���
                            if (isDiagonal &&
                                (gridMap.GetNode(current.x + x, current.y).gridType == Node.GridType.wall
                                || gridMap.GetNode(current.x, current.y + y).gridType == Node.GridType.wall))
                                continue;

                            // current���� �̿��� node�� ���� �Ÿ� ����
                            float distance;
                            if (isDiagonal)
                            {
                                distance = 1.4f;    // �밢���̸� 1.4
                            }
                            else
                            {
                                distance = 1.0f;    // ���̸� 1
                            }

                            // ����� G���� current�� ���ļ� ���� �ͺ��� �� ū ��� G�� ����
                            if (node.G > current.G + distance)
                            {
                                // open ����Ʈ�� ���� ��� (G���� �ʱⰪ�� max�ϱ� ������ ����)
                                if (node.parent == null)                // ó�� ����ϴ� ���� == ���� ����Ʈ�� �ȵ� �ִ�.
                                {
                                    node.H = GetHeuristic(node, goal);  // �޸���ƽ �� ���
                                    open.Add(node);                     // open����Ʈ�� �߰�
                                }

                                // ���¸���Ʈ�� �ֵ� ���� �������� ó���Ǵ� �κ�
                                node.G = current.G + distance;          // g�� ����
                                node.parent = current;                  // �θ� ����
                            }
                        }
                    }

                }
                else
                {
                    // ���������̸� �� �̻� Ž���� ���� �ʰ� ��ƾ ����
                    break;
                }
            }
            if (current == goal)    // ���������� �����ߴ�.
            {
                // path �ϼ��ϱ�
                path = new List<Vector2Int>();  // ����Ʈ ����
                Node result = current;          // current���� �����ؼ�
                while (result != null)          // result�� �ִ� �� ��� ����(���� �θ�� ��� �����ߴµ� result�� null�̸� �������̶�� �Ҹ�)
                {
                    path.Add(new Vector2Int(result.x, result.y));
                    result = result.parent;     // ���� �θ�� �̵�
                }
                path.Reverse();                 // ���� -> �������� �Ǿ� �ִ� ����Ʈ�� ������
            }
        }
        return path;
    }

    /// <summary>
    /// �޸���ƽ ���� ���ϱ� ���� �Լ�
    /// </summary>
    /// <param name="current">���� ��ġ�� ���(�޸���ƽ ���� ����ϱ� ���� ���� ��ġ)</param>
    /// <param name="goal">�������� �׸��� ��ǥ</param>
    /// <returns>current���� goal������ ���� �Ÿ�</returns>
    static float GetHeuristic(Node current, Vector2Int goal)
    {
        return Mathf.Abs(current.x - goal.x) + Mathf.Abs(current.y - goal.y);
    }
}
