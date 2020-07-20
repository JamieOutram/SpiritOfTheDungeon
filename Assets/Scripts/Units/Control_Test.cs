using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Control_Test : Unit_Control_Base
{

    public Animator animator;
    [SerializeField] private Ability ability;
    public bool isUnitTarget = false;
    public KeyCode attackButton = KeyCode.Space;
    private bool isHeld = false;
    
    // Start is called before the first frame update
    void Start()
    {
        /*Instantiating a clone of the ability allows multiple instances 
         *of the same scriptable object in the scene */
        ability = Instantiate(ability); 
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
        if (Input.GetKeyDown(attackButton)) 
        {
            animator.SetBool("Attack", true);
            isHeld = true;
            if (isUnitTarget)
            {
                //might be causing memory leak, see OverlapCircleNonAlloc
                List<Collider2D> colliders = Physics2D.OverlapCircleAll((Vector2)this.transform.position, ability.aRange).ToList();

                //filter valid colliders only, reverse itteration to avoid indexing errors
                for (int i = colliders.Count - 1; i > -1; i--)
                {
                    if (!InteractionManager.IsHealed(this.gameObject, colliders[i].gameObject))
                    {
                        colliders.RemoveAt(i);
                    }
                }

                if (colliders.Count > 0)
                {
                    
                    Collider2D closestCollider = colliders[0];
                    Debug.Log(closestCollider.name);
                    float magnitude = (gameObject.transform.position - closestCollider.transform.position).magnitude;
                    float lowestMagnitude = magnitude;
                    if (ReferenceEquals(this.gameObject, closestCollider.gameObject))
                    {
                        lowestMagnitude = 999;
                    }

                    foreach (var collider in colliders)
                    {
                        magnitude = (gameObject.transform.position - collider.transform.position).magnitude;
                        if (!ReferenceEquals(this.gameObject, collider.gameObject))
                        {
                            if ((magnitude < lowestMagnitude))
                            {
                                closestCollider = collider;
                                lowestMagnitude = magnitude;
                            }
                        }
                    }
                   
                    ability.TriggerAbility(closestCollider.gameObject);
                }
                else
                {
                    Debug.Log("No target in range");
                }
            }
            else
            {
                ability.TriggerAbility();
            }
            
            
            //Debug.Log("KeyDownDetected");
        }
        


    }
}
