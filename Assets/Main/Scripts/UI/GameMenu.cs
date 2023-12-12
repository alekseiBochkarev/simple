using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    [Header("Button main menu")]
    public Button buttonMainMenu;
    public GameObject mainMenuScreen;
    public bool isShowLargeBannerMainMenu;

    [Header("Button shop menu")]
    public Button buttonShopMenu;
    public GameObject shopScreen;
    public bool isShowLargeBannerShop;

    [Header("Button roulette")]
    public Button buttonRoulette;
    public GameObject rouletteScreen;
    public bool isShowLargeBannerToRoulette;

    /*[Header("Button gallery")]
    public Button buttonGallery;
    public GameObject galleryScreen;
    public bool isShowLargeBannerToGallery;*/

    /*[Header("Button photo")]
    public Button buttonPhoto;
    public Gallery gallery;*/
    public ItemController itemController;
    private RightShopController rightShopController;
    public Animator panelPhoto;

    private void Start()
    {
        buttonShopMenu.onClick.AddListener(ButtonShopMenu);
        buttonMainMenu.onClick.AddListener(ButtonMainMenu);
        buttonRoulette.onClick.AddListener(ButtonRoulette);
       // buttonGallery.onClick.AddListener(ButtonGallery);
        //buttonPhoto.onClick.AddListener(ButtonPhoto);

        //panelPhoto.transform.Find("Close").GetComponent<Button>().onClick.AddListener(() => panelPhoto.SetTrigger("Hide"));

        rightShopController = GetComponent<RightShopController>();
    }

    private void ButtonShopMenu()
    {
        if (isShowLargeBannerShop)
            AdsManager.ShowPageBanner();

        shopScreen.GetComponent<BallController>().SpawnBall();
        shopScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ButtonMainMenu()
    {
        if (isShowLargeBannerMainMenu)
            AdsManager.ShowPageBanner();

        mainMenuScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ButtonRoulette()
    {
        if (isShowLargeBannerToRoulette)
            AdsManager.ShowPageBanner();

        rouletteScreen.SetActive(true);
        rouletteScreen.GetComponent<Roulette>().backScreen = gameObject;
        gameObject.SetActive(false);
    }

    /*private void ButtonGallery()
    {
        if (isShowLargeBannerToGallery)
            AdsManager.ShowPageBanner();

        galleryScreen.SetActive(true);
        gameObject.SetActive(false);
    }*/

    private void ButtonPhoto()
    {
        int numDoll = rightShopController.GetNumberSprite(itemController.dollImage.sprite, ItemController.enTypeItem.DOLL);
        int numHat = rightShopController.GetNumberSprite(itemController.hatDollImage.sprite, ItemController.enTypeItem.HAT);
        int numDress = rightShopController.GetNumberSprite(itemController.dressDollImage.sprite, ItemController.enTypeItem.DRESS);
        int numBoot = rightShopController.GetNumberSprite(itemController.bootsDollImage.sprite, ItemController.enTypeItem.BOOTS);
        int numBottle = rightShopController.GetNumberSprite(itemController.bottleDollImage.sprite, ItemController.enTypeItem.BOTTLE);

        //gallery.AddDollImage(numDoll, numHat, numDress, numBoot, numBottle);
        panelPhoto.SetTrigger("Show");
    }
}
