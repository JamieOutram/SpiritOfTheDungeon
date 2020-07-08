using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is where the attack can damage enemies and the like.
public class Melee_Attack_Effect_Script : MonoBehaviour
{

    int damage = 50; //TEMP
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("OnTriggerEnter2D called");

        if (otherObj.CompareTag("HitableEnemy")) 
        {
            otherObj.GetComponent<Unit_Actions>().Damage(damage);
        }
    }
}
