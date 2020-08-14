using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatSetter : MonoBehaviour
{
    Unit_Statistics unit_Statistics;
    public List<UnitStatType> unitStatTypes;
    public List<int> newBaseStats;
    private bool _abort;
    
    // Start is called before the first frame update
    void Awake()
    {
        _abort = false;
        unit_Statistics = GetComponent<Unit_Statistics>();
        if (unitStatTypes.Count != newBaseStats.Count)
        {
            Debug.LogError("StatSetter List Lengths must match");
            _abort = true;
        }
    }

    void Start()
    {
        if (!_abort)
        {
            for (int i = 0; i < unitStatTypes.Count; i++)
            {
                UnitStat unitStat = unit_Statistics.GetStat(unitStatTypes[i]);
                unitStat.BaseValue = newBaseStats[i];
                Debug.Log(string.Format("{0} {1} set to {2}",gameObject.name, unitStatTypes[i].ToString(), unit_Statistics.GetStat(unitStatTypes[i]).BaseValue));
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
