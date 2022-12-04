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

    /// <summary>
    /// ���� ������ ǥ�õ� �̹���
    /// </summary>
    public Sprite[] openCellImages;

    /// <summary>
    /// OpenCellType���� �̹����� �޾ƿ��� �ε��� (enum)
    /// </summary>
    /// <param name="type">�ʿ��� �̹����� enumŸ��</param>
    /// <returns>enumŸ�Կ� �´� �̹���</returns>
    public Sprite this[OpenCellType type] => openCellImages[(int)type];


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
                cell.ID = y * width + x;                                    // ID ���� (ID�� ���� ��ġ�� Ȯ�� ����)
                cell.Board = this;                                          // ���� ����
                cell.name = $"Cell_{cell.ID}_{x}_{y}";                      // ������Ʈ �̸� ����
                cell.transform.position = basePos + offset + new Vector3(x * Distance, -y * Distance);  // ������ ��ġ�� ��ġ
                cells[cell.ID] = cell;                                      // cells �迭�� ����
            }
        }

        // ������� ���� ���ڸ� minCount��ŭ ��ġ�ϱ�
        int[] ids = new int[cells.Length];
        for (int i = 0; i < cells.Length; i++)
        {
            ids[i] = i;
        }
        Shuffl1e(ids);
        for (int i = 0; i < mineCount; i++)
        {
            cells[ids[i]].SetMine();
        }        
    }

    /// <summary>
    /// �Ķ���ͷ� ���� �迭 ������ ������ ������ ���� �Լ�
    /// </summary>
    /// <param name="source">���� �����͸� ���� �迭</param>
    public void Shuffl1e(int[] source)
    {
        // �Ǽ� ������ �˰��� ���� �ϱ�
        // - 1�� �ϴ� ���� : �迭�� 0���� �����ϰ� Length�� 1���� ���ڸ� ���� ����
        int count = source.Length - 1;

        for (int i = 0; i < count; i++) // �迭�� ���� - 1��ŭ for���� ������.
        {
            // �迭�� 5�϶� 4���߿� 3���߿� 2���߿� ���������� �������� �Ѱ��� �̾ƾ� �ϴµ�
            // 0 ���� 4 + 1 - i�� �ؼ� �������� ���ڸ� �����Ѵ�.
            int randomIndex = Random.Range(0, count + 1 - i);
            // �������ε����� 5��° 4��° 3��° ���������� �پ� ���� �Ѵ�.
            int lastIndex = count - i;
            (source[randomIndex], source[lastIndex]) = (source[lastIndex], source[randomIndex]);    // swap ó��
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
