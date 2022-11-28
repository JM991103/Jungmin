using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    //public float rotateSpeed;       // ������Ʈ�� ȸ�� �ӵ�
    public float minHeight;         // ������Ʈ�� ���� ���� ����
    public float maxHeight;         // ������Ʈ�� ���� ���� ����

    float timeElapsed = 0.0f;
    float halfdiff;
    Vector3 newPosition;

    private void Start()
    {
        timeElapsed = 0.0f;

        // newPosition�� �ڱ� �ڽ��� ��ġ�� �ְ�
        newPosition = transform.position;
        // newPosition�� y ���� ���� ���� ���̷� ����
        newPosition.y = minHeight;
        // ������Ʈ�� ��ġ�� newPosition���� ����
        transform.position = newPosition;   

        // ĳ�̿� ��� ��� ����
        halfdiff = 0.5f * (maxHeight - minHeight);
    }

    private void Update()
    {
        //transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        // �ð��� ��� ���� ��Ŵ
        timeElapsed += Time.deltaTime * 3.0f;
        newPosition.x = transform.parent.position.x;    // �θ��� x,z ��ġ�� ��� ����
        newPosition.z = transform.parent.position.z;
        // ���� ���� �� �Ʒ��� ������ �� �ֵ��� cos �׷����� �̿��� ���
        newPosition.y = minHeight + (1 - Mathf.Cos(timeElapsed)) * halfdiff;
        // ����� ���� newPosition���� ��ġ �ű��
        transform.position = newPosition;
    }

}
