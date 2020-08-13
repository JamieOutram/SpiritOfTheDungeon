using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : Unit_Control_Base
{
    public float lookRadius = 4f;
    public float speed = 500f;
    public float attackRange = 1.5f;
    public float attackInterval = 2f;
    public float turnRate = 30f;
    public float pathUpdateRate = 0.5f;
    public Transform target;

    private Rigidbody2D rb;
    private Path path;
    private Seeker seeker;
    [SerializeField] private float nextWaypointDistance = 0.3f;
    private Unit_Abilities abilities;
    private int currentWaypoint = 0;
    private bool isReachedEndOfPath = false;


    // Start is called before the first frame update
    void Awake()
    {
        abilities = GetComponent<Unit_Abilities>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            target = GetClosestOpposition();
            if(target != null) { 
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
    }

    Transform GetClosestOpposition()
    {
        //might be causing memory leak, see OverlapCircleNonAlloc
        List<Collider2D> colliders = Physics2D.OverlapCircleAll((Vector2)this.transform.position, lookRadius).ToList();

        //filter valid colliders only, reverse itteration to avoid indexing errors
        for (int i = colliders.Count - 1; i > -1; i--)
        {
            if (!InteractionManager.IsDamaged(this.gameObject, colliders[i].gameObject))
            {
                colliders.RemoveAt(i);
            }
        }

        if (colliders.Count > 0)
        {

            Collider2D closestCollider = colliders[0];

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

            return closestCollider.transform;
        }
        else
        {
            //Debug.Log("No target in range");
            return null;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FollowPath()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            isReachedEndOfPath = true;
            return;
        }
        else
        {
            isReachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        Vector3 newDirRight = Vector3.RotateTowards(transform.right, direction, turnRate*Time.deltaTime, 0f);
        //rotate 90 degrees to get vertical rotation
        Vector3 newDirUp = Quaternion.Euler(0,0,90) * newDirRight ;
        
        rb.SetRotation(Quaternion.LookRotation(Vector3.forward, newDirUp));
        

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void Attack()
    {
        abilities.TryUseAbility(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            FollowPath();

            //Check whether in attack range 
            float distanceToTarget = Vector2.Distance(rb.position, target.position);
            if (distanceToTarget < attackRange && !IsInvoking("Attack"))
            {
                Invoke("Attack", attackInterval);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
