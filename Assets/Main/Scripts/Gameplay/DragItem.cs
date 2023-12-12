using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [HideInInspector]
    public Sprite itemSprite;

    [HideInInspector]
    public ItemController.enTypeItem typeItem;

    [HideInInspector]
    public bool isActive = true;

    private RightShopController rightShopController;
    private GameObject dragIcon;
    private float offsetY = 0;

	// Use this for initialization
	void Start () {
        rightShopController = transform.parent.parent.GetComponent<RightShopController>();
        dragIcon = rightShopController.dragIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isActive)
            return;
        //Debug.Log("start");
        dragIcon.SetActive(true);
        //dragIcon.GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        dragIcon.GetComponent<Image>().sprite = itemSprite;
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
        if (!isActive)
            return;

        dragIcon.transform.parent.position = eventData.position;
        dragIcon.transform.localPosition = new Vector2(0, (offsetY / 512f) * dragIcon.GetComponent<RectTransform>().rect.height);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isActive)
            return;
        //Debug.Log("finish");
        dragIcon.SetActive(false);
        ItemController.singleton.AddItem(dragIcon.GetComponent<Image>().sprite, typeItem);
    }
}
