using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    const int ID_NOT_VALID = -1;

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

    // �Լ� ----------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("������");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("������");
    }


    /// <summary>
    /// ���� ��� -> ��� -> ����ǥ -> ��� ->  ������� ǥ���ϴ� �Լ�
    /// </summary>
    void SetMark()
    {

    }

    /// <summary>
    /// ���� �������� �� ����� �Լ�
    /// </summary>
    public void CellPress()
    {
        // ������ �̹����� ����
    }

    /// <summary>
    /// ���� ���� ���� �� ����� �Լ�
    /// </summary>
    public void CellRelease()
    {
        // ���� ��� : Open();
        // �����Ǵ� ���
    }

    /// <summary>
    /// �ֺ�8ĭ�� ���ڰ� �߰��ɶ� ����Ǵ� �Լ� 
    /// </summary>
    public void IncreaseAroundMineCount()
    {
        aroundMineCount++;
    }

    /// <summary>
    /// �� ���� ���ڸ� �߰��ϴ� �Լ�
    /// </summary>
    public void SetMine()
    {
        hasMine = true;
    }

}
