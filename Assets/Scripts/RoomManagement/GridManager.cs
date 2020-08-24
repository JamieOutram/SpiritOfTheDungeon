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
    private int tileWidth = 24;
    private int tileHeight = 12;
    private GridGraph pathGraph;
    //private Bounds gridBounds;

    // Start is called before the first frame update
    void Awake()
    {
        RefInfo = GetBehaviour(RefObject);
        tileWidth = (int)RefInfo.cellSize.x;
        tileHeight = (int)RefInfo.cellSize.y;
    }

    private void Start()
    {
        //Calling Astar related functions in Awake() clashes with Astar updates.
        //Update AStar graph size
        AstarData data = AstarPath.active.data;
        pathGraph = (GridGraph)data.graphs[0];
        //gridBounds = new Bounds(pathGraph.center, new Vector2(cols * tileWidth, rows * tileHeight));
        pathGraph.SetDimensions(cols * tileWidth, rows * tileHeight, 1);

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

                float posX = col * tileWidth;
                float posY = row * -tileHeight;
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
        float middleX = posX + transform.position.x - (cols-1) * tileWidth/2;
        float middleY = posY + transform.position.x + (rows-1) * tileHeight/2;
        //Debug.Log(string.Format("{0}, {1} converted to {2}, {3}",posX,posY,middleX,middleY));
        return new Vector2(middleX, middleY);
    }

    public static void SelectCell(GameObject obj, Vector2 index)
    {
        //TODO: Cell Selection Code
        Debug.Log(string.Format("{0} selected at {1}",obj.name, index));
        
        
     
    }

}
