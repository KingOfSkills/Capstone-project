using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    //private Vector3 startPosition;
    //private float distanceCanTravel = 50f;
    private float speed = 10f;
    private int damage = 2;
    private float bulletLifeTime = 5f;

    private void Awake()
    {
        //startPosition = transform.position;
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        Move();
        //CheckTravelDistance();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //private void CheckTravelDistance()
    //{
    //    var distanceTraveled = (transform.position - startPosition).magnitude;
    //    if (distanceTraveled > distanceCanTravel)
    //    {
    //        // Boss.shotsFired++;
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {   // Has PlayerController Script
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
