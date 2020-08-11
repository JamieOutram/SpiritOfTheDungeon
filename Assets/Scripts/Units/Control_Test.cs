using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Control_Test : Unit_Control_Base
{

    public Animator animator;

    [SerializeField] private Ability activeAbility;
    public bool isUnitTarget = false;
    public KeyCode attackButton = KeyCode.Space;
    
    private Unit_Actions actions;
    private Unit_Abilities abilities;
    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponent<Unit_Actions>();
        abilities = GetComponent<Unit_Abilities>();
    }


    void Update()
    {
        //time deltatime is the time from last frame, makes equal on all systems

        if (Input.GetKey("w")) verticalMove = 1;
        else if (Input.GetKey("s")) verticalMove = -1;
        else verticalMove = 0;

        if (Input.GetKey("d")) horizontalMove = 1;
        else if (Input.GetKey("a")) horizontalMove = -1;
        else horizontalMove = 0;
        
        if (Input.GetKey("e")) turnMove = 1;
        else if (Input.GetKey("q")) turnMove = -1;
        else turnMove = 0;


        if (Input.GetKeyDown(attackButton)) 
        {
            actions.Attack(abilities.GetAbility(count));
            count++;
            if(count == abilities.Count)
            {
                count = 0;
            } 
        }
        


    }
}
