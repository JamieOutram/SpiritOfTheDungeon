using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Items : ScriptableObjectManager<EquipableItem>
{
    void Awake()
    {
        InitializeObjects();
    }

}
