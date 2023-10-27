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

    public bool isMerge;

    public bool isAttach;

    public GameManager manager;

    public Rigidbody2D rigid;
    CircleCollider2D circle;
    Animator anim;

    float deadTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetInteger("Level", level);
    }

    private void OnDisable()
    {
        // 동글 속성 초기화
        level = 0;
        isDrag = false;
        isMerge = false;
        isAttach = false;

        // 동글 트랜스폼 초기화
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.zero;

        // 동글 물리 초기화
        rigid.simulated = false;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;
        circle.enabled = true;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("AttachRoutine");
    }

    IEnumerator AttachRoutine()
    {
        if (isAttach)
        {
            yield break;
        }

        isAttach = true;
        manager.sfxPlay(GameManager.sfx.Attach);

        yield return new WaitForSeconds(2.0f);

        isAttach = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dongle")
        {
            Dongle other = collision.gameObject.GetComponent<Dongle>();

            if (level == other.level && !isMerge && !other.isMerge && level < 10)
            {
                // 동글 합치기 로직
                // 나와 상대편 위치 가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                // 1. 내가 아래에 있을 때
                // 2. 동일한 높이일 때, 내가 오른쪽에 있을 때
                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    // 상대방은 숨기기
                    other.Hide(transform.position);
                    // 나는 레벨업
                    LevelUp();
                }
            }
        }
    }

    /// <summary>
    /// 동글이 2개가 만나면 만나는 쪽은 사라지는 함수
    /// /// </summary>
    /// <param name="targetPos"></param>
    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        circle.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    /// <summary>
    /// 상대 위치를 내 위치로 다가오게하는 코루틴
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;

        while (frameCount < 5)
        {
            frameCount++;
            if (targetPos != Vector3.up * 100)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, 1.0f);
            }
            else if (targetPos == Vector3.up * 100)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
            }

            yield return null;
        }

        manager.score += (int)Mathf.Pow(2, level);

        isMerge = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 레벨업 함수
    /// </summary>
    void LevelUp()
    {
        isMerge = true;

        rigid.velocity = Vector2.zero; // 위치 고정
        rigid.angularVelocity = 0f;    // 회전 고정

        StartCoroutine(LevelUpRoutine());
    }

    /// <summary>
    /// 동글이 2개가 합쳐지면 새로운 동글이가 나타나는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.01f);

        anim.SetInteger("Level", level + 1);
        manager.sfxPlay(GameManager.sfx.LevelUp);

        yield return new WaitForSeconds(0.01f);

        level++;

        isMerge = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            deadTime += Time.deltaTime;

            if (deadTime > 1)
            {
                manager.GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            deadTime = 0;
        }
    }
}
