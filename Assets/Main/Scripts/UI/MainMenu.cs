using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Button Start")]
    public Button buttonStart;
    public GameObject shopScreen;
    public GameObject gameScreen;
    public bool isShowLargeBanner;
    
    private void Start()
    {
        buttonStart.onClick.AddListener(ButtonStart);
    }

    /// <summary>
    /// Кнопка старт
    /// </summary>
    public void ButtonStart()
    {
        if (isShowLargeBanner)
            AdsManager.ShowPageBanner();
        if (!SaveManager.HasKey("FirstRun"))
        {
            shopScreen.SetActive(true);
            shopScreen.GetComponent<BallController>().SpawnBall();
            SaveManager.SetKey("FirstRun", 1);
        }
        else
        {
            gameScreen.SetActive(true);
        }
        gameObject.SetActive(false);

    }
}
