﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Timeline;

public enum UnitAnimation
{
    None,
    Cast,
    Melee,
}

//The base class for all abilities
public abstract class Ability : NamedScriptableObject
{
    public Sprite aSprite;
    public AudioClip aSound;
    public UnitAnimation castAnimation = UnitAnimation.None;
    public float baseCooldown = 1f;
    public float baseDamage = 0f;
    public float damageMultiplier = 0f;
    public int baseManaCost = 0;
    public DamageType dmgType = DamageType.None;
    public DamageImpactType dmgImpactType = DamageImpactType.None;
    public bool isHeal;

    public virtual void Initialise()
    {

    }

    public virtual void TriggerAbility(){
        throw new System.Exception("No Override for TriggerAbility variant in Ability.cs");
    }

    public virtual void TriggerAbility(GameObject target)
    {
        throw new System.Exception("No Override for TriggerAbility variant in Ability.cs");
    }
}
