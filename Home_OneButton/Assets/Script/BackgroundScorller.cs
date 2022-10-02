using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScorller : MonoBehaviour
{
    public float scorlSpeed = 5.0f;     // 배경화면이 움직일 속도

    float width = 7.2f;     // 배경화면의 가로 길이
    float edgPoint;         // 이동하면서 도착해할 지점

    Transform[] bgSlots;    // 트랜스폼 배열로 변수를 만듬 (배열의 개수가 얼마나될지 정해지지 않음)
    // 스택 영역에 메모리 할당

    private void Awake()
    {
        // 힙 영역에 데이터를 저장할 수 있도록 메모리 할당
        bgSlots = new Transform[transform.childCount];  // bgSlots에 Transform을 자식의 갯수만큼 배열을 넣어준다.

        for (int i = 0; i < transform.childCount; i++)  //i가 자식의 수보다 클때까지 반복하는 반복문
        {
            bgSlots[i] = transform.GetChild(i);         // bgSlots[i]번째 배열에 i번째 자식을 넣어줌
        }

    }

    private void Start()
    {
        edgPoint = transform.position.x - width * 2.0f; //
    }

    private void Update()
    {
        foreach (var slot in bgSlots)
        {
            slot.Translate(scorlSpeed * Time.deltaTime * -transform.right);
            if (slot.position.x < edgPoint)
            {
                slot.Translate(width * bgSlots.Length * transform.right);
            }
        }


    }

}
