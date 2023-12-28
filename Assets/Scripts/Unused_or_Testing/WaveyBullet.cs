using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 12.5f;
    [SerializeField] private int damage = 10;

    [SerializeField] private float bulletLifeTime = 6f;
    [SerializeField] private float waveRadius = 5f;

    private void Awake()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.position += transform.right * Mathf.Sin(transform.position.z) * waveRadius * Time.deltaTime;
        //Debug.Log(Mathf.Sin(transform.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
