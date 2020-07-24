using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit_Control_Base
{
    public float lookRadius;
    public float speed = 500f;
    public float attackRange = 1.5f;
    public float attackInterval = 2f;
    public float turnRate = 0.01f;
    public float pathUpdateRate = 0.5f;
    public Transform target;

    private Rigidbody2D rb;
    private Path path;
    private Seeker seeker;
    [SerializeField] private float nextWaypointDistance = 0.3f;
    [SerializeField] private Ability ability;
    private int currentWaypoint = 0;
    private bool isReachedEndOfPath = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
        ability.Initialize(gameObject);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
        ability.TriggerAbility();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();

        //Check whether in attack range 
        float distanceToTarget = Vector2.Distance(rb.position, target.position);
        if (distanceToTarget < attackRange && !IsInvoking("Attack"))
        {
            Invoke("Attack", attackInterval);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
