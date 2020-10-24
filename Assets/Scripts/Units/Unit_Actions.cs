using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

// Define a class to hold custom event info
public class OnDamageArgs : EventArgs
{
    public OnDamageArgs(UnitResource health, int damage)
    {
        Health = health;
        Damage = damage;
    }

    public UnitResource Health { get; set; }
    public int Damage { get; set; }
}

[RequireComponent(typeof(Unit_Statistics))]
[RequireComponent(typeof(Animator))]
public class Unit_Actions : MonoBehaviour
{
    public event EventHandler<OnDamageArgs> OnDamageHandler;
    private HealthBarBehaviour healthBar;
    private Unit_Statistics unit_Stats;
    private Unit_Abilities unit_Abilities;
    private Unit_Items unit_Items;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {

        unit_Stats = GetComponent<Unit_Statistics>();
        unit_Abilities = GetComponent<Unit_Abilities>();
        unit_Items = GetComponent<Unit_Items>();
        anim = GetComponent<Animator>();
    }

    public void Attack(Ability ability)
    {
        if (ability == null)
            return;

        if (unit_Abilities.GetCooldownLeftSeconds(ability.aName) == 0f)
        {
            anim.SetBool("Attack", true);
            if (ability.GetType() == typeof(UnitTargetAbility))
            {
                unit_Abilities.TryUseAbility(ability.aName, Targeting.GetClosestUnitTarget(this.transform, ability));
            }
            else
            {
                unit_Abilities.TryUseAbility(ability.aName);
            }
        }
    }

    public void Damage(int damage, Transform source = null, bool isBlockable = true)
    {
        UnitResource currentHealth = unit_Stats.GetResource(UnitStatType.Health);
        //Check or apply target effects here. Basic modifiers from source object should be applied through damage.
        //Directional Damage
        if (!ReferenceEquals(source, null))
        {
            if (isBlockable)
            {
                EquipableItem item = unit_Items.GetItemOfType(EquipableItemType.Shield);
                if (!ReferenceEquals(item, null))
                {
                    damage = item.DamageModTrigger(damage, source);
                }
            }
        }



        currentHealth.Value -= damage;
        //Trigger any subscribed events (null if none)
        if (OnDamageHandler != null)
        {
            OnDamageHandler.Invoke(gameObject, new OnDamageArgs(currentHealth, damage));
        }

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
