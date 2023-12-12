using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public AudioSource ambient;
    public AudioSource buttonTap;
    public AudioSource getItem;

    private void Start()
    {
        GameSettings.Instance.toggleSound.isOn = SaveManager.GetKey("Sound", 1) == 0 ? false : true;
        GameSettings.Instance.toggleMusic.isOn = SaveManager.GetKey("Music", 1) == 0 ? false : true;

        GameSettings.Instance.toggleSound.onValueChanged.AddListener(ToggleSound);
        GameSettings.Instance.toggleMusic.onValueChanged.AddListener(ToggleMusic);
    }

    private void ToggleSound(bool _value)
    {
        SaveManager.SetKey("Sound", _value);
        buttonTap.mute = !_value;
        getItem.mute = !_value;
    }

    private void ToggleMusic(bool _value)
    {
        SaveManager.SetKey("Music", _value);
        if (_value)
            ambient.Play();
        else
            ambient.Pause();
    }
}
