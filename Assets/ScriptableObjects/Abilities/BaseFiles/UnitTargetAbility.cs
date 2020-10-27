using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/UnitTargetAbility")]
public class UnitTargetAbility : Ability
{
    //public int damageMultiplier = 1;
    public string targetGroupTag = "any";

    public override void Initialize(GameObject obj)
    {
        
    }
}

