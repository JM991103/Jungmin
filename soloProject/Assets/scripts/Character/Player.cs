using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour, Ilogging
{
    PlayerInputAction inputActions; 
    Animator anim;
    Rigidbody2D rigid;

    /// <summary>
    /// talkPanel, talkText 관련 클래스
    /// </summary>
    TalkManager talkManager;

    /// <summary>
    /// 입력받은 플레이어 이동 방향
    /// </summary>
    Vector2 inputDir;
    Vector2 oldInputDir;

    /// <summary>
    /// 플레이어 앞에 무엇이 있는지 확인 할 수 있는 레이캐스트 방향
    /// </summary>
    Vector3 dirVec;

    /// <summary>
    /// 레이캐스트로 확인한 게임 오브젝트
    /// </summary>
    GameObject scanObj;

    /// <summary>
    /// 플레이어 이동 스피드
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// 애니메이션 이동용 파라메터
    /// </summary>
    bool isMove = false;

    /// <summary>
    /// 살아있으면 true 죽으면 false
    /// </summary>
    bool isAlive = true;

    public int attackPower = 1;

    public int AttackPower => attackPower;
    
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        talkManager = FindObjectOfType<TalkManager>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnStop;
        inputActions.Player.Space.performed += OnSpace;
    }

    private void OnDisable()
    {
        inputActions.Player.Space.performed -= OnSpace;
        inputActions.Player.Move.canceled -= OnStop;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // 입력받은 방향으로 moveSpeed만큼 움직이게 한다.
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * inputDir);

        // 씬에서 확인 가능하도록 그리는 Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        // dirVec방향 0.7f거리에 있는 오브젝트의 레이어가 Object인지 확인
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object", "Tree"));

        if (rayHit.collider != null)    // rayHit에 해당하는 콜라이더가 있으면
        { 
            scanObj = rayHit.collider.gameObject;   // 해당 콜라이더의 게임오브젝트 가져오기
        }
        else                            
        {
            scanObj = null;                         // 없으면 null
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        isMove = true;
        anim.SetBool("IsMove", isMove);

        inputDir = context.ReadValue<Vector2>();
        oldInputDir = inputDir;
        anim.SetFloat("InputX", inputDir.x);
        anim.SetFloat("InputY", inputDir.y);

        //dirVec
        if (inputDir.y == 1)
        {
            dirVec = Vector3.up;
        }
        else if(inputDir.y == -1)
        {
            dirVec = Vector3.down;
        }
        else if (inputDir.x == 1)
        {
            dirVec = Vector3.right;
        }
        else if (inputDir.x == -1)
        {
            dirVec = Vector3.left;
        }

    }

    private void OnStop(InputAction.CallbackContext context)
    {
        isMove = false;
        anim.SetBool("IsMove", isMove);
        inputDir = Vector2.zero;
    }

    private void OnSpace(InputAction.CallbackContext _)
    {
        if (scanObj != null)    // scanObj가 null이 아닐때 스페이스바를 누르면
        {
            if (scanObj == scanObj.CompareTag("Object"))
            {
                // 앞에 있는 물체의 게임오브젝트를 Action에게 넘겨준다.
                talkManager.Action(scanObj.transform.gameObject);
            }
            
            if (scanObj == scanObj.CompareTag("Tree"))
            {                
                Debug.Log("앞에 나무가 있다");
                anim.SetTrigger("IsTree");
                Ilogging target = scanObj.gameObject.GetComponent<Ilogging>();
                Attack(target);                
            }
            else if (scanObj == scanObj.CompareTag("Enemy"))
            {
                Debug.Log("앞에 몬스터가 있다");
                anim.SetTrigger("IsAttack");
                Ilogging target = scanObj.gameObject.GetComponent<Ilogging>();
                Attack(target);
            }
        }
    }

    /// <summary>
    /// 플레이어의 이동을 사용 가능/불가 하게 해주는 함수
    /// </summary>
    /// <param name="isMove"></param>
    public void OnMoveController(bool isMove)
    {
        if (isMove)
        {
            inputActions.Player.Move.Enable();  // 이동 연결
        }
        else
        {
            inputActions.Player.Move.Disable(); // 이동 연결 해제
        }
    }

    public void Attack(Ilogging target)
    {
        target?.Defence(AttackPower);
    }

    public void Defence(int damage)
    {
        if (isAlive)
        {

        }
    }
}
