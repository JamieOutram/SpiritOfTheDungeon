using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use by move enable and disable rather than destroying and creating more. 
public class PopupInfoBox
{
    public static GameObject infoBox;
    public static GameObject itemPrefab;
    public static GameObject statPrefab;
    public static GameObject resourcePrefab;

    public List<GameObject> prefabInstances;

    private GameObject target;
    private Unit_Items items;
    private Unit_Statistics stats;

    public static void LoadResources()
    {
        //TODO: May relocate in future;
        infoBox = Resources.Load("UnitInfoPopup", typeof(GameObject)) as GameObject;
        itemPrefab = Resources.Load("UnitInfoPopupItem", typeof(GameObject)) as GameObject;
        statPrefab = Resources.Load("UnitInfoPopupStat", typeof(GameObject)) as GameObject;
        resourcePrefab = Resources.Load("UnitInfoPopupResource", typeof(GameObject)) as GameObject;
    }

    public PopupInfoBox(GameObject unitObj, Transform parent)
    {
        if (ReferenceEquals(infoBox, null))
        {
            throw new Exception("Resources for PopupInfoBox are not yet loaded.");
        }

        //Create instance of popup info box here
        infoBox = GameObject.Instantiate(infoBox, parent);
        
        target = unitObj;
        items = target.GetComponent<Unit_Items>();
        stats = target.GetComponent<Unit_Statistics>();
        prefabInstances = new List<GameObject>();
        UpdateContents();
        UpdatePosition();
        ShowBox();

    }

    public void MoveBox(GameObject newTargetObj)
    {

    }

    public void UpdatePosition()
    {

    }

    public void ShowBox()
    {

    }

    public void HideBox()
    {

    }

    private void InvalidTarget()
    {
        HideBox();
        Debug.LogError("Target of PopupInfo is invalid");
    }

    private void UpdateContents() 
    {
        if (ReferenceEquals(items, null) || ReferenceEquals(stats, null))
        {
            InvalidTarget();
            return;
        }

        //TODO: Remove exsisting children bar Title

        //Spawn stat ui prefabs
        List<Tuple<UnitStatType, int>> valueList = stats.GetAllStatValues();
        for (int i = 0; i < valueList.Count; i++)
        {
            GameObject instance = GameObject.Instantiate(statPrefab, infoBox.transform);
            //TODO: Set Values here
            prefabInstances.Add(instance);
        }
        valueList.Clear();
        
        //Spawn resource ui prefabs
        valueList = stats.GetAllResourceValues();
        for (int i = 0; i < valueList.Count; i++)
        {
            GameObject instance = GameObject.Instantiate(resourcePrefab, infoBox.transform);
            //TODO: Set values here
            prefabInstances.Add(instance);
        }
        valueList.Clear();

        //Spawn item ui prefabs
        List<EquipableItem> itemList = items.GetAllItems();
        Debug.Log(itemList.Count);
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject instance = GameObject.Instantiate(itemPrefab, infoBox.transform);
            //TODO: Set values here
            prefabInstances.Add(instance);
        }


    }
}
