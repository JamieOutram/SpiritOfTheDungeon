using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script defines what happens when the ability is triggered
public class AreaOfEffectTriggerable : BaseAbilityTriggerable
{
    public void Fire(AreaOfEffectAbility ability)
    {
        //Debug.Log(string.Format("AoE Fired by {0}!", gameObject.name));
        GameObject effectObj = Instantiate(ability.abilityPrefab, gameObject.transform, !ability.isTrackPlayer);
        AoeBehaviour behaviourScript = effectObj.GetComponent<AoeBehaviour>();
        behaviourScript.damage = (int)DamageCalc.GetAbilityDamage(ability, unitStats);
        behaviourScript.casterObj = gameObject;
    }
}
