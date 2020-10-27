using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/AreaOfEffectAbility")]
//Template defining the basic requirements for an AoE ability and provides trigger 
public class AreaOfEffectAbility : Ability
{
    public float baseDuration = 3f;
    public bool isTrackPlayer;
    
    public override void Initialize(GameObject obj)
    {

    }
}
