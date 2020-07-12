using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Android;
using UnityEngine;

public class ProjectileTriggerable : MonoBehaviour
{
    
    
    [HideInInspector] public float damageModifier;
    [HideInInspector] public float baseDelay;
    [HideInInspector] public float baseRange;
    [HideInInspector] public float baseSpeed;
    [HideInInspector] public float startForwardOffset;
    [HideInInspector] public GameObject abilityProjectile;
    
    

    public void Fire()
    {
        Debug.Log("Projectile Fired!");
        GameObject projectileObj = Instantiate(abilityProjectile) as GameObject;
        projectileObj.transform.position = gameObject.transform.position;
        projectileObj.transform.rotation = gameObject.transform.rotation;
        projectileObj.transform.position += startForwardOffset * projectileObj.transform.right;
        
        Projectile_Behavior objScript = projectileObj.GetComponent<Projectile_Behavior>();
        objScript.range = baseRange;
        objScript.speed = baseSpeed;
        objScript.damage = 50; //temp
        objScript.initialized = true;

        if (baseRange == 0)
        {
            objScript.range = 999;
        }

    }
}
