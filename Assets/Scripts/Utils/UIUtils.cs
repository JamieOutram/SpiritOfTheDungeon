using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIUtils 
{
    public static void ClampRectToRect(RectTransform innerTransform, RectTransform containerTransfrom)
    {
        Vector2 infoSizeDelta = new Vector2(innerTransform.rect.width, innerTransform.rect.height);
        Vector2 canvasSizeDelta = new Vector2(containerTransfrom.rect.width, containerTransfrom.rect.height);
        Vector2 apos = innerTransform.anchoredPosition;
        Vector2 max = canvasSizeDelta * (Vector2.one - containerTransfrom.pivot) - infoSizeDelta * (Vector2.one - innerTransform.pivot);
        Vector2 min = infoSizeDelta * innerTransform.pivot - canvasSizeDelta * containerTransfrom.pivot;
        apos.x = Mathf.Clamp(apos.x, min.x, max.x);
        apos.y = Mathf.Clamp(apos.y, min.y, max.y);
        innerTransform.anchoredPosition = apos;
    }
}
