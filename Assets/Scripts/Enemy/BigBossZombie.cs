using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBossZombie : MonoBehaviour, IDamagable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private StatusEffectSO statusEffectSO;
    private PlayerController targetPlayer;
    private int gold;
    private int currentHealth = 50;

    public int damage = 10;
    private float attackRate = 1f;
    private float timeTillAttack = 0f;
    public bool canAttack = false;

    private WallHealth wallHealth;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        gold = Random.Range(10, 15);
        //moveSpeed = Random.Range(2.5f, 4.5f);
    }

    private void Start()
    {
        targetPlayer = PlayerController.Instance;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
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
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        navMeshAgent.SetDestination(targetPlayer.transform.position);
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponentInChildren<ZombieVisual>().Flash();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<ShopManager>().UpdateMoney(gold);
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
}
