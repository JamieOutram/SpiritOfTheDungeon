using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberMeta
{
    public UnitType unitType;
    public List<EquipableItem> items; //scriptable objects only instantiated during fight
    public List<Ability> abilityList; // "
    public List<(UnitStatType, int)> statList; //Units stats types and base values


    PartyMemberMeta()
    {
        unitType = UnitType.EnemyBasic;
        items = new List<EquipableItem>();
        abilityList = new List<Ability>();
        statList = new List<(UnitStatType, int)>();
    }

    PartyMemberMeta(UnitType unitType, List<EquipableItem> itemList, List<Ability> abilityList, List<(UnitStatType, int)> statList)
    {
        this.unitType = unitType;
        this.items = itemList;
        this.abilityList = abilityList;
        this.statList = statList;
    }
}
