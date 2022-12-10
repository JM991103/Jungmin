using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cell : MonoBehaviour
{
    /// <summary>
    /// ���� ���� � ǥ�ð� �ִ��� ��Ÿ���� enum
    /// </summary>
    enum CellMarkState
    {
        None = 0,       // �ƹ��͵� ǥ�� �ȵ�
        Flag,           // ��� ǥ�õ�
        Question        // ����ǥ ǥ�õ�
    }

    // ���� ----------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// ID�� �߸��Ǿ��ٰ� �˷��ִ� const
    /// </summary>
    public const int ID_NOT_VALID = -1;

    /// <summary>
    /// ���� ID�̸鼭 ��ġ�� ǥ���ϴ� ����
    /// </summary>
    int id = ID_NOT_VALID;

    /// <summary>
    /// ���� �����ִ��� �����ִ��� ����. true�� �����ְ� false�� �����ִ�.
    /// </summary>
    bool isOpen = false;

    /// <summary>
    /// �� ���� ���ڰ� �ִ��� ������ ����. true�� ���ڰ� �ְ� false�� ����.
    /// </summary>
    bool hasMine = false;

    /// <summary>
    /// �� ���� �������� �� ǥ�õǰ� �ִ� ����
    /// </summary>
    CellMarkState markState = CellMarkState.None;

    /// <summary>
    /// �ֺ� ���� ���� ����. ���� ������ �� ǥ���� �̹��� ����
    /// </summary>
    int aroundMineCount = 0;

    /// <summary>
    /// �� ���� ����ִ� ����
    /// </summary>
    Board parentBoard;

    /// <summary>
    /// ������ �� ���� ��������Ʈ ������
    /// </summary>
    SpriteRenderer cover;

    /// <summary>
    /// ������ �� ���� ��������Ʈ ������
    /// </summary>
    SpriteRenderer inside;

    /// <summary>
    /// �� ���� ���� ������ ���� ���(�ڱ� �ڽ� or �ڱ� �ֺ��� �����ִ� ��)
    /// </summary>
    List<Cell> pressedcells;

    List<Cell> neighbors;

    // ������Ƽ ----------------------------------------------------------------------------------------------------------------------------------------------

    public int ID
    {
        get => id;
        set
        {
            if (id == ID_NOT_VALID) // ID�� ó�� �ѹ��� ���� �����ϴ�.
            {
                id = value;
            }
        }
    }

    /// <summary>
    /// �� ���� �ҼӵǾ��ִ� ���� Ȯ�� �� ������ ������Ƽ(������ �ѹ��� ����)
    /// </summary>
    public Board Board
    {
        get => parentBoard;
        set
        {
            if (parentBoard == null)
            {
                parentBoard = value;
            }
        }
    }

    /// <summary>
    /// ���� ���ȴ��� �������� Ȯ���ϴ� ������Ƽ
    /// </summary>
    public bool IsOpen => isOpen;

    /// <summary>
    /// ���� ���ڰ� �ִ��� Ȯ���ϴ� ������Ƽ
    /// </summary>
    public bool HasMine => hasMine;

    /// <summary>
    /// ���� ����� ǥ�õǾ��ִ��� Ȯ���ϴ� ������Ƽ
    /// </summary>
    public bool IsFlaged => markState == CellMarkState.Flag;

    /// <summary>
    /// ���� ����ǥ�� ǥ�õǾ� �ִ��� Ȯ���ϴ� ������Ƽ
    /// </summary>
    public bool IsQuestion => markState == CellMarkState.Question;

    // ��������Ʈ------------------------------------------------------------------------------------------------------------------------------------------

    public Action onFlagUes;
    public Action onFlagReturn;

    // �Լ� ----------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        pressedcells = new List<Cell>(8);               // ���ο� �޸� �Ҵ�

        Transform child = transform.GetChild(0);
        cover = child.GetComponent<SpriteRenderer>();

        child = transform.GetChild(1);
        inside = child.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("������");
        if (Mouse.current.leftButton.ReadValue() > 0)
        {
            Debug.Log($"���콺 ���ʹ�ư�� ����ä�� ������\n{this.gameObject.name}");
            PressCover();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("������");
        if (Mouse.current.leftButton.ReadValue() > 0)
        {
            Debug.Log($"���콺 ���ʹ�ư�� ����ä�� ������\n{this.gameObject.name}");
            RestoreCover();
        }
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    void Open()
    {
        if (! isOpen && !IsFlaged)      // ���� �����ְ� ��� ǥ�ð� �ȵǾ����� ���� ����
        {
            isOpen = true;              // ���ȴٰ� ǥ���ϱ�

            if (hasMine)   // ���ڰ� ������
            {
                inside.sprite = Board[OpenCellType.Mine_Explosion]; // ������ �̹����� ����
            }
            cover.gameObject.SetActive(false);  // ���� ���� �� Ŀ���� ��Ȱ��ȭ

            if (aroundMineCount == 0 && ! hasMine)  // �ֺ� ���� ������ 0�̸�
            {
                foreach (var cell in neighbors) // �ֺ� ������
                {
                    cell.Open();                // ��� ����. (��� ȣ��)
                }
            }
        }
    }

    /// <summary>
    /// ���콺 ���� ��ư�� �� ���� ������ �� ����� �Լ�
    /// </summary>
    public void CellPress()
    {
        // ������ �̹����� ����
        pressedcells.Clear();   // ���Ӱ� ���������� ������ ������ �ִ� ���� ���� ����� ����
        if (IsOpen)
        {
            // �� ���� ������ ������, �ڽ� �ֺ��� ���� ���� ��� ���� ǥ�÷� �Ѵ�.
            foreach (var cell in neighbors)
            {
                if (!cell.IsOpen)       // �ֺ� �� �߿� �����ִ¼���
                {
                    pressedcells.Add(cell); // ������ �ִ� ���̶�� ǥ���ϰ�
                    cell.CellPress();       // ������ �ִ� ǥ�� ����
                }
            }
        }
        else
        {
            // �� ���� ���� ���� �� �ڽ��� ���� ǥ�ø� �Ѵ�.
            PressCover();
        }
    }

    /// <summary>
    /// ���콺 ���� ��ư�� �� �� ������ �������� �� ����� �Լ�
    /// </summary>
    public void CellRelease()
    {
        if (pressedcells.Count != 1)    // 1���� �ƴ� �� (2�� �̻��� ���� �� ó��)
        {
            int flagCount = 0;
            foreach (var cell in neighbors) // �ֺ��� �ִ� ��� ���� ����
            {
                if (cell.IsFlaged)
                {
                    flagCount++;
                }
            }
            if (flagCount == aroundMineCount)   // �ֺ��� ��� ������ �ֺ� ���� ������ ���� ���� ������ �͵� �� ����
            {
                foreach (var cell in pressedcells)
                {
                    cell.Open();
                }
            }
            else
            {
                RestoreCovers();        // ������ �ٸ��� �������ִ� ���� ����
            }
        }
        else
        {
            // 1�� �϶��� �ڱ� �ڽŸ� ���� ������
            pressedcells[0].Open();
        }
    }

    void PressCover()
    {
        switch (markState)
        {
            case CellMarkState.None:
                cover.sprite = Board[CloseCellType.Close_Press];
                break;                
            case CellMarkState.Question:
                cover.sprite = Board[CloseCellType.Question_Press];
                break;
            case CellMarkState.Flag:
            default:
                break;
        }
        pressedcells.Add(this); // ������ ���̶�� ǥ��
    }

    /// <summary>
    /// �� ���� �����ؼ� ������ �ִ� ������ ���� �� �� �ؾ��� ���� ��Ƴ��� �Լ�
    /// </summary>
    void RestoreCovers()
    {
        foreach (var cell in pressedcells)  // ���� ��ȸ�ϸ鼭 ����
        {
            cell.RestoreCover();
        }
        pressedcells.Clear();               // ����Ʈ ����
    }    

    void RestoreCover()
    {
        switch (markState)  // ǥ�� ���¿� ���� �̹��� ����
        {
            case CellMarkState.None:
                cover.sprite = Board[CloseCellType.Close];
                break;
            case CellMarkState.Question:
                cover.sprite = Board[CloseCellType.Question];
                break;
            case CellMarkState.Flag:
            default:
                break;
        }
    }

    /// <summary>
    /// �ֺ�8ĭ�� ���ڰ� �߰��ɶ� ����Ǵ� �Լ� 
    /// </summary>
    public void IncreaseAroundMineCount()
    {
        if (!hasMine)   // ���ڰ� ���� ����
        {
            aroundMineCount++;
            // aroundMineCount�� �����ϸ� inside�� ��������Ʈ �̹�����
            // aroundMineCount�� ���ڿ� �°� OpenCellType���� ĳ���� �ؼ� �־��ش�.
            inside.sprite = Board[(OpenCellType)aroundMineCount];   // �ֺ� ���� ���ڿ� �°� �̹��� ����
        }
    }

    /// <summary>
    /// �� ���� ���ڸ� �߰��ϴ� �Լ�
    /// </summary>
    public void SetMine()
    {
        hasMine = true;     // ���ڰ� ��ġ �Ǿ��ٰ� ǥ��
        inside.sprite = Board[OpenCellType.Mine_NotFound];  // ���� �̹����� ����

        // �� �� �ֺ� ������ IncreaseAroundMineCount�Լ� ���� (aroundMineCount�� 1�� ����)
        List<Cell> cellList = Board.GetNeighbors(ID); 
        foreach (var cell in cellList)
        {
            cell.IncreaseAroundMineCount();
        }
    }

    /// <summary>
    /// ���� ������ Ŭ������ �� ����� �Լ�
    /// </summary>
    public void CellRightPress()
    {
        if (!isOpen)    
        {
            switch (markState)
            {
                case CellMarkState.None:
                    // markState�� none�̸� flag�� �ȴ� -> ��� ������ �پ���. �� �̹��� ����ȴ�.
                    markState = CellMarkState.Flag;
                    cover.sprite = Board[CloseCellType.Flag];
                    onFlagUes?.Invoke();
                    break;
                case CellMarkState.Flag:
                    // markState�� flag�̸� Question�� �ȴ� -> ��� ������ �þ��. �� �̹��� ����ȴ�.
                    markState = CellMarkState.Question;
                    cover.sprite = Board[CloseCellType.Question];
                    onFlagReturn?.Invoke();
                    break;
                case CellMarkState.Question:
                    // markState�� Question�̸� none�� �ȴ� -> �� �̹��� ����ȴ�.
                    markState = CellMarkState.None;
                    cover.sprite = Board[CloseCellType.Close];
                    break;
                default:
                    break;
            }

        }
    }
}
