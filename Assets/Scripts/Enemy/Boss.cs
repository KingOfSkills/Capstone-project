using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable
{
    [SerializeField] private GameObject misslesPF;
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossUITemplate;
    private BossUI bossUI;
    private int phaseNumber;
    private bool isAtDestination;
    // TakeDamage
    private bool canBeDamaged = false;
    private int maxHealth = 150;
    private int currentHealth;
    // Move
    private float moveSpeed = 1f;
    private float turnSpeed = 5f;
    [SerializeField] private Vector3[] movePositions;
    //[SerializeField] private Transform[] movePositions;
    private Vector3 destination;
    // FireBullet
    [SerializeField] private GameObject bulletSpawnPosition;
    private float fireRate = .1f;
    private int bullets = 0;
    private float delayFireTime = 2f;
    private bool canFireBullet = false;
    private float timeTillFire = 0f;
    private int maxShots = 10;
    // FireMissle
    private float launchRate = .25f;
    private int missles = 0;
    private float delayLaunchTime = 20f;
    private bool canLaunchMissle = false;
    private float timeTillLaunch = 0f;
    private int maxMissles = 3;
    [SerializeField] private GameObject missleSpawnPosition;
    // SpawnReinforcements
    //private bool canCallReinforcement;

    //private State gunState;
    //private State missleState;

    //public enum State
    //{
    //    Firing,
    //    CoolingDown,
    //}

    private void Awake()
    {
        currentHealth = maxHealth;
        isAtDestination = false;
        canBeDamaged = false;
    }

    private void Start()
    {
        player = PlayerController.Instance.GetGameObject();
        phaseNumber = 1;
        // Set points to move to
        for (int i = 0; i < 3; i++)
        {
            movePositions[i] = GameObject.Find("BossMovePoint" + i.ToString()).transform.position;
            //Debug.Log("Vector Posistion " + i + " = " + movePositions[i]);
        }
        // Make boss move
        destination = movePositions[0];
        // Set UI to right values
        GameObject bossUIGameObject = Instantiate(bossUITemplate);
        bossUI = bossUIGameObject.GetComponent<BossUI>();
        bossUI.InitializeHealth(maxHealth);
    }

    private void Update()
    {
        if (GameManager.isBossFight == true)
        {
            if (!isAtDestination)
            {
                MoveToDestination();
            }
            if (GameManager.isBossFight == true)
            {
                LookAtTarget();
            }
            // Check to Fire Bullet
            if (canFireBullet) // && gunState == State.Firing)
            {
                FireBullet();
            }
            else
            //if (gunState == State.CoolingDown)
            {
                DelayFireBullet();
            }
            // Check to Launch Missle
            if (canLaunchMissle) // && missleState == State.Firing)
            {
                LaunchMissle();
            }
            else // (missleState == State.CoolingDown)
            {
                DelayLaunchMissle();
            }
        }
        else
        {
            MoveToDestination();
        }
    }

    private void StartBossFight()
    {
        //bossUI.SetActive(true);
        isAtDestination = true;
        GameManager.isBossFight = true;
        canBeDamaged = true;
        canFireBullet = true;
        canLaunchMissle = true;
        moveSpeed = 5f;
    }

    private void MoveToDestination() // Vector3 destination)
    {
        var direction = destination - transform.position;
        transform.position += moveSpeed * Time.deltaTime * direction.normalized;
        //Debug.Log("Boss Posistion = " + transform.position + "\nDestination " + destination);
        if(destination.x - .1f < transform.position.x && transform.position.x < destination.x + .1f)
        {
            if (destination.z - .1f < transform.position.z && transform.position.z < destination.z + .1f)
            {
                isAtDestination = true;
                // Start boss Fight if it is first time reaching destination
                if (GameManager.isBossFight == false)
                {
                    StartBossFight();
                    CallSpawnReinforcement(phaseNumber);
                    //canCallReinforcement = false;
                }
                var randomNumber = Random.Range(0 , movePositions.Length);
                destination = movePositions[randomNumber];
                isAtDestination = false;
            }
        }
    }

    private void LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed).normalized;
        //Debug.Log("Looking at Player");
    }

    public void TakeDamage(int dmg)
    {
        if (canBeDamaged)
        {
            currentHealth -= dmg;
            bossUI.UpdateCurrentHealth(currentHealth);
            // Debug.Log(dmg);
            GetComponentInChildren<ZombieVisual>().Flash();
            if (currentHealth <= maxHealth / 2 && phaseNumber < 2)
            {
                phaseNumber = 2;
                moveSpeed *= 2;
                maxShots *= 2;
                maxMissles *= 2;
                CallSpawnReinforcement(phaseNumber);
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void FireBullet()
    {
        timeTillFire += Time.deltaTime;
        if (timeTillFire > fireRate && bullets < maxShots) // && canFireBullet)
        {
            Instantiate(bulletPF, bulletSpawnPosition.transform.position, transform.rotation);
            bullets++;
            timeTillFire = 0;
            if (bullets == maxShots)
            {
                canFireBullet = false;
                //gunState = State.CoolingDown;
                delayFireTime = 2f;
            }
        }
    }

    public void DelayFireBullet()
    {
        delayFireTime -= Time.deltaTime;
        if (delayFireTime <= 0)
        {
            canFireBullet = true;
            //gunState = State.Firing;
            bullets = 0;
        }
    }

    public void LaunchMissle()
    {
        timeTillLaunch += Time.deltaTime;
        if (timeTillLaunch > launchRate && missles < maxMissles && canLaunchMissle)
        {
            Instantiate(misslesPF, missleSpawnPosition.transform.position, misslesPF.transform.rotation);
            missles++;
            timeTillLaunch = 0;
            if (missles == maxMissles)
            {
                canLaunchMissle = false;
                //missleState = State.CoolingDown;
                delayLaunchTime = 10f;
            }
        }
    }

    public void DelayLaunchMissle()
    {
        delayLaunchTime -= Time.deltaTime;
        if (delayLaunchTime <= 0)
        {
            canLaunchMissle = true;
            //missleState = State.Firing;
            missles = 0;
        }
    }

    private void CallSpawnReinforcement(int phase)
    {
        SpawnManager.Instance.SpawnReinforcement(phase);
    }

    private void Die()
    {
        SpawnManager.zombiesAlive--;
        GameManager.Instance.WinGame();
        Destroy(bossUI.gameObject);
        Destroy(gameObject);
    }
}
