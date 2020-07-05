using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

public class RoomGenerator
{

    private List<RectInt> roomRects;

    public enum RoomType
    {
        basic,
        middleGap,
        cornerGapsEqual
    }

    public RoomGenerator()
    {
        roomRects = new List<RectInt>();

        CreateRoom(RoomType.middleGap);
        DrawRoom();

    }

    private void DrawRoom()
    {

    }

    private void CreateRoom(RoomType roomCase)
    {

        int width = 10;
        int height = 15;
        roomRects.Add(new RectInt(0, 0, width, height));
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