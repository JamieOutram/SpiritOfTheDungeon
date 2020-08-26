using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Place the camera to be moved in here
    public Camera selectedCamera;

    //Flag showing that the camera is moving
    public bool isCameraZooming { get; private set; }
    
    //All the information required to calculate the position and zoom
    private float originalSize;
    private float targetSize;
    private Vector2 originalPivot;
    private Vector2 targetPivot;
    private float transitionTime;
    private float remainingTime;
    private float rampTime;

    //Max speed values do not need to be calculated each frame
    private Vector2 _pivotVmax;
    private float _sizeVmax;

    private void Awake()
    {
        //Ensure the flag is initialized correctly
        isCameraZooming = false;
    }

    void Start()
    {
        //Example Zoom Trigger
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

    //Trigger for the camera movement
    public void ZoomCameraWithRampUpDown(Vector2 pivot, float size, float time, float rampTime)
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

        //Calculate Max speed
        this._pivotVmax = CalcMaxSpeed(targetPivot - originalPivot, rampTime, transitionTime);
        this._sizeVmax = CalcMaxSpeed(targetSize - originalSize, rampTime, transitionTime);

    }

    //Calculates the peak velocity of the camera
    private float CalcMaxSpeed(float distance, float rampT, float transT)
    {
        if (transT <= 2 * rampT)
        {
            //If the ramp time is too large there is no middle platau
            //in this case the maximum speed reached is based on the total transition time i.e. rampTime = transitionTime/2
            return (2 * distance / transT);
        }
        else
        {
            return (distance / (transT - rampT));
        }
    }
    private Vector2 CalcMaxSpeed(Vector2 distance, float rampT, float transT)
    {
        return new Vector2(CalcMaxSpeed(distance.x, rampT, transT), CalcMaxSpeed(distance.y, rampT, transT));
    }

    //Method called every frame during zoom/movement
    private void UpdateZoom()
    {
        //Calculate new time independant of framerate
        remainingTime -= Time.deltaTime;
        float time = transitionTime - remainingTime;

        if(remainingTime<=0)
        {
            //if at the end of transition skip calculations
            selectedCamera.orthographicSize = targetSize;
            selectedCamera.transform.position = new Vector3(targetPivot.x, targetPivot.y, -10);
            //set flag at end of transition
            isCameraZooming = false;
        }
        
        //Calculate the new zoom and position values
        selectedCamera.orthographicSize = CalcRampedValue(originalSize, time, _sizeVmax);
        selectedCamera.transform.position = CalcRampedValue(originalPivot, time, _pivotVmax);
    }
    
    //Controls distance curve of zoom and position (t = 0 to transitiontime)
    private float CalcRampedValue(float startPoint, float t, float Vmax, float transT, float rampT)
    {
        //Ensure time given is within valid range
        t = Mathf.Clamp(t, 0, transT);
        
        
        double d = 0; //holds distance traveled
        //Depending on the given time select the correct calculations for distance
        if (t <= rampT)
        {
            //Ramp Up distance formula
            d = 0.5 / rampT * t * t;
        }
        else 
        {
            //Add Ramp Up total distance
            d += 0.5 * rampT; 
            if (t < transT - rampT)
            {
                //Platau distance formula
                d += (t - rampT);
            }
            else
            {
                //Add Platau total distance
                d += transT - 2 * rampT; 
                //Ramp Down distance formula (Could optimise by finding distance from end and minusing?)
                d += 0.5 * (rampT - (transT - t)*(transT - t) / rampT);
            }
        }
        //Scale According to Max Velocity
        d *= Vmax;
        //Add starting Offset
        d += startPoint; 
        return (float)d;
    }
    float CalcRampedValue(float startPoint, float t, float Vmax)
    {
        return CalcRampedValue(startPoint, t, Vmax, transitionTime, rampTime);
    }
    Vector3 CalcRampedValue(Vector2 startPoint, float t , Vector2 Vmax)
    {
        return new Vector3(CalcRampedValue(startPoint.x, t, Vmax.x), CalcRampedValue(startPoint.y, t, Vmax.y), -10);
    }

}
