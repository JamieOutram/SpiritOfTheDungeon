using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class EquipableItem : NamedScriptableObject
{
    public Sprite aIcon;
    public string aDescription;
    public bool isBasic = true;
    public bool isOffensive = true;

    public abstract EquipableItemType itemType { get; }


    public virtual void ActiveEffect(int damage, Transform source)
    {
        Debug.LogWarning(string.Format("No ActiveEffect() override specified for {0}", aName));
    }

    public virtual void OnHitEffect(int damage, Transform source)
    {
        Debug.LogWarning(string.Format("No OnHitEffect() override specified for {0}", aName));
    }

    public virtual int DamageModTrigger(int damage, Transform source)
    {
        Debug.LogWarning(string.Format("No DamageModTrigger() override specified for {0}", aName));
        return damage;
    }

    public virtual float GetSpecialFlatMod(
        Unit_Items unitItems,
        Ability ability,
        Transform caster = null,
        Transform target = null)
    {
        Debug.LogError(string.Format("No special flat modifier defined for {0}", aName)); //TODO
        return 0f;
    }
    public virtual float GetSpecialPercentAddMod(
        Unit_Items unitItems,
        Ability ability,
        Transform caster = null,
        Transform target = null)
    {
        Debug.LogError(string.Format("No special amp modifier defined for {0}", aName)); //TODO
        return 0f;
    }
}
