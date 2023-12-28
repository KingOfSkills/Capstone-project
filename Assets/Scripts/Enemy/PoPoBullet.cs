using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoPoBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private int damage = 1;

    private float bulletLifeTime = 10f;

    private void Awake()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {   // Has PlayerController Script
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
