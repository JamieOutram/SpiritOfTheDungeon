using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
    SpriteMask mask;
    Vector3 startScale;
    // Start is called before the first frame update
    void Awake()
    {
        mask = GetComponentInChildren<SpriteMask>();
        startScale = mask.transform.localScale;
        transform.parent.gameObject.GetComponent<Unit_Actions>().OnDamageHandler += UpdateHealthBar;
    }


    void UpdateHealthBar(object sender, OnDamageArgs e)
    {
        
        //Calculate mask scale from max health and current health. 
        Vector3 newScale = startScale;
        Debug.Log(string.Format("Scale is {0}", newScale));
        Debug.Log(string.Format("Health is {0}/{1}", e.Health.Value, e.Health.maxValue));
        Debug.Log(string.Format("Health is {0}, {1}", e.Health.Value/e.Health.maxValue, startScale.x));
        newScale.x = startScale.x * ((float)e.Health.Value / e.Health.maxValue);
        Debug.Log(string.Format("Setting scale to {0}", newScale));
        mask.transform.localScale = newScale;
    }
    // Update is called once per frame
    
}
