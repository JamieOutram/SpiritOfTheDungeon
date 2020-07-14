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

    public void Fire(GameObject target)
    {
        GameObject effectObj = Instantiate(particleEffect, target.transform, false);
        effectObj.GetComponent<UnitTargetEffect>().target = target;
        effectObj.GetComponent<UnitTargetEffect>().damage = (int)Math.Round(damageModifier);
        Debug.Log("UnitTarget Fired!");
    }
}
