using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomScroller
{
    private UIManager ui;
    private GridManager grid;

    private Vector2 cameraPos;
    private int posIndex;

    public RoomScroller(UIManager ui, GridManager grid)
    {
        this.ui = ui;
        this.grid = grid;
        cameraPos = grid.transform.position;
        posIndex = 0;
        Update();
    }

    //Check if can scroll, enable/disable buttons
    public void Update(float time = 0)
    {

        if (grid.ViewWidthIndex < grid.Cols)
        {
            cameraPos = posIndex * Vector2.right * grid.TileWidth + (Vector2)grid.transform.position;
            Debug.Log(cameraPos);
            if (time == 0) CameraController.SnapCamera(cameraPos, grid.ViewHeight);
            else CameraController.ZoomCameraWithRampUpDown(cameraPos, grid.ViewHeight, time, 0.5f);
            
            ui.ShowScroll(-grid.Cols / 2 + 1 < posIndex, grid.Cols / 2 - 1 > posIndex);

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
