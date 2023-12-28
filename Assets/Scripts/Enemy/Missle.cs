using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip launchClip;
    [SerializeField] private AudioClip explosionClip;
    private MissleUI missleUI;
    // Move & LookAtTarget
    private float speed = 5f;
    private float turnSpeed = 1f;
    private float increaseRate = 10f;
    // Delay
    private float delayTime = 1f;
    private bool isHeatingUp = true;
    //Find Target
    private Vector3 targetPoint;
    private float accuracyOffset = 5f;
    private int xBound = 19;
    private int zTopBound = 21;
    private int zBotBound = 2;
    private bool hasTarget = false;
    private GameObject target;
    // Explode
    private float radius = 2f;
    private float explosionForce = 550f;
    private int damage = 5;

    private void Start()
    {
        //FindTarget();
        missleUI = FindObjectOfType<MissleUI>();
        isHeatingUp = true;
        player = FindObjectOfType<PlayerController>().gameObject;
        AudioManager.Instance.PlayClip(launchClip);
    }

    void Update()
    {
        Move();
        if (isHeatingUp == true)
        {
            Delay();
            //Debug.Log(delayTime);
        }
        if (hasTarget == true)
        {
            LookAtTarget();
        }
        if (transform.position.y <= .5f)
        {
            Explode();
        }
    }

    private void Move()
    {
        var moveDir = transform.forward;
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void Delay()
    {
        delayTime -= Time.deltaTime;
        if (delayTime <= 0)
        {
            speed = 15f;
            FindTarget();
            isHeatingUp = false;
        }
    }

    private void FindTarget()
    {
        //Debug.Log("Finding Target . . .");
        targetPoint = player.transform.position + new Vector3(Random.Range(-accuracyOffset, accuracyOffset), // x
                                                              0, // y
                                                              Random.Range(-accuracyOffset, accuracyOffset)); // z
        //missleUI = new MissleUI(targetPoint);
        // Check xBound
        if (targetPoint.x < -xBound)
        {
            targetPoint.x = -xBound;
        } else if (targetPoint.x > xBound)
        {
            targetPoint.x = xBound;
        }
        // Check zBound
        if (targetPoint.z < zBotBound)
        {
            targetPoint.z = zBotBound;
        }
        else if (targetPoint.z > zTopBound)
        {
            targetPoint.z = zTopBound;
        }
        //Instantiate();
        target = missleUI.SetMissleUI(targetPoint);
        hasTarget = true;
    }

    private void LookAtTarget()
    {
        // Rotation
        Quaternion rotation = Quaternion.LookRotation((targetPoint - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed).normalized;
        // Increase Speed
        speed += Time.deltaTime * increaseRate;
        //speed = 20f;
        turnSpeed = 5f;
    }

    private void Explode()
    {
        AudioManager.Instance.PlayClip(explosionClip, .1f);

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
        //Debug.Log("Explode");
        Destroy(target);
        Destroy(gameObject);
    }
}
