using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DerivedUnitStat : UnitStat
{
    private bool _isInitial = true;

    public DerivedUnitStat(float bValue, UnitStatType sType, UnitResource lRes = null) : base(bValue, sType, null, lRes) {}

    private StatModifier baseModifier;
    public void UpdateValue(UnitStat unitStat)
    {
        if (_isInitial == false) base.RemoveModifier(baseModifier);
        else _isInitial = false;

        //Base class constructor is run first
        switch (base.statType)
        {
            case UnitStatType.MaxHealth:
                //Check pairing valid
                if (unitStat.statType != UnitStatType.Vit)
                {
                    Debug.LogWarning("Unexpected stat type pairing for derived stat.");
                }

                //Maximum Health formula
                baseModifier = new StatModifier(unitStat.value * 10, StatModType.Flat);
                base.AddModifier(baseModifier);
                break;
            case UnitStatType.MaxMana:
                //Check pairing valid
                if (unitStat.statType != UnitStatType.Int)
                {
                    Debug.LogWarning("Unexpected stat type pairing for derived stat.");
                }

                //Maximum Mana formula
                baseModifier = new StatModifier(unitStat.value * 10, StatModType.Flat);
                base.AddModifier(baseModifier);
                break;
            default:
                Debug.LogError("Invalid stat used with DerivedUnitStat class instance.");
                throw new ArgumentException("Invalid stat used with DerivedUnitStat class instance.");
        }
    }

}
