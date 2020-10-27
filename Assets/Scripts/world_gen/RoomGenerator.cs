using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;
using System.Drawing;
using UnityEngine.U2D;

public class RoomGenerator : MonoBehaviour
{

    private List<RectInt> roomRects;
    private List<Corner> roomVertices;
    [HideInInspector] public Tile floorTile;
    [HideInInspector] public Tile wallTile;
    public Tilemap graphicMap;
    public Tilemap collideMap;
    private int width = 10;
    private int height = 7;



    public enum RoomType
    {
        basic,
        middleGap,
        cornerGapsEqual
    }

    private enum CornerType
    {
        topLeft,
        topRight,
        bottomLeft,
        bottomRight
    }

    private struct Corner
    {
        
        public int x;
        public int y;
        public CornerType cornerCase;

        public Corner(int x, int y, CornerType cornerCase)
        {
            this.x = x;
            this.y = y;
            this.cornerCase = cornerCase;
        }
    }

    struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public RoomGenerator()
    {
        roomRects = new List<RectInt>();
        roomVertices = new List<Corner>();

    }

    public void DrawRoom()
    {
        Debug.Log("Draw Room Has Been Called My Dood");
        graphicMap.BoxFill(new Vector3Int(0, 0, 0), floorTile, -width, -height, width-1, height-1);

        foreach (RectInt rect in roomRects)
        {
            graphicMap.BoxFill(new Vector3Int(0, 0, 0), null, rect.xMin, rect.yMin, rect.xMax-1, rect.yMax-1);
        }


    }

    private RectInt gapGenerator(int gapX, int gapY, int gapWidth, int gapHeight)
    {
        var rnd = new System.Random();
        RectInt gap = new RectInt();
                gap.xMin = gapX - rnd.Next(1, (int)(gapWidth / 1.5));
                gap.yMin = gapY - rnd.Next(1, (int)(gapHeight / 1.5));
                gap.xMax = gapX + rnd.Next(1, (int)(gapWidth / 1.5));
                gap.yMax = gapY + rnd.Next(1, (int)(gapHeight / 1.5));
        return gap;
    }

    public void CreateRoom(RoomType roomCase)
    {
        Debug.Log("Create Room Has Been Called My Dood");
        

        

        switch (roomCase)
        {
            case RoomType.middleGap:
                RectInt gap  = gapGenerator(0,0,(int)(width / 1.5), (int)(height / 1.5));
                roomRects.Add(gap);

                roomVertices.Add(new Corner(-width-1, -height-1, CornerType.topLeft));
                /*roomVertices.Add(new Vector3Int(width+1, -height-1, 2));
                roomVertices.Add(new Vector3Int(width+1, height+1, 3));
                roomVertices.Add(new Vector3Int(-width-1, height+1, 4));
                roomVertices.Add(new Vector3Int(gap.xMin+1, gap.yMin+1, 1));
                roomVertices.Add(new Vector3Int(gap.xMax-1, gap.yMin+1, 2));
                roomVertices.Add(new Vector3Int(gap.xMax-1, gap.yMax-1, 3));
                roomVertices.Add(new Vector3Int(gap.xMin+1, gap.yMax-1, 4));
                
                roomEdges.Add();
                */
                break;
                
            case RoomType.cornerGapsEqual:
                
                break;
            default:
                break;

        }
    }

    // Room Possibilities: [room base dim] [room type] [size of rect to take out]

    // Outputs: border lines - 2 vector2's in each element of a list
}