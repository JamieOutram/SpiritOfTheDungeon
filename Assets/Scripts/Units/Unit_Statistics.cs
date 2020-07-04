using System.Collections;
using System.Collections.Generic;
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
    public Dictionary<UnitStatType, UnitStat> unit_stats = new Dictionary<UnitStatType, UnitStat>();

    public Dictionary<UnitStatType, TrackedUnitStat> unit_resources = new Dictionary<UnitStatType, TrackedUnitStat>();

    private void AddStat(UnitStatType type, float bValue)
    {
        unit_stats.Add(type, new UnitStat(bValue, type));
    }
    private void AddStat(UnitStatType type, float bValue, UnitStatType linkedType)
    {
        
        unit_stats.Add(type, new DerivedUnitStat(bValue, type, unit_stats[linkedType]));
    }
    private void AddTrackedStat(UnitStatType type, int bValue)
    {
        unit_resources.Add(type, new TrackedUnitStat(bValue, type));
    }

    private void PopulateStats()
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
        AddTrackedStat(UnitStatType.Health, unit_stats[UnitStatType.MaxHealth].Value);
        AddTrackedStat(UnitStatType.Mana, unit_stats[UnitStatType.MaxMana].Value);
        AddTrackedStat(UnitStatType.Ammo, 0);
    }
    

    public void Start()
    {
        PopulateStats();


    }

}
