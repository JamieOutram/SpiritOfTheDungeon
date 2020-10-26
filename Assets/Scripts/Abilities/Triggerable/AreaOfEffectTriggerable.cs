using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script defines what happens when the ability is triggered
public class AreaOfEffectTriggerable : MonoBehaviour
{
    [HideInInspector] public float baseDelay;
    [HideInInspector] public float damageModifier;
    [HideInInspector] public float baseRange;
    [HideInInspector] public GameObject abilityEffect; 
    [HideInInspector] public bool isInstantiateInWorldSpace;

    Unit_Statistics unitStats;
    
    public void Awake()
    {
        unitStats = gameObject.GetComponent<Unit_Statistics>();
    }

    public void Start()
    {
        //Debug.Log(string.Format("This Object is {0}!", gameObject.name));

    }

    
    public void Fire(Ability ability)
    {

        //Debug.Log(string.Format("AoE Fired by {0}!", gameObject.name));
        GameObject effectObj = Instantiate(abilityEffect, gameObject.transform, isInstantiateInWorldSpace);
        AoeBehaviour behaviourScript = effectObj.GetComponent<AoeBehaviour>();
        behaviourScript.damage = (int)Math.Round(DamageCalc.GetDamage(true, ability, unitStats) * damageModifier);
        behaviourScript.casterObj = gameObject;
    }
}
