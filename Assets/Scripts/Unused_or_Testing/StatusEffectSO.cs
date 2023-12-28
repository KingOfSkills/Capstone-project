using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatusEffectSO : ScriptableObject
{
    public enum Attribute
    {
        Strengh,
        Intelligence,
        Agility,
    }

    public Attribute attribute;
    public string Name;
    public float Damage;
    public float TickTime;
    public float Duration;
}
