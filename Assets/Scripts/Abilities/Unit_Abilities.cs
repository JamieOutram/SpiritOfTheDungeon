using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;

public class Unit_Abilities : MonoBehaviour
{
    public int Count { get; private set; }

    [SerializeField] private List<Ability> editorAbilities;
    private Dictionary<string, Ability> abilities; 

    void Awake()
    {
        abilities = new Dictionary<string, Ability>();
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

    
}
