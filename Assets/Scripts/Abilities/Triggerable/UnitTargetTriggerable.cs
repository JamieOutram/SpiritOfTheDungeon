using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetTriggerable : BaseAbilityTriggerable
{
    [HideInInspector] public string targetGroupTag;

    public void Fire(GameObject target, Ability ability)
    {
        GameObject effectObj = Instantiate(abilityPrefab, target.transform, false);
        UnitTargetBehaviour effect = effectObj.GetComponent<UnitTargetBehaviour>();
        effect.target = target;
        effect.damage = (int)DamageCalc.GetAbilityDamage(ability, unitStats);
    }
}
