using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA;

[CreateAssetMenu(menuName = "Items/Shield")]
public class Shield : EquipableItem
{
    public float flatReduction;
    public float percentReduction;
    public float protectionArc;

    private ShieldEffectTriggerable itemTrigger;

    public override EquipableItemType itemType { 
        get
        {
            return EquipableItemType.Shield;
        } 
    }

    public override void Initialize(GameObject obj)
    {
        itemTrigger = obj.GetComponentInChildren<ShieldEffectTriggerable>();
        itemTrigger.flatReduction = flatReduction;
        itemTrigger.percentReduction = percentReduction;
        itemTrigger.protectionArc = protectionArc;
    }

    public override int DamageModTrigger(int damage, Transform source)
    {
        return itemTrigger.ArcBlockEffect(damage, source);
    }

}
