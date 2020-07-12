using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Collections;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    [HideInInspector] public int damage;
    public float speed;
    public float range;
    public bool initialized;

    [SerializeField] private float distanceTraveled;
    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0f;
    }

    private void Update()
    {
        if(initialized)
        {
            gameObject.transform.position += speed * gameObject.transform.right;
            //TODO: add range limit destroy
            distanceTraveled += speed;
            if(distanceTraveled > range)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("OnTriggerEnter2D called");
        
        switch(otherObj.tag)
        {
            case "HitableEnemy": 
                otherObj.GetComponent<Unit_Actions>().Damage(damage);
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            default:
                break;
        }
        
    }
}
