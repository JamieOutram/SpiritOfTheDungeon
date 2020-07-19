using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class InteractionManager
{
    private static List<Tuple<string,string>> DamageInteractions = new List<Tuple<string, string>>()
    {
        Tuple.Create("Enemy", "Team"),
        Tuple.Create("Team", "Enemy"),
        Tuple.Create("Team", "BreakableObject"),
        Tuple.Create("EnvironmentEffect", "Team"),
        Tuple.Create("EnvironmentEffect", "Enemy"),
        Tuple.Create("EnvironmentEffect", "BreakableObject"),
        Tuple.Create("Enemy", "Shield"),
        Tuple.Create("Team", "Shield"),
    };

    private static List<Tuple<string, string>> BlockInteractions = new List<Tuple<string, string>>()
    {
        Tuple.Create("Enemy", "Wall"),
        Tuple.Create("Team", "Wall"),
        Tuple.Create("EnvironmentEffect", "Wall"),
    };

    public static bool IsDamaged(GameObject casterObject, GameObject targetObject, 
        bool isFreindlyFire = false, bool isSelfTarget = false)
    {   
        if(ReferenceEquals(casterObject, targetObject))
        {
            //If objects are same instance and self target return true
            if (isSelfTarget) return true;
        }
        else if (casterObject.tag == targetObject.tag)
        {
            //If tags match and freindly fire is on return true 
            if (isFreindlyFire) return true;
        }
        else {
            if (DamageInteractions.Contains(Tuple.Create(casterObject.tag, targetObject.tag))) return true;
        }
        return false;
    }

    public static bool IsBlocked(GameObject casterObject, GameObject targetObject, 
        bool isPassthroughAllies = false)
    {
        if (ReferenceEquals(casterObject, targetObject))
        {
            //If objects are same instance; do not block
        }
        else if (casterObject.tag == targetObject.tag)
        {
            //If tags match and object cannot pass through allies; block
            if (!isPassthroughAllies) return true;
        }
        else
        {
            //If objects are not the same instance and do not have the same tag
            //check interaction list and block if found
            if (BlockInteractions.Contains(Tuple.Create(casterObject.tag, targetObject.tag))) return true;
            
        }
        //do not block by defalt
        return false;
    }

    public static void AddDamageInteraction(string tag1, string tag2)
    {
        DamageInteractions.Add(Tuple.Create(tag1, tag2));
    }

    public static void RemoveDamageInteraction(string tag1, string tag2)
    {
        Tuple<string, string> interaction = Tuple.Create(tag1, tag2);
        if (DamageInteractions.Contains(interaction))
        {
            DamageInteractions.Remove(interaction);
        }
        else
        {
            Debug.LogWarning(string.Format("tuple {0}, {1} not found in Damage Interactions list", tag1, tag2));
        }
    }

    public static void AddBlockingInteraction(string tag1, string tag2)
    {
        BlockInteractions.Add(Tuple.Create(tag1, tag2));
    }

    public static void RemoveBlockingInteraction(string tag1, string tag2)
    {
        Tuple<string, string> interaction = Tuple.Create(tag1, tag2);
        if (BlockInteractions.Contains(interaction))
        {
            BlockInteractions.Remove(interaction);
        }
        else
        {
            Debug.LogWarning(string.Format("tuple {0}, {1} not found in Damage Interactions list", tag1, tag2));
        }
    }
}
