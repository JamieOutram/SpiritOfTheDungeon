using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using UnityEngine;

public enum UnitStatType
{
    None,
    Agi,
    Int,
    Str,
    Spd,
    PhysDmgAmp,
    MagiDmgAmp,
    PhysDmgFlat,
    MagiDmgFlat,
    PhysArmour,
    PhysBlock,
    MagiArmour,
    MagiBlock,
    MaxHealth,
    MaxMana,
    Health,
    Mana,
    Ammo,
    AmmoCapcity,
    MagiDefPercent,
    PhysDefPercent,
}

public class Unit_Statistics : MonoBehaviour
{
    //List of stats not shown to user, only hidden if isDebug = false
    public static UnitStatType[] hiddenTypes = {
        UnitStatType.MagiBlock,
        UnitStatType.MagiDefPercent,
        UnitStatType.MagiDmgAmp,
        UnitStatType.MagiDmgFlat,
        UnitStatType.PhysBlock,
        UnitStatType.PhysDefPercent,
        UnitStatType.PhysDmgAmp,
        UnitStatType.PhysDmgFlat,
        UnitStatType.MaxHealth,
        UnitStatType.MaxMana,
        UnitStatType.AmmoCapcity,
    };

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

    public List<UnitStat> GetAllStats()
    {
        return unit_stats.Values.ToList();
    }
    public List<UnitResource> GetAllResources()
    {
        return unit_resources.Values.ToList();
    }

    public List<Tuple<UnitStatType, int>> GetAllStatValues() 
    {
        List<Tuple<UnitStatType, int>> statValues = new List<Tuple<UnitStatType, int>>();
        foreach(UnitStatType key in unit_stats.Keys)
        {
            statValues.Add(Tuple.Create(key, GetStat(key).Value));
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

    private void AddLinkedStat(UnitStatType type,
        UnitStatType derivedType, float dBaseValue)
    {
        DerivedUnitStat newStat = new DerivedUnitStat(dBaseValue, derivedType);
        unit_stats[type].AddDerivedStat(newStat);
        unit_stats.Add(derivedType, newStat);
    }

    private void AddLinkedResource(UnitStatType parent, UnitStatType resource)
    {
        unit_stats[parent].LinkedResource = new UnitResource(resource);
        unit_resources.Add(resource, unit_stats[parent].LinkedResource);
    }

    protected virtual void PopulateStats()
    {
        //Debug.Log("PopulateStats Called");
        //Add All derived stats and resources after added stat
        //Base Stats
        //Agility
        AddStat(UnitStatType.Agi, 5f);
        AddLinkedStat(UnitStatType.Agi, UnitStatType.PhysArmour, 0f);
        AddLinkedStat(UnitStatType.PhysArmour, UnitStatType.PhysDefPercent, 0f);
        AddLinkedStat(UnitStatType.Agi, UnitStatType.PhysBlock, 0f);
        AddLinkedStat(UnitStatType.Agi, UnitStatType.Spd, 300f);

        //Intellegence
        AddStat(UnitStatType.Int, 5f);
        AddLinkedStat(UnitStatType.Int, UnitStatType.MaxMana, 100f);
        AddLinkedResource(UnitStatType.MaxMana, UnitStatType.Mana);
        AddLinkedStat(UnitStatType.Int, UnitStatType.MagiDmgAmp, 0f);
        AddLinkedStat(UnitStatType.Int, UnitStatType.MagiDmgFlat, 0f);

        //Strength
        AddStat(UnitStatType.Str, 5f);
        AddLinkedStat(UnitStatType.Str, UnitStatType.MaxHealth, 100f);
        AddLinkedResource(UnitStatType.MaxHealth, UnitStatType.Health);
        AddLinkedStat(UnitStatType.Str, UnitStatType.PhysDmgAmp, 0f);
        AddLinkedStat(UnitStatType.Str, UnitStatType.PhysDmgFlat, 0f);
        AddLinkedStat(UnitStatType.Int, UnitStatType.MagiArmour, 0f);
        AddLinkedStat(UnitStatType.MagiArmour, UnitStatType.MagiDefPercent, 0f);
        AddLinkedStat(UnitStatType.Int, UnitStatType.MagiBlock, 0f);

        //Other
        AddStat(UnitStatType.AmmoCapcity, 10f);
        AddLinkedResource(UnitStatType.AmmoCapcity, UnitStatType.Ammo);
        
        //Debug.Log(unit_stats.Count);
        _intialised = true; 
    }
    
    public void Awake()
    {
        if (Constants.isDebug) hiddenTypes = new UnitStatType[0];
        unit_stats = new Dictionary<UnitStatType, UnitStat>();
        unit_resources = new Dictionary<UnitStatType, UnitResource>();
        PopulateStats();
    }
}
