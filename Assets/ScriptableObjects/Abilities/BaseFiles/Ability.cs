using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Timeline;

//The base class for all abilities
public abstract class Ability : NamedScriptableObject
{
    public Sprite aSprite;
    public AudioClip aSound;
    public float baseCooldown = 1f;
    public float baseDamage = 0f;
    public DamageType dmgType = DamageType.None;
    public DamageImpactType dmgImpactType = DamageImpactType.None;
    public bool isHeal;

    public virtual void TriggerAbility(){
        throw new System.Exception("No Override for TriggerAbility variant in Ability.cs");
    }

    public virtual void TriggerAbility(GameObject target)
    {
        throw new System.Exception("No Override for TriggerAbility variant in Ability.cs");
    }
}
