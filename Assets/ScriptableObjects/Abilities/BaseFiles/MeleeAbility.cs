using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/MeleeAbility")]
//Template defining the basic requirements for an AoE ability and provides trigger 
public class MeleeAbility : Ability
{
    public const bool isMelee = true;
    
    public override void Initialize(GameObject obj)
    {

    }
}
