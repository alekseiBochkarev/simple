using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [Header("Button main menu")]
    public Button buttonMainMenu;
    public GameObject mainMenuScreen;
    public bool isShowLargeBannerToMainMenu;

    [Header("Button game")]
    public Button buttonGame;
    public GameObject gameScreen;
    public bool isShowLargeBannerToGame;

    [Header("Button roulette")]
    public Button buttonRoulette;
    public GameObject rouletteScreen;
    public bool isShowLargeBannerToRoulette;

    [Header("Button add ball")]
    public Button buttonAddBall;

    [Header("Other")]
    public Animator panelWarning;
    public bool isShowPanelWarning { get; private set; }

    private BallController ballController;

    public static ShopMenu singleton;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        ballController = GetComponent<BallController>();

        buttonMainMenu.onClick.AddListener(ButtonMainMenu);
        buttonGame.onClick.AddListener(ButtonGame);
        buttonRoulette.onClick.AddListener(ButtonRoulette);
        buttonAddBall.onClick.AddListener(ButtonAddBall);
        panelWarning.transform.Find("OK").GetComponent<Button>().onClick.AddListener(ButtonOk);
        panelWarning.transform.Find("NO").GetComponent<Button>().onClick.AddListener(ButtonNo);
        panelWarning.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ButtonNo);
    }

    private void ButtonMainMenu()
    {
        if (isShowLargeBannerToMainMenu)
            AdsManager.ShowPageBanner();

        if (ballController.curNumberRemainingBalls == 0)
            HidePanelWarning();

        AdsManager.CloseCurrentPanelQuestion();

        mainMenuScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ButtonGame()
    {
        if (isShowLargeBannerToGame)
            AdsManager.ShowPageBanner();

        if (ballController.curNumberRemainingBalls == 0)
            HidePanelWarning();

        AdsManager.CloseCurrentPanelQuestion();

        gameScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ButtonRoulette()
    {
        if (isShowLargeBannerToRoulette)
            AdsManager.ShowPageBanner();

        if (ballController.curNumberRemainingBalls == 0)
            HidePanelWarning();

        AdsManager.CloseCurrentPanelQuestion();

        rouletteScreen.SetActive(true);
        rouletteScreen.GetComponent<Roulette>().backScreen = gameObject;
        gameObject.SetActive(false);
    }

    private void ButtonAddBall()
    {
        if (isShowPanelWarning)
            return;

        AdsManager.ShowRewardedVideo(delegate { ballController.AddBall(1); });
    }

    private void ButtonOk()
    {
        //AdsManager.ShowRewardedVideoWithoutConfirmation(delegate { ballController.AddBall(1); }, ButtonGame);
        AdsManager.ShowRewardedVideoWithoutConfirmation(delegate { ballController.AddBall(1); });
    }

    private void ButtonNo()
    {
        ButtonGame();
    }

    public void ShowPanelWarning()
    {
        if (isShowPanelWarning)
            return;

        AdsManager.CloseCurrentPanelQuestion();

        panelWarning.SetTrigger("Show");
        isShowPanelWarning = true;
    }

    public void HidePanelWarning()
    {
        if (!isShowPanelWarning)
            return;
        panelWarning.SetTrigger("Hide");
        isShowPanelWarning = false;
    }
}
