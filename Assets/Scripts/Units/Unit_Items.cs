using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum EquipableItemType
{
    Shield,
    Weapon,
    Armour
}

public class Unit_Items : ScriptableObjectManager<EquipableItem>
{

    void Awake()
    {
        InitializeObjects();
    }



    public EquipableItem GetItem(string name)
    {
        return GetElement(name);
    }

    public EquipableItem GetItemOfType(EquipableItemType T)
    {
        EquipableItem item;
        for (int i = 0; i<Count; i++)
        {
            item = GetElement(i);
            if (T == item.itemType)
            {
                return item;
            }
        }
        return null;
    }

    //returns list of name, icon, description for each item.
    public List<EquipableItem> GetAllItems()
    {
        List<EquipableItem> itemAttributes = new List<EquipableItem>();
        EquipableItem item;
        for (int i = 0; i<Count; i++)
        {
            item = GetElement(i);
            itemAttributes.Add(item);
        }
        return itemAttributes;
    }

}
