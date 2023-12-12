using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{

    [Header("Button back")]
    public Button buttonBack;
    public GameObject backScreen;
    public bool isShowLargeBannerBack;

    public Button buttonArrowUp;
    public List<Button> cells;
    public Button buttonArrowDown;

    public ItemController itemController;
    public RightShopController rightShopController;

    [Space]
    public GameObject textWarning;
    public GameObject cellsParent;

    private int numOnceDoll = 0;

    void Start ()
    {
        buttonBack.onClick.AddListener(ButtonBack);
        buttonArrowUp.onClick.AddListener(ButtonArrowUp);
        buttonArrowDown.onClick.AddListener(ButtonArrowDown);

        for (int i = 0; i < cells.Count; i++)
        {
            int temp = i;
            cells[i].onClick.AddListener(delegate { ButtonCell(temp); });
        }
    }

    private void OnEnable()
    {
        ShowDolls();
    }

    private void ButtonBack()
    {
        if (isShowLargeBannerBack)
            AdsManager.ShowPageBanner();

        backScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ButtonArrowUp()
    {
        if (numOnceDoll == 0)
            return;

        numOnceDoll -= 3;

        ShowDolls();
    }

    private void ButtonArrowDown()
    {
        int countSet = 0;
        if (SaveManager.HasKey("CountSet"))
        {
            countSet = SaveManager.GetKey("CountSet", 0);
        }

        if (numOnceDoll + 3 < countSet)
            numOnceDoll += 3;

        ShowDolls();
    }

    public void AddDollImage(int _numDoll, int _numHat, int _numDress, int _numBoot, int _numBottle)
    {
        int countSet = 0;
        if (SaveManager.HasKey("CountSet"))
        {
            countSet = SaveManager.GetKey("CountSet", 0);
        }

        SaveManager.SetKey("Set" + countSet.ToString(), _numDoll + ":" + _numHat + ":" + _numDress + ":" + _numBoot + ":" + _numBottle);

        countSet++;
        SaveManager.SetKey("CountSet", countSet);
    }

    private void ShowDolls()
    {
        int countSet = 0;
        if (SaveManager.HasKey("CountSet"))
        {
            countSet = SaveManager.GetKey("CountSet", 0);
        }

        if (countSet == 0)
        {
            textWarning.SetActive(true);
            cellsParent.SetActive(false);
            return;
        }

        textWarning.SetActive(false);
        cellsParent.SetActive(true);

        int numCurrentCell = 0;
        int numLastDoll = countSet;
        if (numOnceDoll + 6 < numLastDoll)
            numLastDoll = numOnceDoll + 6;
        for (int i = numOnceDoll; i < numLastDoll; i++)
        {
            string[] str = SaveManager.GetKey("Set" + i.ToString(), "").Split(':');

            cells[numCurrentCell].transform.Find("Doll").GetComponent<Image>().sprite = GetSpriteOrEmpty(str[0], rightShopController.dollIcon);    // doll
            cells[numCurrentCell].transform.Find("Doll").transform.Find("Hat").GetComponent<Image>().sprite = GetSpriteOrEmpty(str[1], rightShopController.hatIcons);
            cells[numCurrentCell].transform.Find("Doll").transform.Find("Dress").GetComponent<Image>().sprite = GetSpriteOrEmpty(str[2], rightShopController.dressesIcons);
            cells[numCurrentCell].transform.Find("Doll").transform.Find("Boot").GetComponent<Image>().sprite = GetSpriteOrEmpty(str[3], rightShopController.bootsIcons);
            cells[numCurrentCell].transform.Find("Doll").transform.Find("Bottle").GetComponent<Image>().sprite = GetSpriteOrEmpty(str[4], rightShopController.bottleIcons);

            numCurrentCell++;
        }

        if (numCurrentCell == 6)
            return;

        for (int i = numCurrentCell; i < 6; i++)
        {
            cells[i].transform.Find("Doll").GetComponent<Image>().sprite = rightShopController.empty;    // doll
            cells[i].transform.Find("Doll").transform.Find("Hat").GetComponent<Image>().sprite = rightShopController.empty;
            cells[i].transform.Find("Doll").transform.Find("Dress").GetComponent<Image>().sprite = rightShopController.empty;
            cells[i].transform.Find("Doll").transform.Find("Boot").GetComponent<Image>().sprite = rightShopController.empty;
            cells[i].transform.Find("Doll").transform.Find("Bottle").GetComponent<Image>().sprite = rightShopController.empty;
        }
    }

    private Sprite GetSpriteOrEmpty(string _number, List<Sprite> _list)
    {
        if (System.Convert.ToInt32(_number) == -1)
            return rightShopController.empty;
        else
            return _list[System.Convert.ToInt32(_number)];
    }

    private void ButtonCell(int _numCell)
    {
        if (cells[_numCell].transform.Find("Doll").GetComponent<Image>().sprite == rightShopController.empty)
            return;

        itemController.dollImage.sprite = cells[_numCell].transform.Find("Doll").GetComponent<Image>().sprite;
        itemController.hatDollImage.sprite = cells[_numCell].transform.Find("Doll").transform.Find("Hat").GetComponent<Image>().sprite;
        itemController.dressDollImage.sprite = cells[_numCell].transform.Find("Doll").transform.Find("Dress").GetComponent<Image>().sprite;
        itemController.bootsDollImage.sprite = cells[_numCell].transform.Find("Doll").transform.Find("Boot").GetComponent<Image>().sprite;
        itemController.bottleDollImage.sprite = cells[_numCell].transform.Find("Doll").transform.Find("Bottle").GetComponent<Image>().sprite;

        ButtonBack();
    }
}
