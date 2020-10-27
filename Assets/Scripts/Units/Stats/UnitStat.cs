using System;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult,
}


public class UnitStat
{
    private UnitResource _linkedResource;
    public UnitResource LinkedResource 
    {
        get
        {
            return _linkedResource;
        }
        set
        {
            _linkedResource = value;
            if (value != null)
                _linkedResource.UpdateLimits(this.Value);
        }
    }

    public List<DerivedUnitStat> _linkedDerivedStats;
    public void AddDerivedStat(DerivedUnitStat dStat)
    {
        if (!_linkedDerivedStats.Contains(dStat))
        {
            _linkedDerivedStats.Add(dStat);
            dStat.UpdateValue(this.Value);
        }
    }
    public void RemoveDerivedStat(DerivedUnitStat dStat)
    {
        if (_linkedDerivedStats.Contains(dStat))
        {
            _linkedDerivedStats.Remove(dStat);
        }
    }

    public readonly UnitStatType statType;
    protected readonly List<StatModifier> statModifiers;

    public UnitStat(float bValue, UnitStatType sType)
    {
        _baseValue = bValue;
        statType = sType;
        statModifiers = new List<StatModifier>();
        _linkedDerivedStats = new List<DerivedUnitStat>();
        UpdateValues();
    }
    // public UnitStat() : this(10f) { }

    private float _baseValue;

    public float BaseValue
    {
        get 
        {
            return _baseValue;
        }
        set 
        {
            _baseValue = value;
            UpdateValues();
        }
    }
    public int Value { get; private set; }

    public void AddModifier(StatModifier mod)
    {
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
        UpdateValues();
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
        var result = statModifiers.Remove(mod);
        UpdateValues();
        return result;
    }

    private int CalculateFinalValue()
    {
        float finalValue = _baseValue;
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

    private void UpdateValues()
    {
        Value = CalculateFinalValue();
        if(_linkedDerivedStats != null)
        {
            foreach(DerivedUnitStat stat in _linkedDerivedStats)
            {
                stat.UpdateValue(this.Value);
            }
        }
        if (_linkedResource != null)
        {
            _linkedResource.UpdateLimits(this.Value);
        }
        //Debug.Log(string.Format("Stat {0} updated to {1}", statType, value));
    }

    

}
