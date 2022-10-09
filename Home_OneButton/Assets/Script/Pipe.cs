using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float minHeight; // ���� �ּ� ����

    public float maxHeight; // ���� �ִ� ����

    Rigidbody2D rigid;      // ������ٵ� ������Ʈ

    public int point = 10;

    // �ּ� ���� ~ �ִ� ���� ������ �������� RandomHeight�� �־���
    public float RandomHeight { get => Random.Range(minHeight, maxHeight); }    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        Vector2 pos = Vector2.up * RandomHeight;        // Vector2 Pos�� Vector2.up(Y��) * RandomHeight(���� ����)�� ���� �־��ش�.
        transform.Translate(pos);                       // ������ �� ù ��ġ�� �������� �����Ѵ�.

    }

    public void MoveLeft(float moveDelta)  // public ���� �����ؼ� �Ű������� �޾� �� �� �ֵ��� �Ѵ�.
    {
        // ���� ��ġ���� moveDelta��ŭ �������� �̵�
        rigid.MovePosition(rigid.position + moveDelta * Vector2.left);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JumpMove Bird = collision.GetComponent<JumpMove>();     // �÷��̾� Bird�� ������ ������Ʈ���� ������
            if (Bird != null && !Bird.IsDead)
            {

            }

        }
    }
}
