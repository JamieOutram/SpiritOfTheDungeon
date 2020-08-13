using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeDirectionalDamageBehavior : AoeBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        //Debug.Log("OnTriggerEnter2D called");

        if (InteractionManager.IsDamaged(casterObj, otherObj.gameObject))
        {
            otherObj.GetComponent<Unit_Actions>().Damage(damage, casterObj.transform);
        }
    }
}
