using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScroller
{
    private UIManager ui;
    private GridManager grid;

    private Vector2 cameraPos;
    private int posIndex;
    private float viewWidth;

    public RoomScroller(UIManager ui, GridManager grid)
    {
        this.ui = ui;
        this.grid = grid;
        cameraPos = grid.transform.position;
        posIndex = 0;
        Update();
    }

    //Check if can scroll, enable/disable buttons
    public void Update(bool fast = true)
    {

        if (grid.ViewWidthIndex > grid.Cols)
        {
            cameraPos = posIndex * Vector2.right * grid.TileWidth + (Vector2)grid.transform.position;
            if (fast) CameraController.SnapCamera(cameraPos, grid.ViewHeight);
            else CameraController.ZoomCameraWithRampUpDown(cameraPos, grid.ViewHeight, 1.5f, 0.5f);
            
            ui.ShowScroll(-grid.Cols / 2 <= posIndex, grid.Cols / 2 >= posIndex);

        }
        
    }

    //Move grid and track camera reference point when buttons pressed
    public void ScrollRight()
    {
        //TODO
        throw new NotImplementedException();
    }

    public void ScrollLeft()
    {
        //TODO
        throw new NotImplementedException();
    }


}
