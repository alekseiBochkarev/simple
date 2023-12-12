using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenDoll : MonoBehaviour
{
    public Image dollImage;
    public Image hatDollImage;
    public Image dressDollImage;
    public Image bottleDollImage;
    public Image bootsDollImage;

    public RightShopController rightShopController;

    void Start ()
    {
        dollImage.sprite = rightShopController.dollIcon[Random.Range(0, rightShopController.dollIcon.Count)];
        hatDollImage.sprite = rightShopController.hatIcons[Random.Range(0, rightShopController.hatIcons.Count)];
        dressDollImage.sprite = rightShopController.dressesIcons[Random.Range(0, rightShopController.dressesIcons.Count)];
        bottleDollImage.sprite = rightShopController.bottleIcons[Random.Range(0, rightShopController.bottleIcons.Count)];
        bootsDollImage.sprite = rightShopController.bootsIcons[Random.Range(0, rightShopController.bootsIcons.Count)];
    }
}
