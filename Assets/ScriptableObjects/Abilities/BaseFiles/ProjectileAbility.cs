using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public int damageMultiplier = 1;
    public float baseDelay = 0f;
    public float baseRange = 3f;
    public float baseSpeed = 0.01f;
    public float startForwardOffset = 0.5f;
    public GameObject projectile;
    
    private ProjectileTriggerable projectileHit;

    public override void Initialize(GameObject obj)
    {
        //Find triggerable object
        projectileHit = obj.GetComponent<ProjectileTriggerable>();
        
        //Pass custom values required for Fire()
        projectileHit.damageModifier = damageMultiplier;
        projectileHit.baseDelay = baseDelay;
        projectileHit.baseRange = baseRange;
        projectileHit.baseSpeed = baseSpeed;
        projectileHit.startForwardOffset = startForwardOffset;
        projectileHit.abilityProjectile = projectile;
    }

    public override void TriggerAbility()
    {
        projectileHit.Fire();
    }
}
