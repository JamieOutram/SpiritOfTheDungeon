using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit_Control_Base))]
[RequireComponent(typeof(Unit_Statistics))]
public class Unit_Movement : MonoBehaviour
{
    //TODO: Move speed to stats class/calculate from stats class

    private UnitStat speed;
    private float turnRate;
    private Unit_Control_Base ctr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = gameObject.GetComponent<Unit_Statistics>().GetStat(UnitStatType.Spd);
        //TODO: add stat with proper update function for turnrate
        turnRate = 300;
        
        ctr = gameObject.GetComponent<Unit_Control_Base>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (speed == null) Start();
        //set speed of character to direction pressed
        rb.AddForce(new Vector2(ctr.horizontalMove * speed.Value * Time.deltaTime, ctr.verticalMove * speed.Value * Time.deltaTime));

        //rotate character in direction pressed
        rb.AddTorque(-ctr.turnMove * turnRate * Time.deltaTime);

        //what to do if no input detected
        if (ctr.horizontalMove == 0 && ctr.verticalMove == 0 && ctr.turnMove == 0)
        {
            //Debug.Log("No Input");
        }
    }
}
