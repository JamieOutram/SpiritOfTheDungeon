using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class RoomBehaviour : CustomBehaviour
{
    public Vector2 index;
    public Vector2 cellSize;
    private BoxCollider2D cellBox;
    public bool isSelectable { get; private set; }

    private void Awake()
    {
        cellBox = GetComponent<BoxCollider2D>();
        cellBox.size = cellSize;
        SetSelectable(true);
    }

    public override void MyOnMouseDown()
    {
        UIManager.instance.TrainingPanel.OnCellMouseDown(this, index);
    }

    public void SetSelectable(bool selectable)
    {
        isSelectable = selectable;
    }

}
