using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Tutorial : MonoBehaviour
{
    public string tutorialName;

    protected List<Action> stages = new List<Action>();
    protected int IndexStage { get; private set; } = 0;

    protected virtual void Start()
    {
        if (SaveManager.GetKey($"Tutorial{tutorialName}", 0) == 0)
            StartTutorial();
        else
            return;
    }

    protected void StartTutorial()
    {
        SaveManager.SetKey($"Tutorial{tutorialName}", 1);

        if (stages.Count == 0)
        {
            Debug.LogError("stages.Count == 0");
            return;
        }

        IndexStage = 0;
        stages[IndexStage].Invoke();
    }

    protected void NextStage()
    {
        IndexStage++;
        stages[IndexStage].Invoke();
    }

    protected void FinishTutorial()
    {

    }
}
