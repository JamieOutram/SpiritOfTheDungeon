using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleImage : MonoBehaviour
{
    public List<Sprite> sprites = default;
    
    Image pic;
    int index;

    private void Awake()
    {
        pic = GetComponent<Image>();
        index = 0;
        Set(0);
    }

    public void Next()
    {
        index++;
        index %= sprites.Count;
        Set(index);
    }

    public void Set(int i)
    {
        if (0 <= i && i < sprites.Count)
            pic.sprite = sprites[i];
        else
            Debug.LogError("Sprite Index Out of Defined Range");
    }
}
