using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomBehaviour : CustomBehaviour
{
    public Vector2 index;
    public Vector2 cellSize;
    private BoxCollider2D cellBox;
    private GridManager grid;

    private bool isSelectable;

    private void Awake()
    {
        grid = GameObject.Find("Grid Manager").GetComponent<GridManager>();
        cellBox = GetComponent<BoxCollider2D>();
        cellBox.size = cellSize;
        SetSelectable(true);
    }

    public override void MyOnMouseDown()
    {
        
        UIManager.infoBox.HideBox();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isSelectable) {
                grid.SelectCell(gameObject, index);
            }
        }
        else
        {
            Debug.Log("Cell Selection blocked");
        }
    }

    public void SetSelectable(bool selectable)
    {
        isSelectable = selectable;
    }

}
