﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetQuickHealEffect : UnitTargetEffect
{   
    private Unit_Statistics unitStats;

    // Start is called before the first frame update
    void Start()
    {
        if (target.GetComponent<Unit_Statistics>() != null)
        {
            UnitResource currentHealth = target.GetComponent<Unit_Statistics>().GetResource(UnitStatType.Health);

            currentHealth.Value += damage;
        }
        else
        {
            Debug.LogError(string.Format("Target: {0} has no Unit_Statistics component",target.name));
        }
    }

}
