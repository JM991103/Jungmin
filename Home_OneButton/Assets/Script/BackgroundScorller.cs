using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScorller : MonoBehaviour
{
    public float scorlSpeed = 5.0f;

    float width = 7.2f;
    float edgPoint;

    Transform[] bgSlots;

    private void Awake()
    {
        bgSlots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            bgSlots[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        edgPoint = transform.position.x - width * 2.0f;
    }

    private void Update()
    {
        foreach(var solt in bgSlots)
        {
            solt.Translate(scorlSpeed * Time.deltaTime * -transform.right);
            if (solt.position.x < edgPoint)
            {
                solt.Translate(width * bgSlots.Length * transform.right);
            }
        }
    }


}
