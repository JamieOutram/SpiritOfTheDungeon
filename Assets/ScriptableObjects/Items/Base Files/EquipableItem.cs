using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class EquipableItem : NamedScriptableObject
{
    public Sprite aIcon;
    public string aDescription;
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

}
