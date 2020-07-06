using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/AreaOfEffectAbility")]
//Template defining the basic requirements for an AoE ability and provides trigger 
public class AreaOfEffectAbility : Ability
{
    public int damageMultiplier = 1;
    public float baseDelay = 0f;
    public float baseDuration = 3f;
    public float baseRange = 3f;
    public GameObject abilityEffect;
    public bool isTrackPlayer;

    private AreaOfEffectTriggerable aoeHit;
    
    public override void Initialize(GameObject obj)
    {
        //Find triggerable object
        aoeHit = obj.GetComponent<AreaOfEffectTriggerable>();

        aoeHit.isInstantiateInWorldSpace = !isTrackPlayer;
        aoeHit.abilityEffect = abilityEffect;
        //Pass custom values required for Fire()
        aoeHit.baseDelay = baseDelay;
        aoeHit.damageModifier = damageMultiplier;
        aoeHit.baseRange = baseRange;

    }

    public override void TriggerAbility()
    {
        aoeHit.Fire();
    }
}
