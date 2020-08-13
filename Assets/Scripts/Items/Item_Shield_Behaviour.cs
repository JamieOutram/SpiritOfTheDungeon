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
        //Debug.Log(string.Format("A:({0},{2}), B:({1},{3})", A.position.x, B.position.x, A.position.y, B.position.y));
        //Debug.Log(string.Format("A-B:({0},{1})", B.position.x - A.position.x, B.position.y - A.position.y));
        float adj = B.position.x - A.position.x;
        float angleFromX;
        if (adj==0) 
        {
            angleFromX = (float)Math.Atan((B.position.y - A.position.y) / 0.00001f);
        }
        else
        {
            angleFromX = (float)Math.Atan((B.position.y - A.position.y) / adj);
        }
        //convert to degrees
        angleFromX *= (float)(180/Math.PI);
        //convert to angle from x positive
        if (B.position.x < A.position.x) 
        {
            if (angleFromX > 0) 
                angleFromX = angleFromX - 180;
            else 
                angleFromX = angleFromX + 180;
        }

        //Debug.Log(string.Format("angleFromX:{0}", angleFromX));

        if (B.position.y < A.position.y) 
        {
            //Debug.Log(string.Format("correction:{0}", -Vector3.Angle(A.right, Vector3.right)));
            return angleFromX - (-Vector3.Angle(Vector3.right, A.right)); 
        }
        else
        {
            //Debug.Log(string.Format("correction:{0}", Vector3.Angle(A.right, Vector3.right)));
            return angleFromX - Vector3.Angle(Vector3.right, A.right);
        }
    }
}
