using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : Singleton<GameSettings>
{
    [Header("Audio")]
    public Toggle toggleSound;
    public Toggle toggleMusic;

    [Header("Facebook")]
    public Button buttonFacebook;
    public string linkFacebook = "https://www.facebook.com/flamexteam";

    [Header("Twitter")]
    public Button buttonTwitter;
    public string linkTwitter = "https://twitter.com/FLAMEXTEAM";

    [Header("More Games")]
    public Button buttonMoreGames;
    public string linkMoreGames;

    //[Space]
    //public Button buttonLanguage;

    [Header("Privacy Policy")]
    public Button buttonPrivacyPolicy;
    public string linkPrivacyPolicy;

    [Header("FPS")]
    public bool lock60fps = true;

    private void Awake()
    {
        if (lock60fps)
            Application.targetFrameRate = 60;
    }

    private void Start()
    {
        buttonFacebook.onClick.AddListener(() => ButtonLink(linkFacebook));
        buttonTwitter.onClick.AddListener(() => ButtonLink(linkTwitter));
        buttonMoreGames.onClick.AddListener(() => ButtonLink(linkMoreGames));
        buttonPrivacyPolicy.onClick.AddListener(() => ButtonLink(linkPrivacyPolicy));
    }

    private void ButtonLink(string _link)
    {
        Application.OpenURL(_link);
    }
}
