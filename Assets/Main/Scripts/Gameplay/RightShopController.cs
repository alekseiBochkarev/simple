using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightShopController : MonoBehaviour {

    [Header("Buttons left")]
    public Button dollMenuButton;
    public Button hatMenuButton;
    public Button dressesMenuButton;
    public Button bootsMenuButton;
    public Button bottleMenuButton;

    //[Header("Animator")]
    //public Animator rightMenuAnimator;
    [Header("Buttons right")]
    //public Button upArrow;
    public List<Button> rightMenuButtons;
    //public Button downArrow;

    [Header("Icons")]
    public List<Sprite> dollIcon;

    public List<Sprite> hatIcons;
    public List<Sprite> hatIconsBig;

    public List<Sprite> dressesIcons;
    public List<Sprite> dressesIconsBig;

    public List<Sprite> bootsIcons;
    public List<Sprite> bootsIconsBig;

    public List<Sprite> bottleIcons;
    public List<Sprite> bottleIconsBig;

    public GameObject dragIcon;

    public Sprite empty;

    private int curNumberItem = 0;
    private ItemController.enTypeItem currentTypeItem;

    private  void Start ()
    {
        dollMenuButton.onClick.AddListener(ButtonDollMenu);
        hatMenuButton.onClick.AddListener(ButtonHatMenu);
        dressesMenuButton.onClick.AddListener(ButtonDressesMenu);
        bootsMenuButton.onClick.AddListener(ButtonBootsMenu);
        bottleMenuButton.onClick.AddListener(ButtonBottleMenu);

        //upArrow.onClick.AddListener(PreviousItem);
        //downArrow.onClick.AddListener(NextItem);

        dragIcon.SetActive(false);

        ButtonHatMenu();
    }

    private void ButtonDollMenu()
    {
        UpdateViewItems(dollIcon, dollIcon, ItemController.enTypeItem.DOLL);
        currentTypeItem = ItemController.enTypeItem.DOLL;
    }

    private void ButtonHatMenu()
    {
        UpdateViewItems(hatIconsBig, hatIcons, ItemController.enTypeItem.HAT);
        currentTypeItem = ItemController.enTypeItem.HAT;
    }

    private void ButtonDressesMenu()
    {
        UpdateViewItems(dressesIconsBig, dressesIcons, ItemController.enTypeItem.DRESS);
        currentTypeItem = ItemController.enTypeItem.DRESS;
    }

    private void ButtonBootsMenu()
    {
        UpdateViewItems(bootsIconsBig, bootsIcons, ItemController.enTypeItem.BOOTS);
        currentTypeItem = ItemController.enTypeItem.BOOTS;
    }

    private void ButtonBottleMenu()
    {
        UpdateViewItems(bottleIconsBig, bottleIcons, ItemController.enTypeItem.BOTTLE);
        currentTypeItem = ItemController.enTypeItem.BOTTLE;
    }

    private void NextItem()
    {
        curNumberItem++;
        if (curNumberItem > hatIcons.Count - 1)
            curNumberItem = 0;
        UpdateCurrentMenu();
    }

    private void PreviousItem()
    {
        curNumberItem--;
        if (curNumberItem < 0)
            curNumberItem = hatIcons.Count - 1;
        UpdateCurrentMenu();
    }

    private void UpdateCurrentMenu()
    {
        switch (currentTypeItem)
        {
            case ItemController.enTypeItem.DOLL:
                ButtonDollMenu();
                break;

            case ItemController.enTypeItem.BOOTS:
                ButtonBootsMenu();
                break;

            case ItemController.enTypeItem.BOTTLE:
                ButtonBottleMenu();
                break;

            case ItemController.enTypeItem.DRESS:
                ButtonDressesMenu();
                break;

            case ItemController.enTypeItem.HAT:
                ButtonHatMenu();
                break;
        }
    }

    private void UpdateViewItems(List<Sprite> _spriteForButtons, List<Sprite> _spriteForDolls, ItemController.enTypeItem _typeItem)
    {
        for (int i = 0; i < rightMenuButtons.Count; i++)
        {
            int temp = i;

            if (temp + curNumberItem > _spriteForButtons.Count - 1)
                temp = (curNumberItem + temp) - _spriteForButtons.Count;
            else
                temp += curNumberItem;

            if (_typeItem != ItemController.enTypeItem.DOLL)
            {
                rightMenuButtons[i].transform.Find("ImageItems").GetComponent<Image>().sprite = _spriteForButtons[temp];
                rightMenuButtons[i].transform.Find("ImageDolls").GetComponent<Image>().sprite = empty;
            }
            else
            {
                rightMenuButtons[i].transform.Find("ImageDolls").GetComponent<Image>().sprite = _spriteForButtons[temp];
                rightMenuButtons[i].transform.Find("ImageItems").GetComponent<Image>().sprite = empty;
            }
            if (rightMenuButtons[i].GetComponent<DragItem>() == null)
                rightMenuButtons[i].gameObject.AddComponent<DragItem>();
            rightMenuButtons[i].GetComponent<DragItem>().typeItem = _typeItem;
            rightMenuButtons[i].GetComponent<DragItem>().itemSprite = _spriteForDolls[temp];

            if (_typeItem == ItemController.enTypeItem.DOLL)
            {
                if (!SaveManager.HasKey("Doll" + temp) && temp != 0)
                {
                    SetStateButton(rightMenuButtons[i], Color.black, false, true);
                }
                else
                {
                    SetStateButton(rightMenuButtons[i], Color.white, true, true);
                }
            }
            else if (_typeItem == ItemController.enTypeItem.HAT)
            {
                if (!SaveManager.HasKey("HatItem" + temp) && temp != 0)
                {
                    SetStateButton(rightMenuButtons[i], Color.black, false);
                }
                else
                {
                    SetStateButton(rightMenuButtons[i], Color.white, true);
                }
            }
            else if (_typeItem == ItemController.enTypeItem.DRESS)
            {
                if (!SaveManager.HasKey("DressItem" + temp) && temp != 0)
                {
                    SetStateButton(rightMenuButtons[i], Color.black, false);
                }
                else
                {
                    SetStateButton(rightMenuButtons[i], Color.white, true);
                }
            }
            else if (_typeItem == ItemController.enTypeItem.BOTTLE)
            {
                if (!SaveManager.HasKey("BottleItem" + temp) && temp != 0)
                {
                    SetStateButton(rightMenuButtons[i], Color.black, false);
                }
                else
                {
                    SetStateButton(rightMenuButtons[i], Color.white, true);
                }
            }
            else if (_typeItem == ItemController.enTypeItem.BOOTS)
            {
                if (!SaveManager.HasKey("BootsItem" + temp) && temp != 0)
                {
                    SetStateButton(rightMenuButtons[i], Color.black, false);
                }
                else
                {
                    SetStateButton(rightMenuButtons[i], Color.white, true);
                }
            }
        }
    }

    private void SetStateButton(Button _button, Color _color, bool _isActive, bool _isDoll = false)
    {
        if (!_isDoll)
        {
            _button.transform.Find("ImageItems").GetComponent<Image>().color = _color;
            _button.GetComponent<DragItem>().isActive = _isActive;
        }
        else
        {
            _button.transform.Find("ImageDolls").GetComponent<Image>().color = _color;
            _button.GetComponent<DragItem>().isActive = _isActive;
        }
    }

    public int GetNumberSprite(Sprite _sprite, ItemController.enTypeItem _type)
    {
        List<Sprite> tempList = new List<Sprite>();

        if (_type == ItemController.enTypeItem.DOLL)
            tempList = dollIcon;
        else if (_type == ItemController.enTypeItem.HAT)
            tempList = hatIcons;
        else if (_type == ItemController.enTypeItem.DRESS)
            tempList = dressesIcons;
        else if (_type == ItemController.enTypeItem.BOOTS)
            tempList = bootsIcons;
        else if (_type == ItemController.enTypeItem.BOTTLE)
            tempList = bottleIcons;

        for (int i = 0; i < tempList.Count; i++)
        {
            if (_sprite == tempList[i])
                return i;
        }

        return -1;
    }
}
