using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetTriggerable : MonoBehaviour
{
    [HideInInspector] public float damageModifier;
    [HideInInspector] public float baseDelay;
    [HideInInspector] public float baseRange;
    [HideInInspector] public string targetGroupTag;
    [HideInInspector] public GameObject particleEffect;

    Unit_Statistics unitStats;

    public void Awake()
    {
        unitStats = gameObject.GetComponent<Unit_Statistics>();
    }

    public void Fire(GameObject target, Ability ability)
    {
        GameObject effectObj = Instantiate(particleEffect, target.transform, false);
        UnitTargetBehaviour effect = effectObj.GetComponent<UnitTargetBehaviour>();
        effect.target = target;
        effect.damage = (int)Math.Round(DamageCalc.GetDamage(true, ability, unitStats) * damageModifier);
    }
}
