using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneratorController : MonoBehaviour
{
    
    public TileSet set;

    public void Start()
    {
        set.Initialise(gameObject);
        set.CreateRoomTrigger(RoomGenerator.RoomType.middleGap);
        set.DrawRoomTrigger();
    }

}
