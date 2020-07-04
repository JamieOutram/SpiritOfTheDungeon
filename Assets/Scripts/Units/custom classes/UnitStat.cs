using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult,
}


public class UnitStat
{
    public float baseValue;
    public readonly UnitStatType statType;
    protected readonly List<StatModifier> statModifiers;

    public UnitStat(float bValue, UnitStatType sType)
    {
        baseValue = bValue;
        statType = sType;
        statModifiers = new List<StatModifier>();
    }
    // public UnitStat() : this(10f) { }


    private bool _isDirty = true;
    private int _value;

    public int Value { 
        get {
            if (_isDirty) {
                _value = CalculateFinalValue();
                _isDirty = false;
            }
            return _value;
        } 
    }

    public void AddModifier(StatModifier mod)
    {
        _isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    // Add this method to the CharacterStat class
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

    public bool RemoveModifier(StatModifier mod)
    {
        _isDirty = true;
        return statModifiers.Remove(mod);
    }

    private int CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                //Sum additive modifiers
                sumPercentAdd += mod.Value;

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Apply modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        // Rounding gets around float calculation errors and 
        return (int)Math.Round(finalValue);
    }

}
