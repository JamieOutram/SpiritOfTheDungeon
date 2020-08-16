using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
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
    Ammo,
    CarryWeight,
    AmmoCapcity,
}

public class Unit_Statistics : MonoBehaviour
{
    
    public int StatCount
    {
        get
        {
            return unit_stats.Count;
        }
    }
    public int ResourceCount
    {
        get
        {
            return unit_resources.Count;
        }
    }

    public List<UnitStatType> hiddenTypes;

    private Dictionary<UnitStatType, UnitStat> unit_stats;

    private Dictionary<UnitStatType, UnitResource> unit_resources;

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

    public List<Tuple<UnitStatType, int>> GetAllStatValues() 
    {
        List<Tuple<UnitStatType, int>> statValues = new List<Tuple<UnitStatType, int>>();
        foreach(UnitStatType key in unit_stats.Keys)
        {
            statValues.Add(Tuple.Create(key, GetStat(key).value));
        }
        return statValues;
    }
    public List<Tuple<UnitStatType, int>> GetAllResourceValues()
    {
        List<Tuple<UnitStatType, int>> resourceValues = new List<Tuple<UnitStatType, int>>();
        foreach (UnitStatType key in unit_resources.Keys)
        {
            resourceValues.Add(Tuple.Create(key, GetResource(key).Value));
        }
        return resourceValues;
    }

    private void AddStat(UnitStatType type, float bValue)
    {
        unit_stats.Add(type, new UnitStat(bValue, type));
    }
    private void AddLinkedStats(UnitStatType type, float bValue, 
        UnitStatType derivedType, float dBaseValue)
    {
        AddStat(type, bValue);
        //Accociate derived stat and add reference to list, values updated by assignment
        unit_stats[type].LinkedDerivedStat = new DerivedUnitStat(dBaseValue, derivedType);
        unit_stats.Add(derivedType, unit_stats[type].LinkedDerivedStat);
    }

    private void AddLinkedStats(UnitStatType type, float bValue,
    UnitStatType resourceType)
    {
        AddStat(type, bValue);
        //link the new resource to the base stat, limits updated by assignment
        unit_stats[type].LinkedResource = new UnitResource(resourceType);
        unit_resources.Add(resourceType, unit_stats[type].LinkedResource);
    }

    private void AddLinkedStats(UnitStatType type, float bValue,
        UnitStatType derivedType, float dBaseValue,
        UnitStatType resourceType, bool isDerivedLinksResource)
    {
        AddLinkedStats(type, bValue, derivedType, dBaseValue);
        //if the resource specified is linked to the derived stat
        if (isDerivedLinksResource)
        {
            //Link new resource to derived stat, limits updated by assignment
            unit_stats[derivedType].LinkedResource = new UnitResource(resourceType);
            unit_resources.Add(resourceType, unit_stats[derivedType].LinkedResource);
        }
        else
        {
            //otherwise link the new resource to the base stat, limits updated by assignment
            unit_stats[type].LinkedResource = new UnitResource(resourceType);
            unit_resources.Add(resourceType, unit_stats[type].LinkedResource);
        }
        
    }

    protected virtual void PopulateStats()
    {
        //Debug.Log("PopulateStats Called");
        //Base Stats
        AddLinkedStats(UnitStatType.Vit, 10f, UnitStatType.MaxHealth, 100f, UnitStatType.Health, true);
        AddLinkedStats(UnitStatType.Int, 10f, UnitStatType.MaxMana, 100f, UnitStatType.Mana, true);
        AddStat(UnitStatType.Dmg, 50f);
        AddStat(UnitStatType.Def, 0f);
        AddStat(UnitStatType.Spd, 300f);
        AddLinkedStats(UnitStatType.AmmoCapcity, 10f, UnitStatType.Ammo);
        //Debug.Log(unit_stats.Count);
        _intialised = true; 
    }
    
    public void Awake()
    {
        unit_stats = new Dictionary<UnitStatType, UnitStat>();
        unit_resources = new Dictionary<UnitStatType, UnitResource>();
        PopulateStats();
    }

}
