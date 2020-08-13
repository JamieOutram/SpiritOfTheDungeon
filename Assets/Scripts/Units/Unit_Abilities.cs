using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;

public class Unit_Abilities : MonoBehaviour
{
    public class Cooldown
    {
        public float cooldownLeft;
        public float lastUpdateTime;
        public float baseCooldown;
        public Cooldown(float cdl, float lut, float bcd)
        {
            cooldownLeft = cdl;
            lastUpdateTime = lut;
            baseCooldown = bcd;
        }
    }
    
    public int Count { get; private set; }

    [SerializeField] private List<Ability> editorAbilities;
    private Dictionary<string, Ability> abilities;
    private Dictionary<string, Cooldown> cooldowns;



    void Awake()
    {
        abilities = new Dictionary<string, Ability>();
        cooldowns = new Dictionary<string, Cooldown>();
        Count = 0;
        InstantiateAllAbilities();
    }

    //Member functions for editing dictionary
    public void AddAbility(Ability ability)
    {
        if (!abilities.Keys.Contains(ability.aName))
        {
            ability = InstantiateAbility(ability);
            abilities.Add(ability.aName, ability);
            cooldowns.Add(ability.aName, new Cooldown(0f, Time.time, ability.baseCooldown));
            Count++;
        }
        else
        {
            Debug.LogWarning(string.Format("Duplicate ability on {0}", gameObject.name));
        }
    }
    public void RemoveAbility(string ability)
    {
        if (!abilities.Keys.Contains(ability))
        {
            Destroy(abilities[ability]);
            abilities.Remove(ability);
            cooldowns.Remove(ability);
            Count--;
        }
        else
        {
            Debug.LogWarning(string.Format("Ability could not be removed as it does not exsist", gameObject.name));
        }
    }

    //Member functions for accessing dictionary
    public Ability GetAbility(string name)
    {
        return abilities[name];
    }
    public Ability GetAbility(int index)
    {
        return abilities[abilities.Keys.ToList()[index]];
    }

    //Converts serialisable list from editor into dictionary of instantiated abilities
    private void InstantiateAllAbilities()
    {
        for(int i = 0; i<editorAbilities.Count; i++)
        {
            Ability ability = editorAbilities[i];
            AddAbility(ability);
        }
        editorAbilities.Clear();
        
    }

    //Instantiates and returns the reference to an instance of the ability passed
    private Ability InstantiateAbility(Ability ability) 
    {
        /*Instantiating a clone of the ability allows multiple instances 
         *of the same scriptable object in the scene */
        ability = Instantiate(ability);
        ability.Initialize(gameObject);
        return ability;
    }

    public void StartCooldown(string name)
    {
        cooldowns[name].lastUpdateTime = Time.time;
        cooldowns[name].cooldownLeft = cooldowns[name].baseCooldown;
        //Debug.Log(string.Format("baseCooldown: {0}", cooldowns[name].baseCooldown));
    }

    public float GetCooldownLeftSeconds(string name)
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
        if(GetCooldownLeftSeconds(name) == 0f)
        {
            StartCooldown(name);
            if (target == null)
            {
                //Debug.Log(string.Format("Using {0}", abilities[name].aName));
                abilities[name].TriggerAbility();
            }
            else
            {
                abilities[name].TriggerAbility(target);
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


}
