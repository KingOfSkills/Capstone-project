using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStatusEffectable
{   
    public static PlayerController Instance { get; private set; }
    // References
    private Camera mainCamera;
    private Rigidbody rigidBody;
    public int lives = 3;
    // Move
    [SerializeField] private float moveSpeed = 10f;
    private float topBoundary = 22f;
    private float bottomBoundary = 1.5f;
    [SerializeField] private int fallDamage = 1;
    private Vector3 startPosition;
    // Fire
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private GameObject projectileObject;
    private float timeTillFire = 0f;
    [SerializeField] private float fireRate = .5f;
    // Dodge
    //private bool isDodging = false;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    private float dodgeTime = .5f;
    //private float dodgeCooldown = 1f;
    [SerializeField] private float dodgeSpeed;
    // Health
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    private int healthIncrement = 5;
    private PlayerUI playerUI;
    // Audio
    private AudioSource audioSource_Shoot;
    // Limits bullets
    //public static int shotsFired = 0;
    //private int maxShots = 2;
    // Particles
    [SerializeField] private ParticleSystem poisonedParticles;

    public bool isPoisoned;
    public bool isRegeneratingHealth;
    private float startBurnRoutineTime;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        mainCamera = Camera.main;
        startPosition = transform.position;
        timeTillFire = 0;
        rigidBody = GetComponent<Rigidbody>();
        audioSource_Shoot = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }

    private void Update()
    {
        //time += Time.deltaTime;
        //Debug.Log("Time in game = " + time); ;
        if (GameManager.isGamePaused == false)
        {
            Move();
            LookAtMouse();
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //Debug.Log("Space is pressed!");
        //    isDodging = true;
        //    dodgeSpeed = 250f;
        //    //StartCoroutine(Dodge());
        //}
        //if (isDodging == true)
        //{
        //    float dodgeSpeedDropMultiplier = 5f;
        //    dodgeSpeed -= dodgeSpeed * dodgeSpeedDropMultiplier * Time.deltaTime;

        //    float dodgeMinimum = 175f;
        //    if (dodgeSpeed <= dodgeMinimum)
        //    {
        //        isDodging = false;
        //    }
        //}    

        //Die();
        //Shop();
    }

    //private void FixedUpdate()
    //{
    //    //if (isDodging == true)
    //    //{
    //    //    rigidBody.velocity = lastMoveDir * dodgeSpeed;
    //    //}
    //    //else
    //    //{
    //    //    Move();
    //    //}
    //}

    private void Move()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
            if (transform.position.z > topBoundary) // Top Boundary
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, topBoundary);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
            if (transform.position.z < bottomBoundary) // Bottom Boundary
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, bottomBoundary);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector = inputVector.normalized;
        moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (moveDir != new Vector3(0, 0, 0))
        {
            lastMoveDir = moveDir;
        }
        // Check if Player is Falling off Map
        if (transform.position.y < -1)
        {
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(-18f, 0, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(18f, 0, transform.position.z);
            }
            TakeDamage(fallDamage);
        }
    }

    //private void Move()
    //{
    //    rigidBody.velocity = moveDir * moveSpeed;
    //}

    private void LookAtMouse()
    {
        Vector3 direction;
        float angle;
        Quaternion rotation;

        direction = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);    // Gets direction of mouse
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;                          // Gets angle changed
        rotation = Quaternion.AngleAxis(angle, Vector3.up);                                     // Store rotation in a variable
        transform.rotation = rotation;
    }

    private void Fire() // Different way to do shooting
    {
        // Shoots every click
        //Instantiate(projectileObject, projectileSpawnPoint.transform.position, transform.rotation);// - new Vector3(.25f, 0, 0), transform.rotation);        // Spawn projectile
        //audioSource_Shoot.PlayOneShot(audioSource_Shoot.clip);

        // Shoots every click and every fireRate
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            timeTillFire = 1;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timeTillFire += Time.deltaTime;        // Constantly add time for how long you hold the button.
            if (timeTillFire > fireRate)        // Constantly check if nextFire is greater than fireRate;
            {
                Instantiate(projectileObject, projectileSpawnPoint.transform.position, transform.rotation);// - new Vector3(.25f, 0, 0), transform.rotation);        // Spawn projectile
                audioSource_Shoot.PlayOneShot(audioSource_Shoot.clip);                                                                                           //Instantiate(projectileObject, projectileSpawnPoint.transform.position + new Vector3(.25f, 0, 0) , transform.rotation);
                timeTillFire = 0;
            }
        }
        //timeTillFire += Time.deltaTime;        // Constantly add time for how long you hold the button.
        //if (timeTillFire > fireRate)        // Constantly check if nextFire is greater than fireRate;
        //{
        //    Instantiate(projectileObject, projectileSpawnPoint.transform.position, transform.rotation);// - new Vector3(.25f, 0, 0), transform.rotation);        // Spawn projectile
        //    audioSource_Shoot.PlayOneShot(audioSource_Shoot.clip);                                                                                           //Instantiate(projectileObject, projectileSpawnPoint.transform.position + new Vector3(.25f, 0, 0) , transform.rotation);
        //    timeTillFire = 0;
        //}

        //if (GameManager.isShopOpen == false)
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        //if (shotsFired < maxShots)
        //        //{
        //        //    Instantiate(projectileObject, projectileSpawnPoint.transform.position, transform.rotation);
        //        //    shotsFired++;
        //        //    audioSource_Shoot.PlayOneShot(audioSource_Shoot.clip);
        //        //}
        //        Instantiate(projectileObject, projectileSpawnPoint.transform.position, transform.rotation);
        //        shotsFired++;
        //        audioSource_Shoot.PlayOneShot(audioSource_Shoot.clip);
        //    }
        //}
    }

    private IEnumerator Dodge() // Not in use
    {
        //Vector3 dodgePosition = transform.position + Vector3.right * 5;
        //transform.position = dodgePosition;

        //rigidBody.AddForce(transform.right * dodgeSpeed, ForceMode.Impulse);
        rigidBody.MovePosition(transform.position + lastMoveDir * dodgeSpeed);
        //transform.Translate(transform.position + (lastMoveDir * dodgeSpeed));
        Debug.Log("Dodging in direction " + lastMoveDir +  ", now waiting for " + dodgeTime + " seconds.");
        yield return new WaitForSeconds(dodgeTime);
        Debug.Log("Done waiting . . .");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //Debug.Log("Player health = " + currentHealth);
        playerUI.UpdateCurrentHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        LoseLive();
        //Debug.Log("Game Over!");
        //UnityEditor.EditorApplication.isPlaying = false;
        transform.position = startPosition;
        currentHealth = maxHealth;
        playerUI.UpdateCurrentHealth(currentHealth);
    }

    //public void UpgradeFireRate()
    //{
    //    fireRate -= .05f;
    //}

    public void IncreaseHealth()
    {
        currentHealth += healthIncrement;
        maxHealth += healthIncrement;
        playerUI.UpdateMaxHealth(maxHealth);
        playerUI.UpdateCurrentHealth(currentHealth);
    }

    public void LoseLive()
    {
        lives--;
        if (lives < 0)
        {
            //Application.Quit();
            GameManager.Instance.GameOver();
            //Debug.Log("Game Over");
        }
        playerUI.UpdateLives(lives);
    }

    public void StartBurnRoutine(float damage, float duration, float tickTime)
    {
        if (startBurnRoutineTime + 2 < Time.time)
        {
            StartCoroutine(BurnRoutine(damage, duration, tickTime));
        }
    }

    public IEnumerator BurnRoutine(float damage, float duration, float tickTime)
    {
        startBurnRoutineTime = Time.time;
        ParticleSystem particle = Instantiate(poisonedParticles, transform);
        particle.Play();
        while (Time.time - startBurnRoutineTime <= duration)
        {
            yield return new WaitForSeconds(tickTime);
            TakeDamage((int)damage);
        }
        Destroy(particle);
    }

    public IEnumerator AttributeBuffRoutine()
    {
        throw new System.NotImplementedException();
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetLivesRemaining()
    {
        return lives;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
