using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
    private enum Child
    {
        ManaBar,
        HealthBar,
    }

    private enum ChildOfChild
    {
        Mask,
        Top,
    }

    SpriteMask healthMask;
    SpriteMask manaMask;
    SpriteRenderer healthBarRenderer;
    Vector3 startScaleHealth;
    Vector3 startScaleMana;
    // Start is called before the first frame update
    void Awake()
    {
        manaMask = transform.GetChild((int)Child.ManaBar).GetComponentInChildren<SpriteMask>();
        healthMask = transform.GetChild((int)Child.HealthBar).GetComponentInChildren<SpriteMask>();
        healthBarRenderer = transform.GetChild((int)Child.HealthBar).GetChild((int)ChildOfChild.Top).GetComponent<SpriteRenderer>();
        startScaleHealth = healthMask.transform.localScale;
        startScaleMana = manaMask.transform.localScale;
        transform.parent.gameObject.GetComponent<Unit_Actions>().OnDamageHandler += UpdateHealthBar;
        transform.parent.gameObject.GetComponent<Unit_Abilities>().OnCastHandler += UpdateManaBar;
    }


    void UpdateHealthBar(object sender, OnDamageArgs e)
    {
        
        //Calculate mask scale from max health and current health. 
        Vector3 newScale = startScaleHealth;
        float healthRatio = (float)e.Health.Value / e.Health.maxValue;
        //Debug.Log(string.Format("Scale is {0}", newScale));
        //Debug.Log(string.Format("Health is {0}/{1}", e.Health.Value, e.Health.maxValue));
        //Debug.Log(string.Format("Health is {0}, {1}", e.Health.Value/e.Health.maxValue, startScale.x));
        newScale.x = startScaleHealth.x * healthRatio;
        //Debug.Log(string.Format("Setting scale to {0}", newScale));
        healthMask.transform.localScale = newScale;
        
        healthBarRenderer.material.SetFloat("_Color", healthRatio); //Change colour
    }

    void UpdateManaBar(object sender, OnCastArgs e)
    {

        //Calculate mask scale from max health and current health. 
        Vector3 newScale = startScaleMana;
        float manaRatio = (float)e.Mana.Value / e.Mana.maxValue;
        //Debug.Log(string.Format("Scale is {0}", newScale));
        //Debug.Log(string.Format("Health is {0}/{1}", e.Health.Value, e.Health.maxValue));
        //Debug.Log(string.Format("Health is {0}, {1}", e.Health.Value/e.Health.maxValue, startScale.x));
        newScale.x = startScaleMana.x * manaRatio;
        //Debug.Log(string.Format("Setting scale to {0}", newScale));
        manaMask.transform.localScale = newScale;
    }
}
