using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    /// �ȿ��� ������ ǥ�õ� �̹���
    /// </summary>
    public Sprite[] closeCellImages;

    /// <summary>
    /// OpenCellType���� �̹����� �޾ƿ��� �ε��� (enum)
    /// </summary>
    /// <param name="type">�ʿ��� �̹����� enumŸ��</param>
    /// <returns>enumŸ�Կ� �´� �̹���</returns>
    public Sprite this[OpenCellType type] => openCellImages[(int)type];

    /// <summary>
    /// CloseCellType �̹����� �޾ƿ��� �ε���
    /// </summary>
    /// <param name="type">�ʿ��� �̹����� enumŸ��</param>
    /// <returns>enumŸ�Կ� �´� �̹���</returns>
    public Sprite this[CloseCellType type] => closeCellImages[(int)type];

    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.RightClick.performed += OnRightClick;
        inputActions.Player.LeftClick.performed += OnLeftPress;
        inputActions.Player.LeftClick.canceled += OnLeftRelese;

    }

    private void OnDisable()
    {
        inputActions.Player.LeftClick.canceled -= OnLeftRelese;
        inputActions.Player.LeftClick.performed -= OnLeftPress;
        inputActions.Player.RightClick.performed -= OnRightClick;
        inputActions.Player.Disable();
    }


    /// <summary>
    /// �� ���尡 ���� ��� ���� �����ϰ� ��ġ�ϴ� �Լ� 
    /// </summary>
    public void Initialize(int newWidth, int newHeight, int mineCount)
    {
        // ������ �����ϴ� �� �� �����
        ClearCells();

        width = newWidth;
        height = newHeight;

        Vector3 basePos = transform.position;      // ���� ��ġ ���� (������ ��ġ)

        // ������ �Ǻ��� �߽����� ���� �����ǰ� �ϱ� ���� ���� ������ ������ ��� �뵵�� ���ϱ�
        Vector3 offset = new(-(width - 1) * Distance * 0.5f, (height - 1) * Distance * 0.5f);

        // ������ �迭 ����
        cells = new Cell[width * height];

        GameManager gameManager = GameManager.Inst;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cellObj = Instantiate(cellPrefab, transform);    // �� ���带 �θ�� ��� �������� �����Ѵ�.
                Cell cell = cellObj.GetComponent<Cell>();                   // ������ ������Ʈ���� Cell ������Ʈ ã��
                cell.ID = y * width + x;                                    // ID ���� (ID�� ���� ��ġ�� Ȯ�� ����)
                cell.Board = this;                                          // ���� ����
                cell.onFlagUes += gameManager.DecreaseFlagCount;
                cell.onFlagReturn += gameManager.IncreaseFlagCount;
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
    /// �Ķ���ͷ� ���� ID�� ���� �� �ֺ��� ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="id">ã�� �߽� ��</param>
    /// <returns>id�ֺ��� �ִ� ����</returns>
    public List<Cell> GetNeighbors(int id)
    {
        List<Cell> result = new List<Cell>(8);
        Vector2Int grid = IdToGrid(id);

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int index = GridToID(j + grid.x, i + grid.y);
                if (index != Cell.ID_NOT_VALID && !(i == 0 && j == 0))
                {
                    result.Add(cells[index]);
                }
            }
        }
        return result;
    }

    Vector2Int IdToGrid(int id)
    {
        return new Vector2Int(id % width, id / width);
    }

    int GridToID(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return x + y * width;
        }
        return Cell.ID_NOT_VALID;
    }

    /// <summary>
    /// �Է� ���� ��ũ�� ��ǥ�� ���° �׸��忡 �ִ��� �˷��ִ� �Լ�
    /// </summary>
    /// <param name="screenPos">Ȯ���� ��ũ�� ��ǥ</param>
    /// <returns>��ũ�� ��ǥ�� ��Ī�Ǵ� ���� ���� �׸��� ��ǥ</returns>
    Vector2Int ScreenToGird(Vector2 screenPos)
    {
        // ��ũ�� ��ǥ�� ���� ��ǥ�� �����ϱ�
        Vector2 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(screenPos);

        // ������ �߽������� ���� ��(���� ��ǥ) ���ϱ�
        Vector2 startPos = new Vector2(-width * Distance * 0.5f, height * Distance * 0.5f) + (Vector2)transform.position;

        // ������ ���� ������ ���콺�� �󸶸�ŭ ������ �ִ��� Ȯ�� 
        Vector2 Diff = worldPos - startPos;

        return new((int)(Diff.x / Distance), (int)(-Diff.y / Distance));
    }

    int ScreenToID(Vector2 screenPos)
    {
        Vector2Int grid = ScreenToGird(screenPos);
        return GridToID(grid.x, grid.y);
    }

    bool IsValidGrid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    bool IsValidGrid(Vector2Int grid)
    {
        return IsValidGrid(grid.x, grid.y);
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

    private void OnLeftPress(InputAction.CallbackContext _)
    {
        Debug.Log("���� ������.");
        Vector2 screenPos = Mouse.current.position.ReadValue(); // ���콺 Ŀ���� ��ũ�� ��ǥ �б�
        Vector2Int grid = ScreenToGird(screenPos);              // ��ũ�� ��ǥ�� Grid�� ��ȯ
        if (IsValidGrid(grid))                                  // ��� �׸��� ��ǥ�� �������� Ȯ�� => �������� ������ ���� ���̶�� �ǹ�
        {
            Cell target = cells[GridToID(grid.x, grid.y)];      // �ش� �� ��������
            target.CellPress();
        }
    }

    private void OnLeftRelese(InputAction.CallbackContext _)
    {
        Debug.Log("���� ����.");
        Vector2 screenPos = Mouse.current.position.ReadValue(); // ���콺 Ŀ���� ��ũ�� ��ǥ �б�
        Vector2Int grid = ScreenToGird(screenPos);              // ��ũ�� ��ǥ�� Grid�� ��ȯ
        if (IsValidGrid(grid))                                  // ��� �׸��� ��ǥ�� �������� Ȯ�� => �������� ������ ���� ���̶�� �ǹ�
        {
            Cell target = cells[GridToID(grid.x, grid.y)];      // �ش� �� ��������
            target.CellRelease();
        }
    }

    private void OnRightClick(InputAction.CallbackContext _)
    {
        
        Debug.Log("������ Ŭ��");
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2Int grid = ScreenToGird(screenPos);
        if (IsValidGrid(grid))
        {
            Cell target = cells[GridToID(grid.x, grid.y)];
            Debug.Log($"{target.gameObject.name}�� ��Ŭ�� �߽��ϴ�.");
            target.CellRightPress();
        }
        else
        {
            Debug.Log("�� ����");
        }
    }

}
