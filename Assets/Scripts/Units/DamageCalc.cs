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
    public static float GetAbilityDamage(Ability ability, Unit_Statistics stats)
    {
        float damage = 0f;
        float statFlatMod = 0f;
        float statAmpMod = 0f;
        switch (ability.dmgType)
        {
            case DamageType.Physical:
                statFlatMod = stats.GetStat(UnitStatType.PhysDmgFlat).Value;
                statAmpMod = stats.GetStat(UnitStatType.PhysDmgAmp).Value / 100f;
                break;
            case DamageType.Magical:
                statFlatMod =  stats.GetStat(UnitStatType.MagiDmgFlat).Value;
                statAmpMod = stats.GetStat(UnitStatType.MagiDmgAmp).Value / 100f;
                break;
            case DamageType.Pure:
                //No modifiers
                break;
            default:
                Debug.LogError(string.Format("No damage type specified for {0}", ability.aName));
                break;
        }

        damage = CalcDamageAmplified(ability.baseDamage, statFlatMod, ability.damagePercentAmp/100f, statAmpMod);

        if (damage < 0f) damage = 0f;
        return damage;
    }

    public static float GetDamageReduced(float incomingDamage, DamageType dmgType, Unit_Statistics stats)
    {
        float damage = 0f;
        float flatMod = 0f;
        float statAmpMod = 0f;
        switch (dmgType)
        {
            case DamageType.Physical:
                flatMod = stats.GetStat(UnitStatType.PhysBlock).Value;
                statAmpMod = stats.GetStat(UnitStatType.PhysDefPercent).Value / 100f;
                break;
            case DamageType.Magical:
                flatMod = stats.GetStat(UnitStatType.MagiBlock).Value;
                statAmpMod = stats.GetStat(UnitStatType.MagiDefPercent).Value / 100f;
                break;
            case DamageType.Pure:
                //No modifiers
                break;
            default:
                Debug.LogError(string.Format("No damage type specified for GetDamageReduced()"));
                break;
        }

        damage = CalcDamageReduced(incomingDamage, flatMod, statAmpMod);

        if (damage < 0f) damage = 0f;
        return damage;
    }

    private static float CalcDamageAmplified(float abilityFlatDamage, float statFlatDamage, float abilityMultiplier, float statMultiplier)
    {
        return (abilityFlatDamage + statFlatDamage) * (1 + abilityMultiplier) * (1 + statMultiplier);
    }
    private static float CalcDamageReduced(float incomingDamage, float statFlatReduction, float statMultiplier)
    {
        return  incomingDamage * statMultiplier - statFlatReduction;
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
                if (item.isOffensive == isOutgoing)
                {
                    flatMod += item.GetSpecialFlatMod(unitItems, ability, caster, target);
                    ampMod += item.GetSpecialPercentAddMod(unitItems, ability, caster, target);
                }
            }
        }

        damage = isOutgoing ? (damage + flatMod) * ampMod : (damage * ampMod) - flatMod;

        return damage;
    }
}
