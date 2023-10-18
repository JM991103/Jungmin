using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    /// <summary>
    /// 애니메이션에서 처리할 이미지 레벨
    /// </summary>
    public int level;

    /// <summary>
    /// 마우스가 클릭중이면 true 아니면 false
    /// </summary>
    public bool isDrag;

    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetInteger("Level", level);
    }

    void Update()
    {
        if (isDrag)
        {           
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // x축 경계 설정
            float leftBorder = -4.2f + transform.localScale.x / 2.0f;
            float rightBorder = 4.2f - transform.localScale.x / 2.0f;

            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            mousePos.y = 7.5f;
            mousePos.z = 0.0f;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }        
    }

    /// <summary>
    /// 클릭을 하면 true
    /// </summary>
    public void Drag()
    {
        isDrag = true;
    }

    /// <summary>
    /// 마우스 클릭을 해제하면 false하고 조작하지 못하게 함
    /// </summary>
    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }
}
