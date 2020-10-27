using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public float damageModifier;
    [HideInInspector] public float baseDelay;
    [HideInInspector] public float baseRange;
    [HideInInspector] public float baseDamage;
    [HideInInspector] public GameObject abilityPrefab;
    [HideInInspector] public UnitAnimation castAnimation;
    [HideInInspector] public DamageType dmgType;
    [HideInInspector] public DamageImpactType dmgImpactType;


    protected Unit_Statistics unitStats;

    public void Awake()
    {
        unitStats = gameObject.GetComponent<Unit_Statistics>();
    }

    public virtual void fire(Ability ability)
    {
        Debug.LogError("No Implementation for fire");
    }

    public virtual void fire(Ability ability, Transform target)
    {
        Debug.LogError("No Implementation for fire");
    }

}
