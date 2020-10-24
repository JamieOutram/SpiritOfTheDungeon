using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DerivedUnitStat : UnitStat
{
    private bool _isInitial = true;

    public DerivedUnitStat(float bValue, UnitStatType sType) : base(bValue, sType) {}

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
            default:
                Debug.LogError("Invalid stat used with DerivedUnitStat class instance.");
                throw new ArgumentException("Invalid stat used with DerivedUnitStat class instance.");
        }

        base.AddModifier(baseModifier);
    }

}
