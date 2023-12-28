using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour, IDamagable
{
    [SerializeField] private float moveSpeed = .5f;

    private float countdownToExplode = 1f;
    private float radius = 5f;
    private float explosionForce = 350f;
    private int damage = 5;

    private int gold;
    private int currentHealth = 25;

    private bool isAlive;

    private void Start()
    {
        gold = Random.Range(10, 15);
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            Explode();
        }
    }

    public void Explode()
    {
        countdownToExplode -= Time.deltaTime;
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        if (countdownToExplode <= 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // Get all colliders in Explosion
            foreach (Collider nearbyObject in colliders)
            {
                if (nearbyObject.TryGetComponent(out Rigidbody rigidbody))
                {   // Has Rigidbody
                    rigidbody.AddExplosionForce(explosionForce, transform.position, radius);
                }
                if (nearbyObject.TryGetComponent(out PlayerController player))
                {
                    player.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
            FindObjectOfType<ShopManager>().UpdateMoney(gold);
            SpawnManager.zombiesAlive--;
            //Debug.Log(SpawnManager.zombiesAlive + " zombies left");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;
            GetComponentInChildren<ZombieVisual>().Flash();
            if (currentHealth <= 0)
            {
                isAlive = false;
            }
        }
    }
}
