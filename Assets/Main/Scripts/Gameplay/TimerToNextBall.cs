using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerToNextBall : MonoBehaviour
{
    public Text textTimerHeader;
    public Text textTimerToNextBall;
    public Image imageOk;

    public int maxTimeToNextBall = 0;
    public float curTimeToNextBall { get; private set; }

    public int isActiveWheel { get; private set; } // доступно ли сейчас колесо

    public BallController ballController;

    public static TimerToNextBall singleton;

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        curTimeToNextBall = maxTimeToNextBall;

        if (!SaveManager.HasKey("TimeLastGetBall"))
        {
            SetStartBalls();
        }
        else
        {
            LoadBalls();
        }
    }

    void Update()
    {
        if (ballController.curNumberRemainingBalls >= 3)
        {
            if (textTimerToNextBall.enabled)
            {
                ResetTimerToNextBall();
                textTimerToNextBall.enabled = false;
                textTimerHeader.enabled = false;
                imageOk.enabled = true;
            }
        }
        else
        {
            if (!textTimerToNextBall.enabled)
            {
                textTimerToNextBall.enabled = true;
                textTimerHeader.enabled = true;
                imageOk.enabled = false;
            }

            curTimeToNextBall -= Time.deltaTime;

            if (curTimeToNextBall < 0)
                curTimeToNextBall = 0;

            if (curTimeToNextBall == 0)
            {
                AddBalls(1);
                ResetTimerToNextBall();
            }

            UpdateText();
        }
    }

    public void ResetTimerToNextBall()
    {
        curTimeToNextBall = maxTimeToNextBall;
    }

    private void LoadBalls()
    {
        ballController.curNumberRemainingBalls = SaveManager.GetKey("NumberRemainingBalls", 0);

        if (SaveManager.GetKey("NumberRemainingBalls", 0) < 3)
        {
            DateTime timeLastGetBall = DateTime.Parse(SaveManager.GetKey("TimeLastGetBall", ""));
            //Debug.Log(timeLastGetBall);

            if (DateTime.Now.Subtract(timeLastGetBall).TotalSeconds > maxTimeToNextBall)
                AddBalls((int)DateTime.Now.Subtract(timeLastGetBall).TotalSeconds / maxTimeToNextBall);
            else
            {
                curTimeToNextBall = maxTimeToNextBall - (int)DateTime.Now.Subtract(timeLastGetBall).TotalSeconds;
            }
        }
    }

    private void AddBalls(int _countBalls)
    {
        if (ballController.curNumberRemainingBalls > 2)
            return;

        ballController.curNumberRemainingBalls += _countBalls;
        if (ballController.curNumberRemainingBalls > 3)
            ballController.curNumberRemainingBalls = 3;

        if (ballController.GetComponent<ShopMenu>().isShowPanelWarning)
            ballController.GetComponent<ShopMenu>().HidePanelWarning();

        if (!ballController.isBallActive)
            ballController.SpawnBall();

        SaveManager.SetKey("TimeLastGetBall", DateTime.Now.ToString());
        SaveManager.SetKey("NumberRemainingBalls", ballController.curNumberRemainingBalls);
    }

    private void SetStartBalls()
    {
        ballController.curNumberRemainingBalls = 3;
        SaveManager.SetKey("TimeLastGetBall", DateTime.Now.ToString());
        SaveManager.SetKey("NumberRemainingBalls", ballController.curNumberRemainingBalls);
    }

    private void UpdateText()
    {
        textTimerToNextBall.text = ((int)curTimeToNextBall / 60) + ":" + (curTimeToNextBall % 60 < 10 ? "0" : "") + (int)(curTimeToNextBall % 60);
    }
}
