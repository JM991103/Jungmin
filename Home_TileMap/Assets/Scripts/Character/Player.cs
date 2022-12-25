using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �̵� �ӵ�
    /// </summary>
    public float speed = 3.0f;

    /// <summary>
    /// �ִϸ����� ������Ʈ 
    /// </summary>
    Animator anim;

    /// <summary>
    /// ������ٵ� ������Ʈ
    /// </summary>
    Rigidbody2D rigid;

    /// <summary>
    /// ��ǲ �ý��ۿ� ��ǲ �׼Ǹ� Ŭ���� ��ü
    /// </summary>
    PlayerInputActions inputActions;

    /// <summary>
    /// ���� �Էµ� �̵� ����
    /// </summary>
    Vector2 inputDir;

    /// <summary>
    /// ���� ���Ŀ� ���������� �ӽ� ������ ���� ���� �Է� ����
    /// </summary>
    Vector2 oldInputDir;

    /// <summary>
    /// ���� �̵� ������ ǥ���ϴ� ����
    /// </summary>
    bool isMove = false;

    /// <summary>
    /// ���� ������ �߽�(��)
    /// </summary>
    Transform attackAreaCenter;

    List<Slime> attackTarget;

    private void Awake()
    {
        // ������Ʈ ã��
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        // ��ü ����
        inputActions = new PlayerInputActions();

        attackTarget = new List<Slime>(2);
        attackAreaCenter = transform.GetChild(0);
        AttackArea attackArea = attackAreaCenter.GetComponentInChildren<AttackArea>();
        attackArea.onTarget += (slime) =>
        {
            attackTarget.Add(slime);
            slime.ShowOutline(true);
        };

        attackArea.onUnTarget += (slime) =>
        {
            attackTarget.Remove(slime);
            slime.ShowOutline(false);
        };

    }

    private void OnEnable()
    {
        // �Է� ����
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnStop;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        // �Է� ���� ����
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnStop;
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // �̵� ó��
        rigid.MovePosition(rigid.position + Time.deltaTime * speed * inputDir);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // �̵� �Է��� ������ ��
        inputDir = context.ReadValue<Vector2>();    // �Է� �̵� ���� �����ϰ�
        oldInputDir = inputDir;                     // ���� ������ ���� �Է� �̵� ���� ����
        anim.SetFloat("InputX", inputDir.x);        // �ִϸ����� �Ķ���� ����
        anim.SetFloat("InputY", inputDir.y);

        if (inputDir.y > 0)
        {
            // ���� �� ��
            attackAreaCenter.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (inputDir.y < 0)
        {
            // �Ʒ��� �� ��
            attackAreaCenter.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (inputDir.x > 0)
        {
            // ���������� �� ��
            attackAreaCenter.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (inputDir.x < 0)
        {
            // �������� �� ��
            attackAreaCenter.rotation = Quaternion.Euler(0, 0, 270);
        }
        else
        {
            // ���� �� ����.
            attackAreaCenter.rotation = Quaternion.Euler(0, 0, 0);
        }

        isMove = true;                              // �̵� �Ѵٰ� ǥ���ϰ�
        anim.SetBool("IsMove", isMove);             // �̵� �ִϸ��̼� ���
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        isMove = false;                 // �̵� ���� ǥ���ϰ�
        anim.SetBool("IsMove", isMove); // Idle �ִϸ��̼� ���
        inputDir = Vector2.zero;        // �Է¹��� ����
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        oldInputDir = inputDir;         // ���� ������ ���� �Է� �̵� ���� ����        
        inputDir = Vector2.zero;        // �Է� �̵� ���� �ʱ�ȭ
        anim.SetTrigger("Attack");      // ���� �ִϸ��̼� ����
    }

    /// <summary>
    /// ���� �ִϸ��̼� state�� ���� �� ����Ǵ� �Լ�
    /// </summary>
    public void RestoreInputDir()
    {

        if (isMove == true)             // �̵� ���� ����
        {
            inputDir = oldInputDir;     // �Է� �̵� ���� ���� 
        }
    }
}
