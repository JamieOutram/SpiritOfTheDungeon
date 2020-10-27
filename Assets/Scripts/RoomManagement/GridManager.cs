using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    
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
    public RoomBehaviour selectedObj { get; private set; }


    //private Bounds gridBounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogException(new Exception("Only One Static Instance of Grid Manager should exsist"));
        }
        instance = this;

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

    public void SelectCell(RoomBehaviour obj, Vector2 index)
    {
        Debug.Log(string.Format("{0} selected at {1}",obj.name, index));

        if (!ReferenceEquals(selectedObj,null))
            selectedObj.SetSelectable(true); //Re-enable previously selected
        
        selectedObj = obj;

        //Prevent further selection
        selectedObj.SetSelectable(false);
        selectedObj.gameObject.layer = LayerMask.NameToLayer("Popup Cancel");

    }
    
    public void DeselectCell()
    {
        ViewHeightIndex = 3;

        if (selectedObj != null)
        {
            selectedObj.SetSelectable(true);
            selectedObj.gameObject.layer = LayerMask.NameToLayer("Selection Panel");
            selectedObj = null;
        }
    }
}
