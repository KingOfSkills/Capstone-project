using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    [SerializeField] StatusEffectSO statusEffectSO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            ApplyBurnEffect(player.GetComponent<IStatusEffectable>(), statusEffectSO);
            Destroy(gameObject);
        }
    }

    public static void ApplyBurnEffect(IStatusEffectable target, StatusEffectSO statusEffectSO)
    {
        target.StartBurnRoutine(statusEffectSO.Damage, statusEffectSO.Duration, statusEffectSO.TickTime);
    }

    public IEnumerator BurnRoutine(float damage, float duration, float tickTime)
    {
        float startTime = Time.time;
        while (Time.time - startTime <= duration)
        {
            yield return new WaitForSeconds(tickTime);
            //TakeDamage((int)damage);
        }
    }
}
