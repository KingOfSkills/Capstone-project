using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    //private int damageUpgrade = 0;
    //private BulletUpgrade bulletUpgrade;
    private int damage = 1;
    
    private float bulletLifeTime = 5f;

    private void Awake()
    {
        var bulletUpgrade = FindObjectOfType<BulletUpgrade>();
        damage += bulletUpgrade.GetUpgradedDamage();
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        //Debug.Log((transform.position - startPosition).magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            // Object other inherits IDamagable
            damagable.TakeDamage(damage);
            //PlayerController.shotsFired--;
            Destroy(gameObject);
        }
    }

    public void IncreaseDamage()
    {
        damage += 1;
        Debug.Log(damage);
    }

    public int GetDamage()
    {
        return damage; //+ damageUpgrade;
    }
}
