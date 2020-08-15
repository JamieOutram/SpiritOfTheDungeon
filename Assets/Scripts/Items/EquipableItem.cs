using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : NamedScriptableObject
{
    public float aFlatDamageMod = 0f;
    public float aMulDamageMod = 1f;
    
    public EquipableItemType itemType {get; protected set;}


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
