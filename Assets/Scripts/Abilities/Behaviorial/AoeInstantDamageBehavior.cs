using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is where the attack can damage enemies and the like.
public class AoeInstantDamageBehavior : AoeBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        //Debug.Log("OnTriggerEnter2D called");

        if (InteractionManager.IsDamaged(casterObj, otherObj.gameObject)) 
        {
            otherObj.GetComponent<Unit_Actions>().Damage(damage);
        }
    }
}
