using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : NamedScriptableObject
{
    public float aFlatDamageMod = 0f;
    public float aMulDamageMod = 1f;


    public virtual void ActiveEffect(float damage, Transform source)
    {
        Debug.LogWarning(string.Format("No ActiveEffect() override specified for {0}", aName));
    }

    public virtual void OnHitEffect(float damage, Transform source) 
    {
        Debug.LogWarning(string.Format("No OnHitEffect() override specified for {0}", aName));
    }

    public virtual float DamageModEffect(float damage, Transform source)
    {
        Debug.LogWarning(string.Format("No DamageModEffect() override specified for {0}", aName));
        return damage;
    }

}
