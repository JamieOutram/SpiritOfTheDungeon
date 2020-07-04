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

    private Dictionary<UnitStatType, UnitTrackedStat> unit_resources = new Dictionary<UnitStatType, UnitTrackedStat>();

    //Get and set functions here obscures the dictionary structure (easy to change later)  
    public UnitStat GetStat(UnitStatType statType)
    {
        return unit_stats[statType];
    }

    public UnitTrackedStat GetTrackedStat(UnitStatType statType)
    {
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
    private void AddTrackedStat(UnitStatType type, int baseValue)
    {
        unit_resources.Add(type, new UnitTrackedStat(baseValue, type));
    }

    protected virtual void PopulateStats()
    {
        //Base Stats
        AddStat(UnitStatType.Vit, 10f);
        AddStat(UnitStatType.Int, 10f);
        AddStat(UnitStatType.Dmg, 1f);
        AddStat(UnitStatType.Def, 0f);
        AddStat(UnitStatType.Spd, 3f);

        //Add Derived Stats
        AddStat(UnitStatType.MaxHealth, 100f, UnitStatType.Vit);
        AddStat(UnitStatType.MaxMana, 10f, UnitStatType.Int);

        //Add Tracked Stats
        AddTrackedStat(UnitStatType.Health, GetStat(UnitStatType.MaxHealth).Value);
        AddTrackedStat(UnitStatType.Mana, GetStat(UnitStatType.MaxMana).Value);
        AddTrackedStat(UnitStatType.Ammo, 0);
    }
    
    public void Start()
    {
        PopulateStats();
    }

}
