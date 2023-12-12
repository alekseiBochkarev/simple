using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialLink : MonoBehaviour
{
    public string linkFacebook = "https://www.facebook.com/flamexteam";
    public Button buttonFacebook;

    [Space]
    public string linkTwitter = "https://twitter.com/FLAMEXTEAM";
    public Button buttonTwitter;


    private void Start()
    {
        buttonFacebook.onClick.AddListener(ButtonFacebook);
        buttonTwitter.onClick.AddListener(ButtonTwitter);
    }

    public void ButtonFacebook()
    {
        Application.OpenURL(linkFacebook);
    }

    public void ButtonTwitter()
    {
        Application.OpenURL(linkTwitter);
    }
}
