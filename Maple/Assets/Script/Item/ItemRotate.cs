using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    //public float rotateSpeed;       // 오브젝트의 회전 속도
    public float minHeight;         // 오브젝트의 가장 낮은 높이
    public float maxHeight;         // 오브젝트의 가장 높은 높이

    float timeElapsed = 0.0f;
    float halfdiff;
    Vector3 newPosition;

    private void Start()
    {
        timeElapsed = 0.0f;

        // newPosition에 자기 자신의 위치를 넣고
        newPosition = transform.position;
        // newPosition의 y 값을 가장 낮은 높이로 설정
        newPosition.y = minHeight;
        // 오브젝트의 위치를 newPosition으로 설정
        transform.position = newPosition;   

        // 캐싱용 계산 결과 저장
        halfdiff = 0.5f * (maxHeight - minHeight);
    }

    private void Update()
    {
        //transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        // 시간은 계속 누적 시킴
        timeElapsed += Time.deltaTime * 3.0f;
        newPosition.x = transform.parent.position.x;    // 부모의 x,z 위치는 계속 적용
        newPosition.z = transform.parent.position.z;
        // 높이 값을 위 아래로 움직일 수 있도록 cos 그래프를 이용해 계산
        newPosition.y = minHeight + (1 - Mathf.Cos(timeElapsed)) * halfdiff;
        // 계산이 끝난 newPosition으로 위치 옮기기
        transform.position = newPosition;
    }

}
