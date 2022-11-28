using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������ �ϴ� Ŭ����. ���丮 ������ ����
/// ������ �ϴٺ��� ��� ���� ������ �������� �����ϴ��� �𸣱� ������
/// �����ϴ� �κ��� �Ѱ��� �� ��� �������� ����� ����
/// </summary>
public class ItemFactory
{ 
    // �Լ��� ������� : �̸�, �Ķ����, ���� ��, �ٵ�(�ڵ�)
    // overloading(�����ε�) : �̸��� ������ �Ķ���Ͱ� �ٸ� �Լ��� ����� ��
    // overriding(�������̵�) : �̸�, �Ķ����, ���ϰ��� ���� �Լ��� ����� �� 

    static int itemCount = 0;   // ������ �������� �� ����. ������ ���� ���̵��� ���ҵ� ��.

    //static : ���� ������ �޸� �ּ��� ��ȭ�� ����.new�� ���� �ʾƵ� ������ ������ ����.
    /// <summary>
    /// ItemIDCode�� ������ ����
    /// </summary>
    /// <param name="code">������ ������ �ڵ�</param>
    /// <returns>���� ���</returns>
    public static GameObject MakeItem(ItemIDCode code)
    {
        GameObject obj = new GameObject();  // new�� �ϰ� �Ǹ� �� ������Ʈ�� �����ȴ�.

        Item item = obj.AddComponent<Item>();           // Item ������Ʈ �߰��ϱ�
        item.data = GameManager.Inst.ItemData[code];

        string[] itemName = item.data.name.Split("_");      // 00_������   Split("_")�� �ϸ� _�� �������� �յڷ� ���� ���ش�.
        obj.name = $"{itemName[1]}_{itemCount++}";          // {itemName[1]} ������ �ϰ� �Ǹ� �ߺ��� �̸��� ������ ������ ������ ǥ�����ִ� ������ ����Ѵ�.
        obj.layer = LayerMask.NameToLayer("Item");          // ���̾ Item���� ������ �ش�.

        CircleCollider2D cc = obj.AddComponent<CircleCollider2D>(); // �ö��̴� �߰�
        cc.isTrigger = true;
        cc.radius = 0.5f;

        return obj;
    }

    /// <summary>
    /// ������ �ڵ带 �̿��� Ư�� ��ġ�� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="code">������ ������ �ڵ�</param>
    /// <param name="position">������ ��ġ</param>
    /// <param name="randomNoise">��ġ�� �������� ������ ����, true�� �ణ�� �������� ���Ѵ�. �⺻�� = flase</param>
    /// <returns>������ ������</returns>
    public static GameObject MakeItem(ItemIDCode code, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem(code);    // �����
        if (randomNoise)                    // ��ġ�� �������� ���ϸ�
        {
            // insideUnitCircle : ������ 1¥�� ���� �׸��� �� �ȿ� ���� ��ǥ���� �����ش�.
            Vector2 noise = Random.insideUnitCircle * 0.5f; // ������ 0.5�� ���� ���ʿ��� ������ ��ġ ����
            position.x = noise.x;           // ���� ������ ��ġ�� �Ķ���ͷ� ���� ���� ��ġ�� �߰�
            position.y = noise.y;   
        }
        obj.transform.position = position;  // ��ġ����
        return obj;
    }

    /// <summary>
    /// ������ �ڵ带 �̿��ؼ� �������� �ѹ��� ������ �����ϴ� �Լ� 
    /// </summary>
    /// <param name="code">������ �������� ������ �ڵ�</param>
    /// <param name="count">������ ����</param>
    /// <returns>������ �����۵��� ��� �迭</returns>
    public static GameObject[] MakeItems(ItemIDCode code, int count)
    {
        GameObject[] objs = new GameObject[count];  // �迭 �����
        for (int i = 0; i < count; i++)     
        {
            objs[i] = MakeItem(code);               // count��ŭ �ݺ��ؼ� ������ ����
        }
        return objs;                                // ������ �� ����
    }

    /// <summary>
    /// �ڵ带 �̿��� Ư�� ��ġ�� �������� �ѹ��� ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="code">������ �������� ������ �ڵ�</param>
    /// <param name="count">������ ����</param>
    /// <param name="position">������ ���� ��ġ</param>
    /// <param name="randomNoise">��ġ�� �������� ������ ����. true�� �ణ�� �������� ���Ѵ�. �⺻���� false</param>
    /// <returns></returns>
    public static GameObject[] MakeItems(ItemIDCode code, int count ,Vector3 position, bool randomNoise = false)
    {
        GameObject[] objs = new GameObject[count];  // �迭 �����
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(code, position, randomNoise);               // count��ŭ �ݺ��ؼ� ������ ����, ��ġ�� ����� ����
        }
        return objs;                                // ������ �� ����
    }


    /// <summary>
    /// ������ id�� ������ ����
    /// </summary>
    /// <param name="id">������ ������</param>
    /// <returns>������ ������</returns>
    public static GameObject MakeItem(int id)
    {
        if (id < 0)
        {
            return null;
        }
        return MakeItem((ItemIDCode)id);
    }

    /// <summary>
    /// ������ ���̵� �̿��� Ư�� ��ġ�� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="id">������ ������ ���̵�</param>
    /// <param name="position">������ ��ġ</param>
    /// <returns>������ ������</returns>
    public static GameObject MakeItem(int id, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem(id);      // �����
        if (randomNoise)                    // ��ġ�� �������� ���ϸ�
        {
            // insideUnitCircle : ������ 1¥�� ���� �׸��� �� �ȿ� ���� ��ǥ���� �����ش�.
            Vector2 noise = Random.insideUnitCircle * 0.5f; // ������ 0.5�� ���� ���ʿ��� ������ ��ġ ����
            position.x = noise.x;           // ���� ������ ��ġ�� �Ķ���ͷ� ���� ���� ��ġ�� �߰�
            position.y = noise.y;
        }
        obj.transform.position = position;  // ��ġ����
        return obj;
    }

    /// <summary>
    /// ������ �ڵ带 �̿��ؼ� �������� �ѹ��� ������ �����ϴ� �Լ� 
    /// </summary>
    /// <param name="id">������ �������� ������ ���̵�</param>
    /// <param name="count">������ ����</param>
    /// <returns>������ �����۵��� ��� �迭</returns>
    public static GameObject[] MakeItems(int id, int count)
    {
        GameObject[] objs = new GameObject[count];  // �迭 �����
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(id);               // count��ŭ �ݺ��ؼ� ������ ����
        }
        return objs;                                // ������ �� ����
    }

    /// <summary>
    /// �ڵ带 �̿��� Ư�� ��ġ�� �������� �ѹ��� ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="id">������ �������� ������ ���̵�</param>
    /// <param name="count">������ ����</param>
    /// <param name="position">������ ���� ��ġ</param>
    /// <param name="randomNoise">��ġ�� �������� ������ ����. true�� �ణ�� �������� ���Ѵ�. �⺻���� false</param>
    /// <returns></returns>
    public static GameObject[] MakeItems(int id, int count, Vector3 position, bool randomNoise = false)
    {
        GameObject[] objs = new GameObject[count];  // �迭 �����
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(id, position, randomNoise);               // count��ŭ �ݺ��ؼ� ������ ����, ��ġ�� ����� ����
        }
        return objs;                                // ������ �� ����
    }

}
