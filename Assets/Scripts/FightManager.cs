using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Controls the state of the current fight and allows easy reseting
public class FightManager : MonoBehaviour
{
    public MapMeta map;
    public PartyMeta heroParty;
    public PartyMeta enemyParty;

    GameObject[] unitPrefabs;

    bool isPartyInitialized = false;

    void LoadResources()
    {
        unitPrefabs = (GameObject[])Resources.LoadAll("Units/");
    }

    void LoadParty(PartyMeta party, Vector2[] spawnPoints)
    {
        int sizeLimit = party.Size;
        if (party.Size > spawnPoints.Length)
        {
            sizeLimit = spawnPoints.Length;
            Debug.LogError("Not Enough Spawn points for the number of units to be spawned");
        }

        for (int i =0; i<sizeLimit; i++)
        {
            GameObject prefabUnit = unitPrefabs.SingleOrDefault(x => x.name == UnitTypeMap.GetString(party.members[i].unitType));
            //Spawn Blank Units
            GameObject newUnit = Instantiate(prefabUnit, transform);
            newUnit.transform.position = spawnPoints[i];
            //Erase any exsiting abilities and items on the prefab
            var abilities = newUnit.GetComponent<Unit_Abilities>();
            var items = newUnit.GetComponent<Unit_Items>();
            var stats = newUnit.GetComponent<Unit_Statistics>();
            abilities.CleanObjects();
            items.CleanObjects();
            //Apply items, stats and abilities
            foreach(EquipableItem item in party.members[i].items)
            {
                items.AddItem(item);
            }
            foreach(Ability ability in party.members[i].abilityList)
            {
                abilities.AddAbility(ability);
            }
            foreach((UnitStatType,int) stat in party.members[i].statList)
            {
                //Set base stat values
                stats.GetStat(stat.Item1).BaseValue = stat.Item2;
            }
            //Restore resources to max (TODO: could use saved values from previous fight)
            List<UnitResource> resources = stats.GetAllResources();
            foreach (UnitResource resource in resources)
            {
                resource.Value = resource.maxValue;
            }

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
