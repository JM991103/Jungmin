using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpMove : MonoBehaviour
{
    PlayerInput player;     // ����Ƽ���� ���� ��ǲ �׼��� player�� ����
    Rigidbody2D rigid;      // ������ٵ� ����

    [Range(1,10)]
    public float jumpPower = 5.0f;
    public float pitchMaxAngle = 45.0f;

    bool isDead = false;

    private void Awake()
    {
        player = new PlayerInput();     // player�� ���ο� �÷��̾� ��ǲ �׼��� �ν��Ͻ� ��Ŵ
        rigid = GetComponent<Rigidbody2D>();    // ���� �����忡 ������ٵ� ������Ʈ�� �־���
    }

    private void FixedUpdate()      // ���� ������Ʈ �ֱ� ���� ����
    {
        if (!isDead)
        {
            // Mathf.Clamp(�ڽ�, �ּҰ�, �ִ밪)�� �Է¹ް� �ִ� ���� �������� �ִ밪, �ּ� ���� �������� �ּҰ� 
            // �ּҰ��� �ִ밪 ������ ���� �ڽ��� ���� ��ȯ. ��ȯ ���� ���� VelocityY�� ���� �־���
            // rigid.velocity.y�� ���� +6�� ������ �ִ� �� jumpPower(5)�� ���� 5�� ��ȯ��
            float VelocityY = Mathf.Clamp(rigid.velocity.y, -jumpPower, jumpPower);
            // VelocityY(5) / jump(5) = 1 * pitchMaxAngle(����) 
            // ���� -1�̸� -45��, +1�̸� +45�� 0�̸� 0���� �ȴ�.
            float dir = (VelocityY / jumpPower) * pitchMaxAngle;

            rigid.MoveRotation(dir);

        }
    }

    private void OnEnable()
    {
        player.BirdPlayer.Enable();         // ��ǲ �׼����� �Է��� ���� �� �ְ� ����
        player.BirdPlayer.Jump.performed += OnJump;     // ��ǲ �׼ǿ� ������ birdPlayer -> Jump�� �ִ� �׼ǵ��� performed(�Ϻ��ϰ� ������ ��) OnJump ����
    }

    private void OnJump(InputAction.CallbackContext _)
    {
        rigid.velocity = Vector2.up * jumpPower;
        // ������ �ٵ� �ִ� Velocity�� ���� Vector2.up(���� ����) ���� jumpPower ��ŭ �ش�.

        //rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        // Vector2�� ���� ���� ���� jumpPower��ŭ ������ ���� ForceMode(���� ���) Impulse(ª�� ������ ��)�� �ش�.
        // ���������� AddForce�� �ְ� �Ǹ� ���� �������� ���� �þ�� �ȴ�. (Rigidbdy ������ ����)
    }

    private void OnDisable()
    {
        player.BirdPlayer.Jump.performed -= OnJump;     // ����
        player.BirdPlayer.Disable();        // ��ǲ �׼����� �Է��� ���� �� ���� ���� ��
    }


}
