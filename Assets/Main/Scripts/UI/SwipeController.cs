using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IDragHandler
{
    public bool invert;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;

        if (invert)
            delta *= -1;

        if (delta.y != 0)
            Wheel.singleton.RotateWheel(delta.y);
    }
}
