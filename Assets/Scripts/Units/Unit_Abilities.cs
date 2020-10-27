using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;

[RequireComponent(typeof(UnitTargetTriggerable))]
[RequireComponent(typeof(AreaOfEffectTriggerable))]
[RequireComponent(typeof(ProjectileTriggerable))]
public class Unit_Abilities : ScriptableObjectManager<Ability>
{
    public event EventHandler<OnCastArgs> OnCastHandler;

    public class Cooldown
    {
        public float cooldownLeft;
        public float lastUpdateTime;
        public float baseCooldown;
        public Cooldown(float cLeft, float bcd)
        {
            cooldownLeft = cLeft;
            lastUpdateTime = Time.time;
            baseCooldown = bcd;
        }
    }
    
    private Dictionary<string, Cooldown> cooldowns;
    private Dictionary<string, BaseAbilityTriggerable> triggerableInstances;
    
    private UnitResource mana;

    private UnitTargetTriggerable unitTargetTrigger;
    private ProjectileTriggerable projectileTrigger;
    private AreaOfEffectTriggerable aoeTrigger;

    void Awake()
    {
        cooldowns = new Dictionary<string, Cooldown>();
        unitTargetTrigger = GetComponent<UnitTargetTriggerable>();
        projectileTrigger = GetComponent<ProjectileTriggerable>();
        aoeTrigger = GetComponent<AreaOfEffectTriggerable>();
        InitializeObjects();
        InitializeCooldowns();
    }

    void Start()
    {
        mana = GetComponent<Unit_Statistics>().GetResource(UnitStatType.Mana);
    }


    //Member functions for editing dictionary
    public void AddAbility(Ability ability)
    {
        if (AddElement(ability))
        {
            cooldowns.Add(ability.aName, new Cooldown(0f, ability.baseCooldown));
        }
    }
    public void RemoveAbility(string ability)
    {
        if (RemoveElement(ability))
        {
            cooldowns.Remove(ability);
        }
    }

    //Member functions for accessing dictionary
    public Ability GetAbility(string name)
    {
        return GetElement(name);
    }
    public Ability GetAbility(int index)
    {
        return GetElement(index);
    }

    private void InitializeCooldowns()
    {
        for(int i = 0; i<Count; i++)
        {
            cooldowns.Add(GetElement(i).aName, new Cooldown(0f, GetElement(i).baseCooldown));
        }
    }

    public void StartCooldown(string name)
    {
        cooldowns[name].lastUpdateTime = Time.time;
        cooldowns[name].cooldownLeft = cooldowns[name].baseCooldown;
        //Debug.Log(string.Format("baseCooldown: {0}", cooldowns[name].baseCooldown));
    }

    public float GetCooldownLeftSeconds(string name)
    {
        if (cooldowns.ContainsKey(name))
        {
            if (cooldowns[name].cooldownLeft > 0f)
            {
                float currentTime = Time.time;
                cooldowns[name].cooldownLeft -= (currentTime - cooldowns[name].lastUpdateTime);
                cooldowns[name].lastUpdateTime = currentTime;
                if (cooldowns[name].cooldownLeft < 0f) cooldowns[name].cooldownLeft = 0f;
            }
            //Debug.Log(string.Format("{0} On Cooldown, {1} seconds left.", name, cooldowns[name].cooldownLeft));
            return cooldowns[name].cooldownLeft;
        }
        else
        {
            return -1; //Error code: No key of name 
        }
    }

    public float GetCooldownLeftSeconds(int index)
    {
        return GetCooldownLeftSeconds(GetAbility(index).aName);
    }

    public bool IsOffCooldown(int index)
    {
        return GetCooldownLeftSeconds(GetAbility(index).aName) == 0f;
    }


    public void TryUseAbility(string name, GameObject target = null)
    {
        Ability ability = GetElement(name);
        if(GetCooldownLeftSeconds(name) == 0f)
        {
            if(mana.Value >= ability.baseManaCost) {
                StartCooldown(name);
                
                if (target == null)
                {
                    //Debug.Log(string.Format("Using {0}", abilities[name].aName));
                    TriggerAbility(ability);
                }
                else
                {
                    TriggerAbility(ability, target);
                }

                //Update mana for used ability
                mana.Value -= ability.baseManaCost;

                //Trigger any subscribed events (null if none)
                if (OnCastHandler != null)
                {
                    OnCastHandler.Invoke(gameObject, new OnCastArgs(mana, ability));
                }
            }
        }
    }
    public void TryUseAbility(Ability ability, GameObject target = null)
    {
        TryUseAbility(ability.aName, target);
    }

    public void TryUseAbility(int index, GameObject target = null)
    {
        TryUseAbility(GetAbility(index).aName, target);
    }

    private void TriggerAbility(Ability ability, GameObject target = null)
    {
        Type T = ability.GetType();
        if (T == typeof(UnitTargetAbility)) unitTargetTrigger.Fire(target, ability as UnitTargetAbility);
        else if (T == typeof(ProjectileAbility)) projectileTrigger.Fire(ability as ProjectileAbility);
        else if (T == typeof(AreaOfEffectAbility)) aoeTrigger.Fire(ability as AreaOfEffectAbility);
    }

}
