using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : Singleton<GameSettings>
{
    [Header("Audio")]
    public Toggle toggleSound;
    public Toggle toggleMusic;

    [Header("FPS")]
    public bool lock60fps = true;

    private void Awake()
    {
        if (lock60fps)
            Application.targetFrameRate = 60;
    }

    private void Start()
    {
        
    }

    private void ButtonLink(string _link)
    {
        
    }
}
