using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomBehaviour : MonoBehaviour
{
    public Vector2 index;
    public Vector2 cellSize;
    private BoxCollider2D cellBox;
    

    private void Awake()
    {
        cellBox = GetComponent<BoxCollider2D>();
        cellBox.size = cellSize;
    }

    private void OnMouseDown()
    {
        GridManager.SelectCell(gameObject, index);
    }

}
