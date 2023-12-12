using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isTouchEnter;

    public Sprite empty;

    public Image dollImage;
    public Image hatDollImage;
    public Image dressDollImage;
    public Image bottleDollImage;
    public Image bootsDollImage;

    [HideInInspector]
    public GameObject dragIcon;

    public enum enTypeItem
    {
        DOLL,
        HAT,
        DRESS,
        BOTTLE,
        BOOTS
    }

    public static ItemController singleton;

    void Awake () {
        singleton = this;
	}

    private void Start()
    {
        dragIcon = transform.parent.GetComponent<RightShopController>().dragIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isTouchEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isTouchEnter = false;
    }

    public void AddItem(Sprite _sprite, enTypeItem _typeItem)
    {
        if (!isTouchEnter)
            return;

        switch(_typeItem)
        {
            case enTypeItem.DOLL:
                dollImage.sprite = _sprite;
                break;

            case enTypeItem.HAT:
                hatDollImage.sprite = _sprite;
                break;

            case enTypeItem.DRESS:
                dressDollImage.sprite = _sprite;
                break;

            case enTypeItem.BOTTLE:
                bottleDollImage.sprite = _sprite;
                break;

            case enTypeItem.BOOTS:
                bootsDollImage.sprite = _sprite;
                break;
        }
    }
}
