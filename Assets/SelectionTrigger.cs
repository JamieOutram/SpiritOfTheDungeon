using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTrigger : MonoBehaviour
{
    public Vector2 coordinate;
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        GridManager.SelectCell(gameObject);
    }
}
