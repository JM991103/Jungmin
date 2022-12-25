using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���� ���� Ŭ����. ���丮 ���ϰ� �̱��� ���� ����
/// </summary>
public class SlimeFactory : Singleton<SlimeFactory>
{
    /// <summary>
    /// ������ �������� ������
    /// </summary>
    public GameObject slimePrefab;

    /// <summary>
    /// ó���� ������ �������� ����. �ѹ��� ���� �� �ִ� �������� �ִ� ������ ũ�� �ϴ� ���� ����.
    /// </summary>
    public int poolSize = 128;

    /// <summary>
    /// ������ �������� ������ �迭
    /// </summary>
    Slime[] pool;

    /// <summary>
    /// ���� ����� �� �ִ� �����ӵ��� ť
    /// </summary>
    Queue<Slime> readyQueue;

    /// <summary>
    /// �� �ε� ���Ŀ� ȣ�� �Ǵ� �Լ�
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();  // ����� ��¿�(��� ��� ����)

        pool = new Slime[poolSize];                 // Ǯ �迭 ����(poolSize��ŭ)
        readyQueue = new Queue<Slime>(poolSize);    // ����ť ����(poolSize��ŭ Capaticy Ȯ��)

        for (int i = 0; i < poolSize; i++)          // poolSize��ŭ �ݺ�
        {
            GameObject obj = Instantiate(slimePrefab, this.transform);  // ������ �����ϰ� ���丮�� �ڽ����� ����
            obj.name = $"Slime_{i}";                                    // �̸� �缳��
            Slime slime = obj.GetComponent<Slime>();                    // Slime ������Ʈ ã�Ƽ�
            slime.onDisable += () =>
            {
                readyQueue.Enqueue(slime);  // ������ ���� ������Ʈ�� disable�� �� ����ť�� �ǵ�����
            };
            pool[i] = slime;        // Ǯ�� ������ ������ ����
            obj.SetActive(false);   // ������ ���� ������Ʈ ��Ȱ��ȭ
        }
    }

    /// <summary>
    /// Ǯ���� ������ �ϳ��� ������ �ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public Slime GetSlime()
    {
        // Ǯ���� ����� �� �ִ� �������� �ִ��� Ȯ��
        if (readyQueue.Count > 0)
        {
            // ���� ����� �� �ִ� �������� �ִ� ����
            Slime slime = readyQueue.Dequeue(); // ť���� �ϳ� ������
            slime.gameObject.SetActive(true);   // Ȱ��ȭ ��Ų ��
            return slime;                       // Ȱ��ȭ ��Ų �������� ����
        }
        else
        {
            // ���� ����� �� �ִ� �������� ���� ���� => Ǯ�� ũ�⸦ 2��� �ø��� �����ӵ� �߰�
            int newSize = poolSize * 2;                 // ��ũ�⸦ ���� ũ���� 2��� ����
            Slime[] newpool = new Slime[newSize];       // ��Ǯ�� ���� Ǯ�� 2��� ����
            for (int i = 0; i < poolSize; i++)
            {
                newpool[i] = pool[i];                   // ��Ǯ�� ���� Ǯ�� �ִ� �����ӵ� ���� ����
            }
            for (int i = poolSize; i < newSize; i++)    // ��Ǯ�� ����� �� ������ ���� �߰�(Insitialize�� ���� ����)
            {
                GameObject obj = Instantiate(slimePrefab, this.transform);
                obj.name = $"Slime_{i}";
                Slime slime = obj.GetComponent<Slime>();
                slime.onDisable += () =>
                {
                    readyQueue.Enqueue(slime);
                };
                newpool[i] = slime;                     // ��Ǯ�� ���ʿ� �߰��Ѵٴ� �͸� �ٸ�
                obj.SetActive(false);
            }
            pool = newpool;                             // ��Ǯ�� Ǯ�� ����
            poolSize = newSize;                         // ��ũ��� Ǯũ�� ����

            return GetSlime();                          // �� �� �ϳ� ������ �����ϱ�
        }
    }
}
