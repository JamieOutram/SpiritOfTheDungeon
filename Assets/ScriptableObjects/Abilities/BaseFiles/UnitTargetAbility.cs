using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/UnitTargetAbility")]
public class UnitTargetAbility : Ability
{
    //public int damageMultiplier = 1;
    public float baseDelay = 0f;
    public float baseRange = 3f;
    public string targetGroupTag = "any";
    public GameObject particleEffect;

    private UnitTargetTriggerable unitTargetHit;

    public override void Initialize(GameObject obj)
    {
        //Find triggerable object
        unitTargetHit = obj.GetComponent<UnitTargetTriggerable>();

        //Pass custom values required for Fire()
        unitTargetHit.damageModifier = damageMultiplier;
        unitTargetHit.baseDelay = baseDelay;
        unitTargetHit.baseRange = baseRange;
        unitTargetHit.targetGroupTag = targetGroupTag;
        unitTargetHit.abilityPrefab = particleEffect;
    }

    public override void TriggerAbility(GameObject target)
    {
        unitTargetHit.Fire(target, this);
    }
}

