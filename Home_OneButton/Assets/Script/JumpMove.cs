using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpMove : MonoBehaviour
{
    PlayerInput player;     // 유니티에서 만든 인풋 액션을 player로 선언
    Rigidbody2D rigid;      // 리지드바디를 선언

    [Range(1, 10)]
    public float jumpPower = 5.0f;
    public float pitchMaxAngle = 45.0f;

    public Action OnDead;

    bool isDead = false;

    public bool IsDead { get => isDead; }

    private void Awake()
    {
        player = new PlayerInput();     // player에 새로운 플레이어 인풋 액션을 인스턴스 시킴
        rigid = GetComponent<Rigidbody2D>();    // 만든 리지드에 리지드바디 컴포넌트를 넣어줌
    }

    private void FixedUpdate()      // 물리 업데이트 주기 마다 실행
    {
        if (!isDead)
        {
            // Mathf.Clamp(자신, 최소값, 최대값)을 입력받고 최대 보다 높아지면 최대값, 최소 보다 낮아지면 최소값 
            // 최소값과 최대값 사이의 값은 자신의 값을 반환. 반환 받은 값을 VelocityY에 값을 넣어줌
            // rigid.velocity.y의 값이 +6이 나오면 최대 값 jumpPower(5)에 막혀 5가 반환됨
            float VelocityY = Mathf.Clamp(rigid.velocity.y, -jumpPower, jumpPower);
            // VelocityY(5) / jump(5) = 1 * pitchMaxAngle(각도) 
            // 따라서 -1이면 -45도, +1이면 +45도 0이면 0도가 된다.
            float dir = (VelocityY / jumpPower) * pitchMaxAngle;

            rigid.MoveRotation(dir);

        }
    }

    private void OnEnable()
    {
        player.BirdPlayer.Enable();         // 인풋 액션으로 입력을 받을 수 있게 해줌
        player.BirdPlayer.Jump.performed += OnJump;     // 인풋 액션에 만들어둔 birdPlayer -> Jump에 있는 액션들을 performed(완벽하게 눌렸을 때) OnJump 실행
    }

    private void OnJump(InputAction.CallbackContext _)
    {
        rigid.velocity = Vector2.up * jumpPower;
        // 리지드 바디에 있는 Velocity의 값을 Vector2.up(위쪽 방향) 으로 jumpPower 만큼 준다.

        //rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        // Vector2의 위쪽 방향 으로 jumpPower만큼 곱해준 다음 ForceMode(포스 모드) Impulse(짧은 순간의 힘)를 준다.
        // 연속적으로 AddForce를 주게 되면 점점 가해지는 힘이 늘어나게 된다. (Rigidbdy 물리력 적용)
    }

    private void OnDisable()
    {
        player.BirdPlayer.Jump.performed -= OnJump;     // 종료
        player.BirdPlayer.Disable();        // 인풋 액션으로 입력을 받을 수 없게 종료 함
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die(collision.GetContact(0));
    }

    private void Die(ContactPoint2D contact)
    {
        Vector2 dir = (contact.point - (Vector2)transform.position).normalized;
        // 새가 충돌한 지점으로 가는 방향
        Vector2 reflect = Vector2.Reflect(dir, contact.normal);
        // dir이 반사된 벡터
        rigid.velocity = reflect * 10.0f + Vector2.left * 5.0f;
        // 반사되는 방향에 왼쪽으로 가는 힘 추가
        rigid.angularVelocity = 1000.0f;
        // 회전 추가 (초당 1000도)

        if (!isDead)
        {
            OnDead?.Invoke();               // 델리게이트 연결
            player.BirdPlayer.Enable();     // 입력 시스템 막기
            isDead = true;                  // 죽었다고 표시
        }
    }
}
