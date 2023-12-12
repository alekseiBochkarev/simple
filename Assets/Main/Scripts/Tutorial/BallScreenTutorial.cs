using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScreenTutorial : Tutorial
{
    [Space]
    public GameObject screenParent;
    public List<GameObject> panels;

    public List<GameObject> hiddenObjects;

    private void Awake()
    {
        stages.Add(ShowParentScreen);
        stages.Add(HideObjects);
        stages.Add(ShowPanelTutorial0);
        stages.Add(ShowPanelTutorial1);
        stages.Add(HidePanelTutorial1);
        stages.Add(HideParentScreen);
        stages.Add(AddBallCompleteEvent);
        stages.Add(RemoveBallCompleteEvent);
        stages.Add(ShowObjects);
        stages.Add(ShowParentScreen);
        stages.Add(ShowPanelTutorial2);
        stages.Add(HideParentScreen);
        stages.Add(FinishTutorial);
    }

    private void HideObjects()
    {
        foreach (GameObject i in hiddenObjects)
            i.SetActive(false);
        NextStage();
    }

    private void ShowParentScreen()
    {
        screenParent.SetActive(true);
        NextStage();
    }

    private void ShowPanelTutorial0()
    {
        panels[0].SetActive(true);
        panels[0].transform.Find("Ok").GetComponent<Button>().onClick.AddListener(NextStage);
    }

    private void ShowPanelTutorial1()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
        panels[1].transform.Find("Ok").GetComponent<Button>().onClick.AddListener(NextStage);
    }

    private void HidePanelTutorial1()
    {
        panels[1].SetActive(false);
        NextStage();
    }

    private void HideParentScreen()
    {
        screenParent.SetActive(false);
        NextStage();
    }

    private void AddBallCompleteEvent()
    {
        BallController.Instance.BallComplete.AddListener(NextStage);
    }

    private void RemoveBallCompleteEvent()
    {
        BallController.Instance.BallComplete.RemoveListener(NextStage);
        NextStage();
    }

    private void ShowObjects()
    {
        foreach (GameObject i in hiddenObjects)
            i.SetActive(true);
        NextStage();
    }

    private void ShowPanelTutorial2()
    {
        panels[2].SetActive(true);
        panels[2].transform.Find("Ok").GetComponent<Button>().onClick.AddListener(NextStage);
    }
}
