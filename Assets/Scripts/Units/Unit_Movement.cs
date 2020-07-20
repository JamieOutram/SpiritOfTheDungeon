using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit_Control_Base))]
[RequireComponent(typeof(Unit_Statistics))]
public class Unit_Movement : MonoBehaviour
{
    //TODO: Move speed to stats class/calculate from stats class

    private UnitStat speed;
    private UnitStat turnRate;
    private Unit_Control_Base ctr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = gameObject.GetComponent<Unit_Statistics>().GetStat(UnitStatType.Spd);
        //TODO: add stat with proper update function for turnrate
        turnRate = gameObject.GetComponent<Unit_Statistics>().GetStat(UnitStatType.Spd); 
        ctr = gameObject.GetComponent<Unit_Control_Base>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (speed == null || turnRate == null) Start();
        //set speed of character to direction pressed
        rb.velocity = new Vector2(ctr.horizontalMove * speed.Value * 2, ctr.verticalMove * speed.Value * 2);

        //rotate character in direction pressed
        rb.angularVelocity = -ctr.turnMove * turnRate.Value * 100;

        //what to do if no input detected
        if (ctr.horizontalMove == 0 && ctr.verticalMove == 0 && ctr.turnMove == 0)
        {
            //Debug.Log("No Input");
        }
    }
}
