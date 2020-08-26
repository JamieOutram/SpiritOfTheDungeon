using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera selectedCamera;

    // Start is called before the first frame update
    public bool isCameraZooming { get; private set; }
    private float originalSize;
    private float targetSize;
    private Vector2 originalPivot;
    private Vector2 targetPivot;
    private float transitionTime;
    private float remainingTime;
    private float rampTime;

    private Vector2 _pivotVmax;
    private float _sizeVmax;

    private void Awake()
    {
        isCameraZooming = false;
    }

    void Start()
    {
        ZoomCameraWithRampUpDown(new Vector2(9, -9), 5f, 1f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraZooming)
        {
            UpdateZoom();
        }
    }

    void ZoomCameraWithRampUpDown(Vector2 pivot, float size, float time, float rampTime)
    {
        if (isCameraZooming)
        {
            Debug.LogWarning("Camera already zooming");
            return;
        }

        //Set Values and Flags for zoom 
        this.originalSize = selectedCamera.orthographicSize;
        this.originalPivot = selectedCamera.transform.position;
        this.targetPivot = pivot;
        this.targetSize = size;
        this.transitionTime = time;
        this.remainingTime = time;
        this.rampTime = rampTime;
        this.isCameraZooming = true;

        //calculate Max speed
        this._pivotVmax = CalcMaxSpeed(targetPivot - originalPivot, rampTime, transitionTime);
        this._sizeVmax = CalcMaxSpeed(targetSize - originalSize, rampTime, transitionTime);

    }

    float CalcMaxSpeed(float distance, float rampT, float transT)
    {
        if (transT <= 2 * rampT)
        {
            return (2 * distance / transT);
        }
        else
        {
            return (distance / (transT - rampT));
        }
    }
    Vector2 CalcMaxSpeed(Vector2 distance, float rampT, float transT)
    {
        return new Vector2(CalcMaxSpeed(distance.x, rampT, transT), CalcMaxSpeed(distance.y, rampT, transT));
    }


    void UpdateZoom()
    {
        remainingTime -= Time.deltaTime;
        float time = transitionTime - remainingTime;

        selectedCamera.orthographicSize = CustomCurve(originalSize, time, _sizeVmax);
        selectedCamera.transform.position = CustomCurve(originalPivot, time, _pivotVmax);

        if(remainingTime<=0)
        {
            isCameraZooming = false;
        }
    }
    
    //Controls distance curve of zoom and position (t = 0 to transitiontime)
    float CustomCurve(float startPoint, float t, float Vmax, float transT, float rampT)
    {
        t = Mathf.Clamp(t, 0, transT);
        double d = 0;
        if (t <= rampT)
        {
            //Ramp Up
            d = 0.5 / rampT * t * t;
        }
        else 
        {
            d += 0.5 * rampT;
            if (t < transT - rampT)
            {
                //Middle
                d += (t - rampT);
            }
            else
            {
                //add constant speed distance
                d += transT - 2 * rampT;

                //Ramp Down
                d += 0.5 * (rampT - (transT - t)*(transT - t) / rampT);
            }
        }
        d *= Vmax;

        d += startPoint;
        return (float)d;
    }
    float CustomCurve(float startPoint, float t, float Vmax)
    {
        return CustomCurve(startPoint, t, Vmax, transitionTime, rampTime);
    }

    //Vector2 wrapper for customcurve
    Vector3 CustomCurve(Vector2 startPoint, float t , Vector2 Vmax)
    {
        return new Vector3(CustomCurve(startPoint.x, t, Vmax.x), CustomCurve(startPoint.y, t, Vmax.y), -10);
    }

}
