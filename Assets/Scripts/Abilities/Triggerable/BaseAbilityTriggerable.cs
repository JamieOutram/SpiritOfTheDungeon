using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityTriggerable : MonoBehaviour
{
    protected Unit_Statistics unitStats;

    public void Awake()
    {
        unitStats = gameObject.GetComponent<Unit_Statistics>();
    }

    public virtual void fire(Ability ability)
    {
        Debug.LogError("No Implementation for fire");
    }

    public virtual void fire(Ability ability, Transform target)
    {
        Debug.LogError("No Implementation for fire");
    }

}
