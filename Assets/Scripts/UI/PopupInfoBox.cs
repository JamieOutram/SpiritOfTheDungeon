using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

//Use by move enable and disable rather than destroying and creating more. 
public class PopupInfoBox
{

    public bool isHidden { get; private set; }
    public static bool isLoaded { get; private set; } = false;

    private static GameObject infoBox;
    private static GameObject itemPrefab;
    private static GameObject statPrefab;
    private static GameObject resourcePrefab;

    private List<GameObject> prefabInstances;

    private GameObject target;
    private Unit_Items items;
    private Unit_Statistics stats;
    private bool _isDirty = false;
    private int statCount = 0;
    private int resourceCount = 0;
    private int itemCount = 0;

    public static void LoadResources(bool force = false)
    {
        Debug.Log("Loading PopupInfoBox Resources...");
        if (!force && isLoaded)
        {
            Debug.LogWarning("PopupInfoBox already Loaded.");
            return;
        }

        //TODO: May relocate in future;
        infoBox = Resources.Load("UnitInfoPopup", typeof(GameObject)) as GameObject;
        itemPrefab = Resources.Load("UnitInfoPopupItem", typeof(GameObject)) as GameObject;
        statPrefab = Resources.Load("UnitInfoPopupStat", typeof(GameObject)) as GameObject;
        resourcePrefab = Resources.Load("UnitInfoPopupResource", typeof(GameObject)) as GameObject;
        isLoaded = true;

    }

    public PopupInfoBox(GameObject unitObj, Transform parent)
    {
        Debug.Log("Spawning InfoBox...");
        CheckLoaded();

        //Create instance of popup info box here
        infoBox = GameObject.Instantiate(infoBox, parent);

        prefabInstances = new List<GameObject>();
        target = unitObj;

        HideBox();

    }

    public void ChangeTarget(GameObject newTargetObj, bool forceUpdate = false)
    {
        if (!ReferenceEquals(newTargetObj, target) || forceUpdate)
        {
            target = newTargetObj;
            UpdateContents();
            UpdatePosition();
        }
    }

    //Displays Popup with passed object info
    public void ShowBox(GameObject target, bool forceUpdate = false)
    {
        ShowBox();
        ChangeTarget(target, forceUpdate);
    }

    public void ShowBox()
    {
        infoBox.SetActive(true);
    }

    public void HideBox()
    {        
        infoBox.SetActive(false);
    }

    private void UpdatePosition()
    {
        //Ensure size fitter content is up to date
        Canvas.ForceUpdateCanvases();

        //Set position to mouse location
        infoBox.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //TODO: add logic to prevent displaying outside canvas space
        //Clamp to Canvas
        RectTransform infoBoxRect = (RectTransform)infoBox.transform;
        RectTransform canvasRect = (RectTransform)infoBox.transform.parent;

        UIUtils.ClampRectToRect(infoBoxRect, canvasRect);



    }


    private void InvalidTarget()
    {
        HideBox();
        Debug.LogError("Target of PopupInfo is invalid");
    }

    private void UpdateContents()
    {

        if (!infoBox.activeSelf)
        {
            Debug.LogException(new Exception("UpdateContents requires gameobject to be active"));
        }

        items = target.GetComponent<Unit_Items>();
        stats = target.GetComponent<Unit_Statistics>();
        if (ReferenceEquals(items, null) || ReferenceEquals(stats, null))
        {
            InvalidTarget();
            return;
        }




        List<UnitStat> statList = stats.GetAllStats();
        List<UnitResource> resourceList = stats.GetAllResources();
        List<EquipableItem> itemList = items.GetAllItems();

        //Remove exsisting children bar Title
        ClearClones();
        //TODO: CouldOnly Remove as many as nessecary

        //Set UnitName
        Text UnitName = infoBox.GetComponentInChildren<Text>();
        UnitName.text = target.name;

        //Spawn stat ui prefabs
        //Only Remove as many as nessecary
        int j = 0;
        SpawnPrefabs(statPrefab, statList.Count);
        for (int i = j; i < j + statList.Count; i++)
        {
            //TODO: Set Values here
            Text text = prefabInstances[i].GetComponent<Text>();
            text.text = string.Format("{0} : {1}", statList[i].statType.ToString(), statList[i].value);
        }
        statCount = statList.Count;
        statList.Clear();


        //Spawn resource ui prefabs
        j += statCount;
        SpawnPrefabs(resourcePrefab, resourceList.Count);
        for (int i = j; i < j + resourceList.Count; i++)
        {
            //TODO: Set values here
            Text text = prefabInstances[i].GetComponent<Text>();
            text.text = string.Format("{0} : {1}/{2}", resourceList[i - j].type.ToString(), resourceList[i - j].Value, resourceList[i - j].maxValue);
            
        }
        resourceCount = resourceList.Count;
        resourceList.Clear();


        //Spawn item ui prefabs
        //Debug.Log(itemList.Count);
        j += resourceCount;
        SpawnPrefabs(itemPrefab, itemList.Count);
        for (int i = j; i < j + itemList.Count; i++)
        {
            //TODO: Set values here
            //TODO: could optimise by moving to prefab and referencing accessor rather than searching all children?
            Text text = prefabInstances[i].GetComponentInChildren<Text>();
            IconSwitcher icon = prefabInstances[i].GetComponent<IconSwitcher>();
            text.text = string.Format("{0}\n{1}", itemList[i - j].aName, itemList[i - j].aDescription);
            icon.SwitchIcon(itemList[i - j].aIcon);
        }
        itemCount = itemList.Count;
        itemList.Clear();

    }

    private void SpawnPrefabs(GameObject prefab, int x)
    {
        AddXofPrefab(x, prefab);
    }

    private void AddXofPrefab(int x, GameObject prefab)
    {
        for (int i = 0; i < x; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab, infoBox.transform);
            //instance.transform.SetSiblingIndex(i);
            prefabInstances.Add(instance);
        }
    }


    //private void DestroyExcessClones(GameObject prefab, int destroyIndex) 
    //{
    //    string cloneName = prefab.name + "(Clone)";
    //    Debug.Log(string.Format("Checking for {0} at {1}", cloneName, destroyIndex));
    //    while (cloneName == prefabInstances[destroyIndex].name)
    //    {
    //        Debug.Log("Destroying clone");
    //        GameObject.Destroy(prefabInstances[destroyIndex]);
    //        prefabInstances.RemoveAt(destroyIndex);
    //    }
    //}

    private void ClearClones()
    {
        for (int i = prefabInstances.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(prefabInstances[i]);
            prefabInstances.RemoveAt(i);
        }
    }

    private void CheckLoaded()
    {
        if (!isLoaded)
            Debug.LogException(new Exception("Resources for PopupInfoBox are not yet loaded."));
    }
}
