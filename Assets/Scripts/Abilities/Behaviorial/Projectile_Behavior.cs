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
    public GameObject casterObj;

    private float distanceTraveled;
    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0f;
    }

    private void Update()
    {
        if(initialized)
        {
            gameObject.transform.position += speed * gameObject.transform.right * Time.deltaTime;
            //TODO: add range limit destroy
            distanceTraveled += speed * Time.deltaTime;
            if(distanceTraveled > range)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        //Debug.Log("OnTriggerEnter2D called");
        if (GameObject.ReferenceEquals(casterObj, null))
        {
            Debug.LogError(string.Format("Ability {0} has no assigned caster, Destroying Object.", gameObject.name));
            Destroy(gameObject);
            return;
        }
        else if (InteractionManager.IsDamaged(casterObj, otherObj.gameObject))
        {
            otherObj.GetComponent<Unit_Actions>().Damage(damage);
            Destroy(gameObject);
        }
        else if(InteractionManager.IsBlocked(casterObj, otherObj.gameObject))
        {
            Destroy(gameObject);
        }
    }
}
