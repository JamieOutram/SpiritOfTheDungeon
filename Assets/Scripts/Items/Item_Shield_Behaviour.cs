using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA;

public class Item_Shield_Behaviour : MonoBehaviour
{
    public float flatReduction;
    public float percentReduction;
    public float protectionArc;
    public Collider2D projectileBlockArea;
    public Transform parentTransform;

    void Awake()
    {
        parentTransform = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ApplyModifier(int damage, Transform source)
    {
        //Debug.Log("Shield called");
        float tempDamage = damage;
        Debug.Log(string.Format("Relative angle:{0}", CalcRelativeAngleFromRight(parentTransform, source)));
        if (Math.Abs(CalcRelativeAngleFromRight(parentTransform, source)) <= protectionArc)
        {
            tempDamage -= flatReduction;
            tempDamage *= (1 - percentReduction/100);
            //Debug.Log("Damage Reduced by Shield");
        }
        
        return (int)tempDamage;
    }

    private float CalcRelativeAngleFromRight(Transform A, Transform B)
    {
        Vector2 toTarget = B.position - A.position;
        Vector2 facing = A.right;
        //Debug.Log(string.Format("Name:{0}", A.gameObject.name));
        //Debug.Log(string.Format("to target:{0}", toTarget));
        //Debug.Log(string.Format("facing:{0}", facing));
        //Debug.Log(string.Format("angle between:{0}", Vector2.SignedAngle(toTarget, facing)));
        return Vector2.SignedAngle(toTarget, facing);
    }
}
