using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject RefObject;
    [SerializeField] private int rows = 3;
    [SerializeField] private int cols = 3;
    [SerializeField] private float tileWidth = 24;
    [SerializeField] private float tileHeight = 12;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        
        for (int row = 0; row<rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject obj = Instantiate(RefObject, transform);

                float posX = col * tileWidth;
                float posY = row * -tileHeight;
                obj.transform.position = GetWorldPosition(posX, posY);
                
                
            } 
        }
    }

    private Vector2 GetWorldPosition(float posX, float posY)
    {
        float middleX = posX + transform.position.x - (cols-1) * tileWidth/2;
        float middleY = posY + transform.position.x + (rows-1) * tileHeight/2;
        Debug.Log(string.Format("{0}, {1} converted to {2}, {3}",posX,posY,middleX,middleY));
        return new Vector2(middleX, middleY);
    }

    public static void SelectCell(GameObject obj)
    {
        //TODO: Cell Selection Code
    }

    //Update is called once per frame
    void Update()
    {
        
    }
}
