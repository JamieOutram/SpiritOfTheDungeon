using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Timeline;

public enum UnitAnimation
{
    None,
    Cast,
    Melee,
}

public enum AbilityTargetType
{
    None,
    UnitTarget,
    AreaOfEffect,
    Projectile,
}

//The base class for all abilities
public abstract class Ability : NamedScriptableObject
{
    public Sprite aSprite;
    public AudioClip aSound;
    public UnitAnimation castAnimation = UnitAnimation.None;

    public GameObject abilityPrefab;
    public float baseCooldown = 1f;
    public float baseDamage = 0f;
    public float damagePercentAmp = 0f;
    public float baseRange = 3f;
    public float baseDelay = 0f;
    public int baseManaCost = 0;
    public DamageType dmgType = DamageType.None;
    public DamageImpactType dmgImpactType = DamageImpactType.None;
    public bool isHeal;

    public virtual void Initialise()
    {
        Debug.LogWarning("No Override for initialize() in Ability.cs");
    }
}
