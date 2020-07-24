using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public enum UnitStatType
{
    Vit,
    Int,
    Dmg,
    Def,
    Spd,
    MaxHealth,
    MaxMana,
    Health,
    Mana,
    Ammo
}

public class Unit_Statistics : MonoBehaviour
{
    private Dictionary<UnitStatType, UnitStat> unit_stats = new Dictionary<UnitStatType, UnitStat>();

    private Dictionary<UnitStatType, UnitResource> unit_resources = new Dictionary<UnitStatType, UnitResource>();

    private bool _intialised = false;

    //Get and set functions here obscures the dictionary structure (easy to change later)  
    public UnitStat GetStat(UnitStatType statType)
    {

        if (_intialised == false)
        {
            Debug.LogWarning("GetStat called before Stats initialised in Unit_Statistics.");
            return null;
        }
        return unit_stats[statType];
    }

    public UnitResource GetResource(UnitStatType statType)
    {
        if (_intialised == false)
        {
            Debug.LogWarning("GetTrackedStat called before Stats initialised in Unit_Statistics.");
            return null;
        }
        return unit_resources[statType];
    }

    private void AddStat(UnitStatType type, float bValue)
    {
        unit_stats.Add(type, new UnitStat(bValue, type));
    }
    private void AddStat(UnitStatType type, float bValue, UnitStatType linkedType)
    {
        unit_stats.Add(type, new DerivedUnitStat(bValue, type, GetStat(linkedType)));
    }
    private void AddResource(UnitStatType type, UnitStatType linkedType)
    {
        UnitResource newStat = new UnitResource(GetStat(linkedType).Value, type);
        newStat.UpdateLimits(GetStat(linkedType).Value);
        unit_resources.Add(type, newStat);
    }
    private void AddResource(UnitStatType type, int baseValue)
    {
        UnitResource newStat = new UnitResource(baseValue, type);
        unit_resources.Add(type, newStat);
    }


    protected virtual void PopulateStats()
    {
        //Debug.Log("PopulateStats Called");
        //Base Stats
        AddStat(UnitStatType.Vit, 10f);
        AddStat(UnitStatType.Int, 10f);
        AddStat(UnitStatType.Dmg, 50f);
        AddStat(UnitStatType.Def, 0f);
        AddStat(UnitStatType.Spd, 3f);

        _intialised = true;

        //Add Derived Stats
        AddStat(UnitStatType.MaxHealth, 100f, UnitStatType.Vit);
        AddStat(UnitStatType.MaxMana, 10f, UnitStatType.Int);

        //Add Tracked Stats
        AddResource(UnitStatType.Health, UnitStatType.MaxHealth);
        AddResource(UnitStatType.Mana, UnitStatType.MaxMana);
        AddResource(UnitStatType.Ammo, 0);
        
        
    }
    
    public void Awake()
    {
        PopulateStats();
    }

}
