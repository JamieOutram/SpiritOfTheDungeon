using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DerivedUnitStat : UnitStat
{
    private bool _isInitial = true;

    public DerivedUnitStat(float bValue, UnitStatType sType) : base(bValue, sType) { }

    private StatModifier baseModifier;
    public void UpdateValue(float unitStatValue)
    {
        if (_isInitial == false) base.RemoveModifier(baseModifier);
        else _isInitial = false;

        float value = 0f;
        //Base class constructor is run first
        switch (base.statType)
        {
            case UnitStatType.Spd: //Speed formula
                baseModifier = new StatModifier(unitStatValue * 10, StatModType.Flat);
                break;

            case UnitStatType.MaxHealth: //Maximum Health formula
                baseModifier = new StatModifier(unitStatValue * 10, StatModType.Flat);
                break;

            case UnitStatType.MaxMana: //Maximum Mana formula
                baseModifier = new StatModifier(unitStatValue * 10, StatModType.Flat);
                break;

            case UnitStatType.PhysArmour: //Physical Armour formula
                baseModifier = new StatModifier(unitStatValue * 0.2f, StatModType.Flat);
                break;

            case UnitStatType.PhysDefMult: //Physical Defence multiplier formula
                value = (1f - (unitStatValue * 0.1f) / (1f + unitStatValue * 0.1f)) * 100f;
                baseModifier = new StatModifier(Mathf.Clamp(value, 0f, 100f), StatModType.Flat);
                break;

            case UnitStatType.PhysBlock: //Physical Defence bonus formula
                baseModifier = new StatModifier(unitStatValue * 5f, StatModType.Flat);
                break;

            case UnitStatType.PhysDmgAmp: //Physical Attack multiplier formula
                baseModifier = new StatModifier(unitStatValue * 5f, StatModType.Flat);
                break;

            case UnitStatType.PhysDmgFlat: //Physical Attack bonus formula
                baseModifier = new StatModifier(unitStatValue * 10f, StatModType.Flat);
                break;

            case UnitStatType.MagiArmour: //Magical Armour formula
                baseModifier = new StatModifier(unitStatValue * 0.2f, StatModType.Flat);
                break;

            case UnitStatType.MagiDefMult: //Magical Defence multiplier formula
                value = (1f - (unitStatValue * 0.1f) / (1f + unitStatValue * 0.1f)) * 100f;
                baseModifier = new StatModifier(Mathf.Clamp(value, 0f , 100f), StatModType.Flat);
                break;

            case UnitStatType.MagiBlock: //Magical Defence bonus formula
                baseModifier = new StatModifier(unitStatValue * 5f, StatModType.Flat);
                break;

            case UnitStatType.MagiDmgAmp: //Magical Attack multiplier formula
                baseModifier = new StatModifier(unitStatValue * 5f, StatModType.Flat);
                break;

            case UnitStatType.MagiDmgFlat: //Magical Attack bonus formula
                baseModifier = new StatModifier(unitStatValue * 10f, StatModType.Flat);
                break;

            default:
                Debug.LogError(string.Format("Invalid stat {0} used with DerivedUnitStat class instance.", statType.ToString()));
                throw new ArgumentException("Invalid stat used with DerivedUnitStat class instance.");
        }

        base.AddModifier(baseModifier);
    }

}
