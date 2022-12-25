using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    // �� ���� : ���� �Ʒ��� ����
    // �� ���� : ���������� �� ���� x+, ���� �� ���� y+

    /// <summary>
    /// �׸���ʿ����� x��ǥ
    /// </summary>
    public int x;

    /// <summary>
    /// �׸���ʿ����� y��ǥ
    /// </summary>
    public int y;

    public enum GridType
    {
        plain = 0,  // �ٴ�
        wall,       // ��
        monster,    // ����
    }

    /// <summary>
    /// �� ����� ����
    /// </summary>
    public GridType gridType = GridType.plain;

    /// <summary>
    /// A* �˰����� G ��(���������� �� ������ ���µ� �ɸ� ���� �Ÿ�)
    /// </summary>
    public float G;

    /// <summary>
    /// A* �˰����� H ��(�� ��忡�� ������������ ���� �Ÿ�)
    /// </summary>
    public float H;

    /// <summary>
    /// A* �˰������� �� ��带 ������ ���������� ���������� �̵��� ���� �Ÿ�(���� ���� �Ÿ�)
    /// </summary>
    public float F => G + H;

    /// <summary>
    /// �� ����� �� ���
    /// </summary>
    public Node parent;

    /// <summary>
    /// Node ������
    /// </summary>
    /// <param name="x">��ġ x��ǥ</param>
    /// <param name="y">��ġ y��ǥ</param>
    /// <param name="gridType">����� ����. �⺻������ ����</param>
    public Node(int x, int y, GridType gridType = GridType.plain)
    {
        this.x = x;
        this.y = y;
        this.gridType = gridType;
        ClearAStarData();
    }

    /// <summary>
    /// A* ������ �ʱ�ȭ. �ٽ� ��ã�⸦ �� �� �ʱ�ȭ �뵵
    /// </summary>
    public void ClearAStarData()
    {
        G = float.MaxValue;
        H = float.MaxValue;
        parent = null;
    }

    /// <summary>
    /// ���� �񱳿����� IComparable�� ��ӹ��� ��� �����ؾ��ϴ� �Լ�
    /// </summary>
    /// <param name="other">�� ���</param>
    /// <returns></returns>
    public int CompareTo(Node other)
    {
        // ������ 0���� �۴� : this < other
        // ������ 0 : this, other�� ����.
        // ������ 0���� ũ�� : this > other
        if (other == null)
            return 1;
        //if (this.F < other.F)
        //{
        //    return -1;
        //}
        //else if (this.F > other.F)
        //{
        //    return 1;
        //}
        //else
        //{
        //    return 0;
        //}
        return this.F.CompareTo(other.F);
    }

    public override bool Equals(object obj)
    {
        return obj is Node other && this.x == other.x && this.y == other.y;
        // obj�� Node���� �ƴ��� Ȯ���ϰ� Node�� ������ other �̸����� �ٲ��ش�.
    }

    public override int GetHashCode()   // ���� �ٸ� �����͸� ������ �׻� ���� �ٸ� ���� ������ ���ִ� �Լ�
    {
        return HashCode.Combine(x, y);
    }

    /// <summary>
    /// == ��ɾ� �����ε�(==�� !=�� �ݵ�� ������ �����ؾ� �Ѵ�) // ������ �����ε�
    /// </summary>
    /// <param name="op1">�������� ���ʿ� �ִ� ����</param>
    /// <param name="op2">�������� �����ʿ� �ִ� ����</param>
    /// <returns>���� ���</returns>
    public static bool operator ==(Node op1, Vector2Int op2)
    {
        return op1.x == op2.x && op1.y == op2.y;
    }

    public static bool operator !=(Node op1, Vector2Int op2)
    {
        return (op1.x != op2.x) || (op1.y != op2.y);
    }
}
