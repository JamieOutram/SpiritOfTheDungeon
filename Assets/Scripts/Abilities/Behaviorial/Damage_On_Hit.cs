using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is where the attack can damage enemies and the like.
public class Damage_On_Hit : MonoBehaviour
{

    int damage = 50; //TEMP

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        //Debug.Log("OnTriggerEnter2D called");

        if (otherObj.CompareTag("HitableEnemy")) 
        {
            otherObj.GetComponent<Unit_Actions>().Damage(damage);
        }
    }
}
