using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIUtils 
{
    public static void ClampRectToRect(RectTransform innerTransform, RectTransform containerTransfrom)
    {
        Vector2 innerDelta = new Vector2(innerTransform.rect.width, innerTransform.rect.height);
        Vector2 containerDelta = new Vector2(containerTransfrom.rect.width, containerTransfrom.rect.height);
        Vector2 apos = innerTransform.anchoredPosition;
        Vector2 max = containerDelta * (Vector2.one - containerTransfrom.pivot) - innerDelta * (Vector2.one - innerTransform.pivot);
        Vector2 min = innerDelta * innerTransform.pivot - containerDelta * containerTransfrom.pivot;
        Debug.Log(string.Format("Min: {0}", min));
        Debug.Log(string.Format("Max: {0}", max));
        apos.x = Mathf.Clamp(apos.x, min.x, max.x);
        apos.y = Mathf.Clamp(apos.y, min.y, max.y);
        innerTransform.anchoredPosition = apos;
    }
}
