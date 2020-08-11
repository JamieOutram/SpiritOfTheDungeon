using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Timeline;

//The base class for all abilities
public abstract class Ability : ScriptableObject
{

    public string aName = "New Ability";
    public Sprite aSprite;
    public AudioClip aSound;
    public float baseCooldown = 1f;
    public float aRange = 3f;

    private float lastUpdateTime = 0f;
    public float cooldownLeft = 0f;
    

    public abstract void Initialize(GameObject obj);
    public virtual void TriggerAbility(){
        throw new System.Exception("No Override for TriggerAbility variant in Ability.cs");
    }

    public virtual void TriggerAbility(GameObject target)
    {
        throw new System.Exception("No Override for TriggerAbility variant in Ability.cs");
    }

    public void TriggerAbilityWithCooldown(GameObject target = null)
    {
        if(GetCooldownLeftSeconds() == 0f)
        {
            StartCooldown();
            if(target == null)
            {
                TriggerAbility();
            }
            else
            {
                TriggerAbility(target);
            }
        }
    }

    protected void StartCooldown()
    {
        lastUpdateTime = Time.time;
        cooldownLeft = baseCooldown;
        Debug.Log(string.Format("baseCooldown: {0}", baseCooldown));
    }

    public float GetCooldownLeftSeconds()
    {
        if (cooldownLeft > 0f) {
            float currentTime = Time.time;
            cooldownLeft -= (currentTime - lastUpdateTime);
            lastUpdateTime = currentTime;
            if (cooldownLeft < 0f) cooldownLeft = 0f;
        }
        Debug.Log(string.Format("{0} On Cooldown, {1} seconds left.", aName, cooldownLeft));
        return cooldownLeft;
    }
}
