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

    Unit_Statistics stats;
    private void Awake()
    {
        stats = GetComponent<Unit_Statistics>();
    }


    public void Fire(Ability ability)
    {
        //Debug.Log("Projectile Fired!");
        GameObject projectileObj = Instantiate(abilityProjectile) as GameObject;
        projectileObj.transform.position = gameObject.transform.position;
        projectileObj.transform.rotation = gameObject.transform.rotation;
        projectileObj.transform.position += startForwardOffset * projectileObj.transform.right;
        
        Projectile_Behavior objScript = projectileObj.GetComponent<Projectile_Behavior>();
        objScript.range = baseRange;
        objScript.speed = baseSpeed;
        objScript.damage = Mathf.FloorToInt(DamageCalc.GetDamage(true, ability, stats) * damageModifier);  //temp
        Debug.Log(string.Format("Firing projectile with {0} damage", Mathf.FloorToInt(DamageCalc.GetDamage(true, ability, stats))));
        objScript.initialized = true;
        objScript.casterObj = gameObject;

        if (baseRange == 0)
        {
            objScript.range = 999;
        }

    }
}
