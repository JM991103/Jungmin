using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    TalkManager talkManager;
    PlayerInputAction inputActions;
    Animator anim;
    Rigidbody2D rigid;

    Vector2 inputDir;
    Vector2 oldInputDir;

    Vector3 dirVec;
    GameObject scanObj;

    public float moveSpeed = 3.0f;
    bool isMove = false;

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
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * inputDir);

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObj = rayHit.collider.gameObject;
        }
        else
        {
            scanObj = null;
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
        if (scanObj != null)
        {
            talkManager.Action(scanObj);            
        }     
    }

    public void OnMoveController(bool isMove)
    {
        if (isMove)
        {
            inputActions.Player.Move.Enable();
        }
        else
        {
            inputActions.Player.Move.Disable();
        }
    }

}
