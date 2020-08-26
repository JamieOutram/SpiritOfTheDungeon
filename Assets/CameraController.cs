using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

//ensure only one in scene
public class CameraController : MonoBehaviour
{
    //Place the camera to be moved in here
    public static Camera selectedCamera { get; private set; }

    //Flag showing that the camera is moving
    public static bool isCameraZooming { get; private set; }
    
    //All the information required to calculate the position and zoom
    private static float originalSize;
    private static float targetSize;
    private static Vector2 originalPivot;
    private static Vector2 targetPivot;
    private static float transitionTime;
    private static float remainingTime;
    private static float rampTime;

    //Max speed values do not need to be calculated each frame
    private static Vector2 _pivotVmax;
    private static float _sizeVmax;

    private void Awake()
    {
        //Ensure the flag is initialized correctly
        isCameraZooming = false;
        SelectCamera(GetComponent<Camera>());
    }

    void Start()
    {
        //Example Zoom Trigger
        
        //ZoomCameraWithRampUpDown(new Vector2(9, -9), 5f, 1f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraZooming)
        {
            UpdateZoom();
        }
    }

    public static void SelectCamera(Camera c)
    {
        if (!isCameraZooming)
        {
            selectedCamera = c;
        }
    }

    //Trigger for the camera movement
    public static void ZoomCameraWithRampUpDown(Vector2 pivot, float size, float time, float rampT)
    {
        //Another solution could be to find new points along line and linear interpolate between them.
        if (isCameraZooming)
        {
            Debug.LogWarning("Camera already zooming");
            return;
        }

        //Set Values and Flags for zoom 
        originalSize = selectedCamera.orthographicSize;
        originalPivot = selectedCamera.transform.position;
        targetPivot = pivot;
        targetSize = size;
        transitionTime = time;
        remainingTime = time;
        rampTime = rampT;
        isCameraZooming = true;

        //Calculate Max speed
        _pivotVmax = CalcMaxSpeed(targetPivot - originalPivot, rampTime, transitionTime);
        _sizeVmax = CalcMaxSpeed(targetSize - originalSize, rampTime, transitionTime);

    }

    public static void SnapCamera(Vector2 pivot, float size)
    {
        ZoomCameraWithRampUpDown(pivot, size, 0, 0);
    }

    //Calculates the peak velocity of the camera
    private static float CalcMaxSpeed(float distance, float rampT, float transT)
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
    private static Vector2 CalcMaxSpeed(Vector2 distance, float rampT, float transT)
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
