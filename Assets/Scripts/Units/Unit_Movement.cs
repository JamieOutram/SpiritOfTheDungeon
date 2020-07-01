using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit_Movement_Control_Base))]
public class Unit_Movement : MonoBehaviour
{
    //TODO:Move speed to stats class/calculate from stats class
    public float speed = 3f;
    public float turnRate = 200f;

    Unit_Movement_Control_Base ctr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        ctr = gameObject.GetComponent<Unit_Movement_Control_Base>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //set speed of character to direction pressed
        rb.velocity = new Vector2(ctr.horizontalMove * speed, ctr.verticalMove * speed);
        
        //rotate character in direction pressed
        rb.angularVelocity = -ctr.turnMove * turnRate;
        
        //what to do if no input detected
        if (ctr.horizontalMove == 0 && ctr.verticalMove == 0 && ctr.turnMove == 0)
        {
            //Debug.Log("No Input");
        }
        
    }
}
