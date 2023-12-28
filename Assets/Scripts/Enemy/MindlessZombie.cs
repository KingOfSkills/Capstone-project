using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindlessZombie : MonoBehaviour, IDamagable
{
    [SerializeField] private StatusEffectSO statusEffectSO;
    private float moveSpeed;
    private int gold;
    private int currentHealth = 5;

    public int damage = 5;
    private float attackRate = 1f;
    private float timeTillAttack = 0f;
    public bool canAttack = false;

    private WallHealth wallHealth;

    private void Awake()
    {
        gold = Random.Range(0, 3);
        moveSpeed = Random.Range(.75f, 1.5f);
    }

    private void Update()
    {
        Move();
        //Die();
        if (canAttack)
        {
            DoDamage();
        }
    }

    private void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out PlayerController player))
        {
            player.TakeDamage(damage);

            if (statusEffectSO != null)
            {
                StatusEffect.ApplyBurnEffect(player.GetComponent<IStatusEffectable>(), statusEffectSO);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallHealth wall) && !canAttack)
        {
            //wall.TakeDamage(damage);
            canAttack = true;
            wallHealth = wall;
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
        }
    }

    public void DoDamage()
    {
        timeTillAttack += Time.deltaTime;
        if (timeTillAttack > attackRate)
        {
            wallHealth.TakeDamage(damage);
            timeTillAttack = 0f;
        }
    }

    public StatusEffectSO GetStatusEffectSO()
    {
        return statusEffectSO;
    }
}
