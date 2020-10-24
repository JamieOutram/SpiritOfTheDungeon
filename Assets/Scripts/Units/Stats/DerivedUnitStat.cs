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

        //Base class constructor is run first
        switch (base.statType)
        {
            case UnitStatType.MaxHealth: //Maximum Health formula
                baseModifier = new StatModifier(unitStatValue * 10, StatModType.Flat);
                break;

            case UnitStatType.MaxMana: //Maximum Mana formula
                baseModifier = new StatModifier(unitStatValue * 10, StatModType.Flat);
                break;

            case UnitStatType.PhysDefAmp: //Physical Defence multiplier formula
                baseModifier = new StatModifier(1f - (unitStatValue * 0.1f) / (1f + unitStatValue * 0.1f), StatModType.PercentMult);
                break;

            case UnitStatType.PhysDefFlat: //Physical Defence bonus formula
                baseModifier = new StatModifier(unitStatValue * 5f, StatModType.Flat);
                break;

            case UnitStatType.PhysDmgAmp: //Physical Attack multiplier formula
                baseModifier = new StatModifier(unitStatValue * 0.05f, StatModType.PercentAdd);
                break;

            case UnitStatType.PhysDmgFlat: //Physical Attack bonus formula
                baseModifier = new StatModifier(unitStatValue * 10f, StatModType.Flat);
                break;

            case UnitStatType.MagiDefAmp: //Magical Defence multiplier formula
                baseModifier = new StatModifier(1f - (unitStatValue * 0.1f) / (1f + unitStatValue * 0.1f), StatModType.PercentMult);
                break;

            case UnitStatType.MagiDefFlat: //Magical Defence bonus formula
                baseModifier = new StatModifier(unitStatValue * 5f, StatModType.Flat);
                break;

            case UnitStatType.MagiDmgAmp: //Magical Attack multiplier formula
                baseModifier = new StatModifier(unitStatValue * 0.05f, StatModType.PercentAdd);
                break;

            case UnitStatType.MagiDmgFlat: //Magical Attack bonus formula
                baseModifier = new StatModifier(unitStatValue * 10f, StatModType.Flat);
                break;

            default:
                Debug.LogError("Invalid stat used with DerivedUnitStat class instance.");
                throw new ArgumentException("Invalid stat used with DerivedUnitStat class instance.");
        }

        base.AddModifier(baseModifier);
    }

}
