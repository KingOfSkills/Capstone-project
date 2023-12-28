using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    private float leftBoundary = -6;
    private float rightBoundary = 6;

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //transform.position = player.transform.position + offset;

        //if (transform.position.x < -20)
        //{
        //    transform.position = new Vector3(-20, transform.position.y, transform.position.z);
        //}
        //else if (transform.position.x > 20)
        //{
        //    transform.position = new Vector3(20, transform.position.y, transform.position.z);
        //}

        if (leftBoundary < player.transform.position.x && player.transform.position.x < rightBoundary)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
