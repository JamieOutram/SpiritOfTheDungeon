using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit_Statistics))]
[RequireComponent(typeof(Animator))]
public class Unit_Actions : MonoBehaviour
{
    private Unit_Statistics unit_Stats;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        unit_Stats = GetComponent<Unit_Statistics>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        UnitResource currentHealth = unit_Stats.GetResource(UnitStatType.Health);
        currentHealth.Value -= (int)damage;
        anim.SetBool("Hit", true);
        Debug.Log(string.Format("{0} damaged for {1}, current health {2}", gameObject.name, damage, currentHealth.Value));
        
        if (currentHealth.Value <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
