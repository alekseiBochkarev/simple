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

    [Header("Button Settings")]
    public Button buttonSettings;
    public Animator panelSettings;
    //public string linkPrivacyPolicy = "https://sites.google.com/site/argamesgeneration/";
    private bool isShowSettings;

    private void Start()
    {
        buttonStart.onClick.AddListener(ButtonStart);
        buttonSettings.onClick.AddListener(ButtonSettings);

        //panelSettings.transform.Find("PrivacyPolicy").GetComponent<Button>().onClick.AddListener(delegate { Application.OpenURL(linkPrivacyPolicy); });
        panelSettings.transform.Find("Close").GetComponent<Button>().onClick.AddListener(CloseSettingsPanel);
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

        if (isShowSettings)
            panelSettings.SetTrigger("Hide");
    }

    /// <summary>
    /// Кнопка политика конфиденциальности
    /// </summary>
    private void ButtonSettings()
    {
        if (isShowSettings)
            return;

        panelSettings.SetTrigger("Show");
        isShowSettings = true;
    }

    private void CloseSettingsPanel()
    {
        panelSettings.SetTrigger("Hide");
        isShowSettings = false;
    }
}
