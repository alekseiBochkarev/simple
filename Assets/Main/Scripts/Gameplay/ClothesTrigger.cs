using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClothesTrigger : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image dollImage;

    private bool isAboveTrigger = false;

    private GameObject dragIcon;
    private float offsetY = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragIcon == null)
            dragIcon = ItemController.singleton.dragIcon;

        if (dollImage.sprite.name == "Empty")
            return;

        isAboveTrigger = true;

        dragIcon.GetComponent<Image>().sprite = dollImage.sprite;
        dragIcon.SetActive(true);
        offsetY = 512 - GetHighestPixel(dragIcon.GetComponent<Image>().sprite.texture, dragIcon.GetComponent<Image>().sprite.rect);
    }

    private int GetHighestPixel(Texture2D _tex, Rect _rect)
    {
        for (int j = (int)(_rect.y + _rect.height); j > _rect.y; j--)
        {
            for (int i = (int)_rect.x; i < _rect.x + _rect.width; i++)
            {
                if (_tex.GetPixel(i, j).a != 0)
                {
                    return j - (int)_rect.y;
                }
            }
        }
        return 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragIcon.transform.parent.position = eventData.position;
        dragIcon.transform.localPosition = new Vector2(0, (offsetY / 512f) * dragIcon.GetComponent<RectTransform>().rect.height);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isAboveTrigger)
            dollImage.sprite = ItemController.singleton.empty;
        dragIcon.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isAboveTrigger = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isAboveTrigger = false;
    }
}
