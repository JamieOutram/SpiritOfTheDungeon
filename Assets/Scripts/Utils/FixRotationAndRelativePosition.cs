using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FixRotationAndRelativePosition : MonoBehaviour
{
    Quaternion rotation;
    Vector3 startOffset;

    void Awake()
    {
        rotation = transform.rotation;
        startOffset = transform.position - transform.parent.position;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
        transform.position = transform.parent.position + startOffset;
    }
}
