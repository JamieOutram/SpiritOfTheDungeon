using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Android;
using UnityEngine;

public class ProjectileTriggerable : BaseAbilityTriggerable
{
    public void Fire(ProjectileAbility ability)
    {
        //Debug.Log("Projectile Fired!");
        GameObject projectileObj = Instantiate(ability.abilityPrefab) as GameObject;
        projectileObj.transform.position = gameObject.transform.position;
        projectileObj.transform.rotation = gameObject.transform.rotation;
        projectileObj.transform.position += ability.startForwardOffset * projectileObj.transform.right;
        
        Projectile_Behavior objScript = projectileObj.GetComponent<Projectile_Behavior>();
        objScript.range = ability.baseRange;
        if (ability.baseRange == -1)
            objScript.range = 999;
        objScript.speed = ability.baseSpeed;
        objScript.damage = (int)DamageCalc.GetAbilityDamage(ability, unitStats);  
        //Debug.Log(string.Format("Firing projectile with {0} damage", Mathf.FloorToInt(DamageCalc.GetAbilityDamage(true, ability, stats) * damageModifier)));
        objScript.initialized = true;
        objScript.casterObj = gameObject;
    }
}
