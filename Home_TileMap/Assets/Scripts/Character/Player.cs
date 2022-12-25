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
    /// �÷��̾��� ���� ��Ÿ��
    /// </summary>
    public float attackCoolTime = 1.0f;

    /// <summary>
    /// �÷��̾��� ���� �����ִ� ��Ÿ��
    /// </summary>
    float currentAttackCoolTime = 0.0f;

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

    /// <summary>
    /// �÷��̾ �����ϸ� ���� �����ӵ�
    /// </summary>
    List<Slime> attackTarget;

    /// <summary>
    /// ������ȿ�Ⱓ ǥ��. true�� �������� ���� �� �ִ�. false�� �� ���̴� ��Ȳ
    /// </summary>
    bool isAttackValid = false;

    private void Awake()
    {
        // ������Ʈ ã��
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        // ��ü ����
        inputActions = new PlayerInputActions();

        // ���� ��� ����� ����Ʈ ����
        attackTarget = new List<Slime>(2);  // �⺻ ĳ�۽�Ƽ 2���� ����
        attackAreaCenter = transform.GetChild(0);   // ���������� �߽��� ã��
        AttackArea attackArea = attackAreaCenter.GetComponentInChildren<AttackArea>();  // �������� ã��
        attackArea.onTarget += (slime) => 
        {
            // slime�� ���������ȿ� ������ ���� ó��
            attackTarget.Add(slime);    // ����Ʈ�� �߰�
            slime.ShowOutline(true);    // �ƿ����� ǥ��
        };

        attackArea.onTargetRelease += (slime) =>
        {
            // slime�� �������������� ������ ���� ó��
            attackTarget.Remove(slime); // ����Ʈ���� ����
            slime.ShowOutline(false);   // �ƿ����� ����
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

    private void Update()
    {
        // �ƹ� ���� ���� ��� ��Ÿ�� ����
        currentAttackCoolTime -= Time.deltaTime;

        if (isAttackValid && attackTarget.Count > 0)
        {
            foreach(var target in attackTarget)
            {
                target.Die();
            }
        }
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

        isMove = true;                              // �̵� �Ѵٰ� ǥ���ϰ�
        anim.SetBool("IsMove", isMove);             // �̵� �ִϸ��̼� ���

        // �Է� ���⿡ ���� ���������� ��ġ ���� (�߽��� ȸ������ ó��)
        if (inputDir.y > 0)
        {
            // ���� �� �� (��, �Ʒ��� �켱������ ����)
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
            // ���� �� ����. (Ȥ�ó� �; �ۼ��� �ڵ�)
            attackAreaCenter.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        isMove = false;                 // �̵� ���� ǥ���ϰ�
        anim.SetBool("IsMove", isMove); // Idle �ִϸ��̼� ���
        inputDir = Vector2.zero;        // �Է¹��� ����
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (currentAttackCoolTime < 0)
        {
            oldInputDir = inputDir;         // ���� ������ ���� �Է� �̵� ���� ����        
            inputDir = Vector2.zero;        // �Է� �̵� ���� �ʱ�ȭ
            anim.SetTrigger("Attack");      // ���� �ִϸ��̼� ����

            currentAttackCoolTime = attackCoolTime; // ��Ÿ�� ����
        }
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

    /// <summary>
    /// ������ ȿ�������� ���� �� ����Ǵ� �Լ�
    /// </summary>
    public void AttackValid()
    {
        isAttackValid = true;
    }

    /// <summary>
    /// ������ ȿ�������� ���̴� �Ⱓ�� ���� �� ����� �Լ�
    /// </summary>
    public void AttackNotValid()
    {
        isAttackValid = false;
    }
}
