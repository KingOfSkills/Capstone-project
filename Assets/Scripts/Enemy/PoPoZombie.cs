using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoPoZombie : MonoBehaviour, IDamagable
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawnPosition;

    private float turnSpeed = 1.5f;
    private float fireRate = 1f;

    private int gold;
    private int currentHealth = 10;

    private void Start()
    {
        gold = Random.Range(6, 9);
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        Fire();
        Move();
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed).normalized;
    }

    private void Fire()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            Instantiate(bullet, bulletSpawnPosition.transform.position, transform.rotation);
            fireRate = 1f;
        }
    }

    private void Move()
    {
        if (transform.position.z > 25.5)
        {
            Vector3 moveDir = new Vector3(0, 0, -1); // Moves down
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponentInChildren<ZombieVisual>().Flash();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<ShopManager>().UpdateMoney(gold);
            SpawnManager.zombiesAlive--;
            //Debug.Log(SpawnManager.zombiesAlive + " zombies left");
        }
    }
}
