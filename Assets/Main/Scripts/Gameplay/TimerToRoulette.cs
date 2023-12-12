using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerToRoulette : MonoBehaviour
{
    public int maxTimeToNextWheel = 0;
    public float curTimeToNextWheel { get; private set; }

    public bool isActiveWheel { get; private set; } // доступно ли сейчас колесо

    public Roulette roulette;

    public static TimerToRoulette singleton;

    private void Awake()
    {
        singleton = this;
    }

    void Start ()
    {
        if (!SaveManager.HasKey("TimeLastGetWheel"))
        {
            OpenWheel();
        }
        else
            LoadActiveWheel();
    }
	
	void Update ()
    {
        if (!isActiveWheel)
        {
            curTimeToNextWheel -= Time.deltaTime;

            if (curTimeToNextWheel < 0)
                curTimeToNextWheel = 0;

            if (curTimeToNextWheel == 0)
                OpenWheel();
        }
    }

    public void ResetTimerToNextWheel()
    {
        curTimeToNextWheel = maxTimeToNextWheel;
        SaveManager.SetKey("TimeLastGetWheel", DateTime.Now.ToString());
        SaveManager.SetKey("WheelIsActive", 0);
        isActiveWheel = false;
        roulette.StartTimer();
    }

    private void LoadActiveWheel()
    {
        if (SaveManager.GetKey("WheelIsActive", 0) == 1)
        {
            OpenWheel();
            return;
        }

        DateTime timeLastGetWheel = DateTime.Parse(SaveManager.GetKey("TimeLastGetWheel", ""));
        if (DateTime.Now.Subtract(timeLastGetWheel).TotalSeconds > maxTimeToNextWheel)
            OpenWheel();
        else
        {
            curTimeToNextWheel = maxTimeToNextWheel - (int)DateTime.Now.Subtract(timeLastGetWheel).TotalSeconds;
            isActiveWheel = false;
            roulette.StartTimer();
        }
    }

    private void OpenWheel()
    {
        isActiveWheel = true;
        roulette.FinishTimer();
        SaveManager.SetKey("TimeLastGetWheel", DateTime.Now.ToString());
        SaveManager.SetKey("WheelIsActive", 1);
    }
}
