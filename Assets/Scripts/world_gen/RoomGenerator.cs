using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

public class RoomGenerator : MonoBehaviour
{

    private List<RectInt> roomRects;
    [HideInInspector] public Tile floorTile;
    [HideInInspector] public Tile wallTile;
    public Tilemap graphicMap;
    public Tilemap collideMap;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 16;

    public enum RoomType
    {
        basic,
        middleGap,
        cornerGapsEqual
    }

    public RoomGenerator()
    {
        roomRects = new List<RectInt>();
    }

    public void DrawRoom()
    {
        Debug.Log("Draw Room Has Been Called My Dood");
        graphicMap.BoxFill(new Vector3Int(0, 0, 0), floorTile, (int)Math.Floor((decimal)(- width / 2)), (int)Math.Floor((decimal)(- height / 2)), (int)Math.Floor((decimal)(width / 2)), (int)Math.Floor((decimal)(height / 2)));

        foreach (RectInt rect in roomRects)
        {
            graphicMap.BoxFill(new Vector3Int(0, 0, 0), null, rect.xMin, rect.yMin, rect.xMax, rect.yMax);
        }
    }

    public void CreateRoom(RoomType roomCase)
    {
        Debug.Log("Create Room Has Been Called My Dood");
        var rnd = new System.Random();

        switch (roomCase)
        {
            case RoomType.middleGap:
                roomRects.Add(new RectInt(0, 0, rnd.Next(2, width - 2), rnd.Next(2, height - 2)));
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