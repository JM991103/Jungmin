using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float speed = 3.0f;
    //Transform target;
    Vector3 offset;
    Player player;

    private void Start()
    {
        player = GameManager.Inst.Player;
        //target = player.transform.position;
        offset = transform.position - player.transform.position;

        transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        if (player.transform.position.x > -5 && player.transform.position.x < 33 && player.transform.position.y > -6.5 && player.transform.position.y < 6.5)
        {
            //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.fixedDeltaTime);
            transform.position = player.transform.position;
            
        }
    }
}
