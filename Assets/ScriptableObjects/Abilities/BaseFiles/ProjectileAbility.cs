using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    //public int damageMultiplier = 1;
    public float baseSpeed = 0.01f;
    public float startForwardOffset = 0.5f;

    public override void Initialize(GameObject obj)
    {
        
    }
}
