using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;

public class GridManager : MonoBehaviour
{
    public GameObject RefObject;
    

    private RoomBehaviour RefInfo;
    [SerializeField] private int rows = 3;
    [SerializeField] private int cols = 3;
    [SerializeField] private int padding = 3;

    public int Rows { get => rows; }
    public int Cols { get => cols; }
    public int Padding { get => padding; }
    public int TileWidth { get; private set; }
    public int TileHeight { get; private set; }

    public int ViewHeightIndex { get; private set; } = 3;
    public int ViewHeight { get { return TileHeight * ViewHeightIndex / 2 + padding; } }
    public int ViewWidthIndex { get; private set; } = 3;
    public int ViewWidth { get { return TileWidth * ViewWidthIndex / 2 + padding; } }



    private GridGraph pathGraph;
    private RoomBehaviour selectedObj;
    private RoomScroller scroller;

    //private Bounds gridBounds;

    // Start is called before the first frame update
    void Awake()
    {
        RefInfo = GetBehaviour(RefObject);
        TileWidth = (int)RefInfo.cellSize.x;
        TileHeight = (int)RefInfo.cellSize.y;
    }

    private void Start()
    {
        //Calling Astar related functions in Awake() clashes with Astar updates.
        //Update AStar graph size
        AstarData data = AstarPath.active.data;
        pathGraph = (GridGraph)data.graphs[0];
        //gridBounds = new Bounds(pathGraph.center, new Vector2(cols * tileWidth, rows * tileHeight));
        pathGraph.SetDimensions(cols * TileWidth, rows * TileHeight, 1);

        //Spawn rooms
        GenerateGrid();

        //Calling imediatly after resizing performs scan first.
        Invoke("AStarScan", 0.0001f);

    }

    private void AStarScan()
    {
        AstarPath.active.Scan(pathGraph);
    }

    private void GenerateGrid()
    { 
        for (int row = 0; row<rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject obj = Instantiate(RefObject, transform);
                RoomBehaviour info = GetBehaviour(obj);
                info.index = new Vector2(col, row);

                float posX = col * TileWidth;
                float posY = row * -TileHeight;
                obj.transform.position = GetWorldPosition(posX, posY);
            } 
        }
    }

    private RoomBehaviour GetBehaviour(GameObject obj)
    {
        RoomBehaviour info;
        if (!obj.TryGetComponent<RoomBehaviour>(out info))
        {
            Exception e = new Exception("Reference object of GridManager has no component of type RoomBehaviour");
            Debug.LogException(e);
            throw e;
        }
        return info;
    }

    private Vector2 GetWorldPosition(float posX, float posY)
    {
        float middleX = posX + transform.position.x - (cols-1) * TileWidth/2;
        float middleY = posY + transform.position.x + (rows-1) * TileHeight/2;
        //Debug.Log(string.Format("{0}, {1} converted to {2}, {3}",posX,posY,middleX,middleY));
        return new Vector2(middleX, middleY);
    }

    public void SelectCell(GameObject obj, Vector2 index)
    {
        
        //TODO: Cell Selection Code
        Debug.Log(string.Format("{0} selected at {1}",obj.name, index));
        if (!CameraController.isCameraZooming)
        {
            if (!ReferenceEquals(selectedObj,null))
                selectedObj.SetSelectable(true); //Re-enable previously selected
            float size = obj.GetComponent<Collider2D>().bounds.size.y / 2;
            CameraController.ZoomCameraWithRampUpDown(obj.transform.position, size, 1.5f, 0.5f);
            selectedObj = obj.GetComponent<RoomBehaviour>();

            //Prevent further selection
            selectedObj.SetSelectable(false);
            selectedObj.gameObject.layer = LayerMask.NameToLayer("Popup Cancel");
        }
    }
    
    public void DeselectCell()
    {
        if (!CameraController.isCameraZooming)
        {
            ViewHeightIndex = 3;

            CameraController.ZoomCameraWithRampUpDown(transform.position, ViewHeight, 1.5f, 0.5f);
            if (selectedObj != null)
            {
                selectedObj.SetSelectable(true);
                selectedObj.gameObject.layer = LayerMask.NameToLayer("Selection Panel");
            }
        }
    }

    public void ScrollRight()
    {
        scroller.ScrollRight();
    }
    public void ScrollLeft()
    {
        scroller.ScrollLeft();
    }



}
