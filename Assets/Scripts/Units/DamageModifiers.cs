using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageTargetType
{
    Point,
    Directional,
    Area,
}
public enum DamageSourceType
{
    Physical,
    Magical,
    Pure,
}


//This should just manage Ability interactions.
//Item modifiers should be handled by UnitStat 
//Stat modifiers could be contained within derived stats but handled here
public static class AbilityDamageModifiers
{
    public static float GetDamage(DamageSourceType sourceType, DamageTargetType targetType, bool isOutgoing, float baseDamage = 0f, Unit_Statistics stats = null, Unit_Items items = null)
    {
        float damage = ApplyStatModifiers(baseDamage); ;
        damage = ApplyItemModifiers(damage);
        return damage;

        float ApplyItemModifiers(float dmg)
        {
            //Item flat modifiers handled by unitstat system
            //Any item specific traits need to be handled here

            return dmg;
        }

        float ApplyStatModifiers(float dmg)
        {
            //Get effecting stat value
            float statValue = 0f;
            float statToDamageScale = 0f;
            UnitStatType type = UnitStatType.None; //default no modifier
            switch (sourceType)
            {
                case DamageSourceType.Physical:
                    type = isOutgoing ? UnitStatType.Agi : UnitStatType.Def;
                    statToDamageScale = isOutgoing ? 10f : 5f;
                    break;
                case DamageSourceType.Magical:
                    type = isOutgoing ? UnitStatType.Int : UnitStatType.Str; //Vit may need substitution for other stat
                    statToDamageScale = isOutgoing ? 10f : 5f;
                    break;
                case DamageSourceType.Pure:
                    //No resistance
                    break;
            }

            //Apply damage modifier
            if (type != UnitStatType.None)
            {
                statValue = stats.GetStat(type).Value;
                dmg += statValue * statToDamageScale;
            }

            return dmg;
        }

    }
}
