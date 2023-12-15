using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wheel : MonoBehaviour
{
    private float forceRotateWheel;
    private bool isRotateWheel = false;

    public Transform wheelTransform;

    public static Wheel singleton;

    private enum enTypePhrase
    {
        BALL1,
        BALL2,
        BALL3,
        BOOT,
        DOLL,
        HAT
    }
    private enTypePhrase currentTypePhrase;

    public GameObject rays;
    public Animator iconOpenItem;
    public Animator iconOpenCharacter;

    public BallController ballController;

    void Awake ()
    {
        singleton = this;
	}

    public void RotateWheel(float _force)
    {
        if (forceRotateWheel == 0 && !isRotateWheel && TimerToRoulette.singleton.isActiveWheel)
        {
            isRotateWheel = true;
            ClickOnNewItem();

            _force *= 2;

            if (_force > 0 && _force < 8)
                _force = 8;
            if (_force < 0 && _force > -8)
                _force = -8;

            if (_force > 35)
                _force = 35;
            if (_force < -35)
                _force = -35;

            forceRotateWheel = _force;
        }
    }

    void Update ()
    {
        if (forceRotateWheel != 0 && isRotateWheel && TimerToRoulette.singleton.isActiveWheel)
        {
            if (forceRotateWheel > 0)
                forceRotateWheel -= Time.deltaTime * 10;
            else if (forceRotateWheel < 0)
                forceRotateWheel += Time.deltaTime * 10;

            if (forceRotateWheel > -0.1f && forceRotateWheel < 0.1f)
            {
                forceRotateWheel = 0;
            }
            if (forceRotateWheel == 0)
            {
                isRotateWheel = false;
                CheckCurrentStateRoulette(GetNumberPointsOnWheel(wheelTransform.localEulerAngles.z));
                TimerToRoulette.singleton.ResetTimerToNextWheel();
                SoundController.Instance.getItem.Play();
            }

            wheelTransform.localEulerAngles += Vector3.forward * forceRotateWheel;
        }
    }

    private void CheckCurrentStateRoulette(enTypePhrase _type)
    {
        switch (_type)
        {
            case enTypePhrase.BALL1:
                ballController.AddBall(1);
                break;

            case enTypePhrase.BALL2:
                ballController.AddBall(2);
                break;

            case enTypePhrase.BALL3:
                ballController.AddBall(3);
                break;

            case enTypePhrase.BOOT:
                iconOpenItem.GetComponent<Image>().sprite = ballController.OpenRandomItem(ItemController.enTypeItem.BOOTS);
                ShowNewItemIcon();
                break;

            case enTypePhrase.DOLL:
                iconOpenCharacter.GetComponent<Image>().sprite = ballController.OpenRandomDoll();
                ShowNewCharacterIcon();
                break;

            case enTypePhrase.HAT:
                iconOpenItem.GetComponent<Image>().sprite = ballController.OpenRandomItem(ItemController.enTypeItem.HAT);
                ShowNewItemIcon();
                break;
        }
    }

    private void ShowNewItemIcon()
    {
        rays.SetActive(true);
        iconOpenItem.gameObject.SetActive(true);
        iconOpenItem.SetBool("isShow", true);
    }

    private void ShowNewCharacterIcon()
    {
        rays.SetActive(true);
        iconOpenCharacter.gameObject.SetActive(true);
        iconOpenCharacter.SetBool("isShow", true);
    }

    public void ClickOnNewItem()
    {
        rays.SetActive(false);
        iconOpenItem.gameObject.SetActive(false);
        iconOpenCharacter.gameObject.SetActive(false);
    }

    private enTypePhrase GetNumberPointsOnWheel(float _angle)
    {
        if (_angle > 0 && _angle <= 30)
            return enTypePhrase.BALL2;
        else if (_angle > 30 && _angle <= 60)
            return enTypePhrase.BALL3;
        else if (_angle > 60 && _angle <= 135)
            return enTypePhrase.BOOT;
        else if (_angle > 135 && _angle <= 208)
            return enTypePhrase.BALL1;
        else if (_angle > 208 && _angle <= 238)
            return enTypePhrase.DOLL;
        else if (_angle > 238 && _angle <= 315)
            return enTypePhrase.HAT;
        else
            return enTypePhrase.BALL2;
    }
}
