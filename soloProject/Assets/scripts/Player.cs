using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputAction inputActions;
    Animator anim;
    Rigidbody2D rigid;

    Vector2 inputDir;
    Vector2 oldInputDir;

    public float moveSpeed = 3.0f;
    bool isMove = false;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnStop;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnStop;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.Disable();
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * inputDir);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        isMove = true;
        anim.SetBool("IsMove", isMove);

        inputDir = context.ReadValue<Vector2>();
        oldInputDir = inputDir;
        anim.SetFloat("InputX", inputDir.x);
        anim.SetFloat("InputY", inputDir.y);        
        
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        isMove = false;
        anim.SetBool("IsMove", isMove);
        inputDir = Vector2.zero;
    }

}
