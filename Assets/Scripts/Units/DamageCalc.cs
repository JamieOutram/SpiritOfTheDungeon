using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum DamageImpactType
{
    None,
    Point,
    Directional,
    Area,
}
public enum DamageType
{
    None,
    Physical,
    Magical,
    Pure,
}


//This should just manage Ability interactions.
//Item modifiers should be handled by UnitStat 
//Stat modifiers could be contained within derived stats but handled here
public static class DamageCalc
{
    public static float GetDamage(bool isOutgoing, Ability ability, Unit_Statistics stats)
    {
        float damage = 0f;
        float flatMod = 0f;
        float ampMod = 0f;
        switch (ability.dmgType)
        {
            case DamageType.Physical:
                flatMod = isOutgoing ? stats.GetStat(UnitStatType.PhysDmgFlat).Value : stats.GetStat(UnitStatType.PhysBlock).Value;
                ampMod = isOutgoing ? stats.GetStat(UnitStatType.PhysDmgAmp).Value : stats.GetStat(UnitStatType.PhysArmour).Value;
                break;
            case DamageType.Magical:
                flatMod = isOutgoing ? stats.GetStat(UnitStatType.MagiDmgFlat).Value : stats.GetStat(UnitStatType.MagiBlock).Value;
                ampMod = isOutgoing ? stats.GetStat(UnitStatType.MagiDmgAmp).Value : stats.GetStat(UnitStatType.MagiArmour).Value;
                break;
            case DamageType.Pure:
                //No modifiers
                break;
            default:
                Debug.LogError(string.Format("No damage type specified for {0}", ability.aName));
                break;
        }

        damage = isOutgoing ? (ability.baseDamage + flatMod) * (1+ampMod) : ability.baseDamage * ampMod - flatMod;

        if (damage < 0f) damage = 0f;
        return damage;
    }

    //TODO: For later use if special items added / shield adapted
    public static float ApplySpecialItemModifiers(
        float damage,
        bool isOutgoing,
        Unit_Items unitItems, 
        Ability ability,  
        Transform caster = null, 
        Transform target = null)
    {
        float flatMod = 0f;
        float ampMod = 1f;

        //Add item special modifiers
        List<EquipableItem> items = unitItems.GetAllItems();
        foreach (EquipableItem item in items)
        {
            if (!item.isBasic)
            {
                if(item.isOffensive == isOutgoing) {
                    flatMod += item.GetSpecialFlatMod(unitItems, ability, caster, target);
                    ampMod += item.GetSpecialPercentAddMod(unitItems, ability, caster, target);
                }
            }
        }

        damage = isOutgoing ? (damage + flatMod) * ampMod : (damage * ampMod) - flatMod;

        return damage;
    }
}
