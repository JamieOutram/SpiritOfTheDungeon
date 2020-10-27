using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/AreaOfEffectAbility")]
//Template defining the basic requirements for an AoE ability and provides trigger 
public class AreaOfEffectAbility : Ability
{
    public float baseDelay = 0f;
    public float baseDuration = 3f;
    public float baseRange = 3f;
    public GameObject abilityEffectPrefab;
    public bool isTrackPlayer;

    private AreaOfEffectTriggerable aoeHit;
    
    public override void Initialize(GameObject obj)
    {
        //Find triggerable object
        aoeHit = obj.GetComponent<AreaOfEffectTriggerable>();

        aoeHit.isInstantiateInWorldSpace = !isTrackPlayer;
        aoeHit.abilityEffect = abilityEffectPrefab;
        //Pass custom values required for Fire()
        aoeHit.baseDelay = baseDelay;
        aoeHit.damageModifier = damageMultiplier;
        aoeHit.baseRange = baseRange;

    }

    public override void TriggerAbility()
    {
        aoeHit.Fire(this);
    }
}
