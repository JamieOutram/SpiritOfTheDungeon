using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomScroller
{
    private Vector2 cameraPos;
    private int posIndex;
    private Training_UIPanel ui;

    public RoomScroller()
    {

        ui = UIManager.instance.TrainingPanel;
        if (GridManager.instance == null)
        {
            Debug.LogException(new Exception("Grid Manager should be instantiated in scene before the scroller object"));
        }
        
        cameraPos = GridManager.instance.transform.position;
        
        posIndex = 0;
        Update();
    }

    //Check if can scroll, enable/disable buttons
    public void Update(float time = 0)
    {

        if (GridManager.instance.ViewWidthIndex < GridManager.instance.Cols)
        {
            cameraPos = posIndex * Vector2.right * GridManager.instance.TileWidth + (Vector2)GridManager.instance.transform.position;
            Debug.Log(cameraPos);
            if (time == 0) CameraController.SnapCamera(cameraPos, GridManager.instance.ViewHeight);
            else CameraController.ZoomCameraWithRampUpDown(cameraPos, GridManager.instance.ViewHeight, time, 0.5f);
            
            ui.ShowScroll(-GridManager.instance.Cols / 2 + 1 < posIndex, GridManager.instance.Cols / 2 - 1 > posIndex);
        }
        
    }

    //Move grid and track camera reference point when buttons pressed
    public void ScrollRight()
    {
        posIndex++;
        Update(0.5f);
        Debug.Log("Called");
    }

    public void ScrollLeft()
    {
        posIndex--;
        Update(0.5f);
    }


}
