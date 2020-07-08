using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Actions : MonoBehaviour
{
    public Unit_Statistics unit_Stats;
    // Start is called before the first frame update
    void Start()
    {
        unit_Stats = GetComponent<Unit_Statistics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        UnitTrackedStat currentHealth = unit_Stats.GetTrackedStat(UnitStatType.Health);
        currentHealth.value -= (int)damage;

        Debug.Log(string.Format("{0} damaged for {1}, current health {2}", gameObject.name, damage, currentHealth.value));
        
        if (currentHealth.value <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
