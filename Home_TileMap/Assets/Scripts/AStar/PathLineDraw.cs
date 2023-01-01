using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ��θ� �׸��� ���� Ŭ����
public class PathLineDraw : MonoBehaviour
{
    /// <summary>
    /// ��θ� �׸� ���� ������
    /// </summary>
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// �Է� ���� ��δ�� ���� �������� �׸��� �Լ�
    /// </summary>
    /// <param name="map">��� ��</param>
    /// <param name="path">��� �ʿ��� ������ ���</param>
    void DrawPath(GridMap map, List<Vector2Int> path)
    {
        // ��� ���̿� ���� ���� �������� �����ϴ� ���� ���� ����
        lineRenderer.positionCount = path.Count;

        int index = 0;
        foreach (var node in path)                      // ��θ� �ϳ��ϳ� ���󰡸�
        {
            Vector2 worldPos = map.GridToWorld(node);   // ���� ��ǥ�� ���
            lineRenderer.SetPosition(index, new(
                worldPos.x - lineRenderer.transform.position.x, 
                worldPos.y - lineRenderer.transform.position.y,
                1));    // ���� �������� ���� (���� �������� position�� local ��ǥ�� �����Ǿ�� �ϱ� ������ ��ȯ���� �߰�)

            index++;
        }
    }
}
