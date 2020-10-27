using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    //Define interactions in inspector
    [SerializeField] LayerMask mouseInteractables = default;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D[] hits = GetSortedRayIntersectionAll(CameraController.activeCamera.ScreenPointToRay(Input.mousePosition));

            //Trigger on click event for object
            if (hits.Length > 0) {
                CustomBehaviour trigger = hits[0].collider.gameObject.GetComponent<CustomBehaviour>();
                if (trigger != null)
                    trigger.MyOnMouseDown();
                else Debug.LogWarning("Clicked collider does not have a CustomBehaviour script");
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RaycastHit2D[] hits = GetSortedRayIntersectionAll(CameraController.activeCamera.ScreenPointToRay(Input.mousePosition));

            //Trigger on click event for object
            if (hits.Length > 0)
            {
                CustomBehaviour trigger = hits[0].collider.gameObject.GetComponent<CustomBehaviour>();
                if (trigger != null)
                    trigger.MyOnMouseUp();
                else Debug.LogWarning("Clicked collider does not have a CustomBehaviour script");
            }
        }

    }

    RaycastHit2D[] GetSortedRayIntersectionAll(Ray ray)
    {
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, mouseInteractables);
        int[] keys = new int[hits.Length];
        for (int j = 0; j < hits.Length; j++)
        {
            keys[j] = hits[j].collider.gameObject.layer;
        }
        Array.Sort(keys, hits);
        return hits;
    }
    
}
