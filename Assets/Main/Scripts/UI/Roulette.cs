using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Roulette : MonoBehaviour
{
    [Header("Button back")]
    public Button buttonBack;
    public GameObject backScreen;
    public bool isShowLargeBannerBack;

    [Header("Header")]
    public GameObject headerTextTurn;
    public Text textTimer;
    public Text headerTimer;

    private void Start ()
    {
        buttonBack.onClick.AddListener(ButtonBack);
    }
	
	void FixedUpdate ()
    {
		if (!TimerToRoulette.singleton.isActiveWheel)
        {
            textTimer.text = (((int)TimerToRoulette.singleton.curTimeToNextWheel / 60) / 60).ToString() + ":"
                + (((int)TimerToRoulette.singleton.curTimeToNextWheel / 60) % 60).ToString() + ":"
                + (int)TimerToRoulette.singleton.curTimeToNextWheel % 60;
        }
	}

    private void ButtonBack ()
    {
        if (isShowLargeBannerBack)
            AdsManager.ShowPageBanner();

        Wheel.singleton.ClickOnNewItem();

        if (backScreen.GetComponent<ShopMenu>() != null)
            backScreen.GetComponent<BallController>().SpawnBall();

        backScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        headerTextTurn.SetActive(false);
        headerTimer.gameObject.SetActive(true);
        textTimer.gameObject.SetActive(true);
    }

    public void FinishTimer()
    {
        headerTextTurn.SetActive(true);
        headerTimer.gameObject.SetActive(false);
        textTimer.gameObject.SetActive(false);
    }
}
