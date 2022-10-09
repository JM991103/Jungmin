using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float minHeight; // 랜덤 최소 높이

    public float maxHeight; // 랜덤 최대 높이

    Rigidbody2D rigid;      // 리지드바디 컴포넌트

    public int point = 10;

    // 최소 높이 ~ 최대 높이 까지의 랜덤값을 RandomHeight애 넣어줌
    public float RandomHeight { get => Random.Range(minHeight, maxHeight); }    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        Vector2 pos = Vector2.up * RandomHeight;        // Vector2 Pos에 Vector2.up(Y축) * RandomHeight(랜덤 높이)의 값을 넣어준다.
        transform.Translate(pos);                       // 시작할 때 첫 위치를 랜덤으로 지정한다.

    }

    public void MoveLeft(float moveDelta)  // public 으로 설정해서 매개변수를 받아 올 수 있도록 한다.
    {
        // 현재 위치에서 moveDelta만큼 왼쪽으로 이동
        rigid.MovePosition(rigid.position + moveDelta * Vector2.left);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JumpMove Bird = collision.GetComponent<JumpMove>();     // 플레이어 Bird와 부착된 컴포넌트들을 가져옴
            if (Bird != null && !Bird.IsDead)
            {

            }

        }
    }
}
