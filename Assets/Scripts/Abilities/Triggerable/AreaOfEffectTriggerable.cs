using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script defines what happens when the ability is triggered
public class AreaOfEffectTriggerable : MonoBehaviour
{
    
    [HideInInspector] public float baseDelay;
    [HideInInspector] public float damageModifier;
    [HideInInspector] public float baseRange;
    [HideInInspector] public GameObject abilityEffect; //collider representing AoE
    [HideInInspector] public bool isInstantiateInWorldSpace;

    public void Fire()
    {
        
        
        Debug.Log("AoE Fired!");
        GameObject effectObj = Instantiate(abilityEffect, gameObject.transform, isInstantiateInWorldSpace);
        
    }
}
