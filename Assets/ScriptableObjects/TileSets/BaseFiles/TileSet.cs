using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "TileSet")]

public class TileSet : ScriptableObject
{
    public Tile floorTile;
    public Tile wallTile;
    public Tilemap graphicMap;
    public Tilemap collideMap;

    private RoomGenerator roomGen;


    public void Initialise(GameObject obj)
    {
        roomGen = obj.GetComponent<RoomGenerator>();
        roomGen.floorTile = floorTile;
        roomGen.wallTile = wallTile;
       // roomGen.graphicMap = graphicMap;
       // roomGen.collideMap = collideMap;

    }

    public void DrawRoomTrigger()
    {
        roomGen.DrawRoom();
    }

    public void CreateRoomTrigger(RoomGenerator.RoomType roomCase)
    {
        roomGen.CreateRoom(roomCase);
    }
}