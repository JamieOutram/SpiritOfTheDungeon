using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomBehaviour : MonoBehaviour
{
    public Vector2 index;
    public Vector2 cellSize;
    private BoxCollider2D cellBox;
    private GridManager grid;

    private void Awake()
    {
        grid = GameObject.Find("Grid Manager").GetComponent<GridManager>();
        cellBox = GetComponent<BoxCollider2D>();
        cellBox.size = cellSize;
    }

    private void OnMouseDown()
    {
        
        UIManager.infoBox.HideBox();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            grid.SelectCell(gameObject, index);
        }
        else
        {
            Debug.Log("Cell Selection blocked");
        }
    }

}
