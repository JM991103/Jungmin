using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 700;

    private void Update()
    {
        transform.localPosition += Vector3.down * noteSpeed * Time.deltaTime;
    }
}
