using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Test : Unit_Control_Base
{

    public Animator animator;
    [SerializeField] private Ability ability;

    private bool isHeld = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ability.Initialize(gameObject);
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

        if (isHeld)
        {
            isHeld = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            animator.SetBool("Attack", true);
            isHeld = true;

            ability.TriggerAbility();
            //Debug.Log("KeyDownDetected");
        }
        


    }
}
