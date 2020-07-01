using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BaseUnit
{
    public struct UnitStats{
        public int vitality;
        public int intellegence;
        public int damage;
        public int defence;
        public int speed;
        public UnitStats(int vit, int intel, int dmg, int def, int spd)
        {
            vitality = vit;
            intellegence = intel;
            damage = dmg;
            defence = def;
            speed = spd;
        }
    }
    
    private UnitStats stats;
    
    
    public BaseUnit()
    {
        SetStats(new UnitStats(10, 10, 1, 1, 10));
    } 

    public BaseUnit(UnitStats unitStats)
    {
        SetStats(unitStats);
    }

    public UnitStats GetStats()
    {
        return stats;
    }

    public void SetStats(UnitStats unitStats)
    {
        stats = unitStats;
    }

}
