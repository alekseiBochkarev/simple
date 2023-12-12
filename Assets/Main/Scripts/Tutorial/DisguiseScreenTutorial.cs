using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisguiseScreenTutorial : Tutorial
{
    [Space]
    public GameObject screenParent;
    public List<GameObject> panels;

    [Space]
    public GameObject arrowShare;
    public GameObject arrowGallery;
    public GameObject arrowRoulette;

    private void Awake()
    {
        stages.Add(ShowParentScreen);
        stages.Add(ShowPanelTutorial0);
        stages.Add(ShowPanelTutorial1);
        stages.Add(ShowPanelTutorial2);
        stages.Add(ShowPanelTutorial3);
        stages.Add(ShowPanelTutorial4);
        stages.Add(HideParentScreen);
        stages.Add(FinishTutorial);
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

    private void ShowPanelTutorial2()
    {
        panels[1].SetActive(false);
        panels[2].SetActive(true);
        panels[2].transform.Find("Ok").GetComponent<Button>().onClick.AddListener(NextStage);
        ShowArrow(arrowShare);
    }

    private void ShowPanelTutorial3()
    {
        panels[2].SetActive(false);
        panels[3].SetActive(true);
        panels[3].transform.Find("Ok").GetComponent<Button>().onClick.AddListener(NextStage);
        HideArrow(arrowShare);
        ShowArrow(arrowGallery);
    }

    private void ShowPanelTutorial4()
    {
        panels[3].SetActive(false);
        panels[4].SetActive(true);
        panels[4].transform.Find("Ok").GetComponent<Button>().onClick.AddListener(NextStage);
        HideArrow(arrowGallery);
        ShowArrow(arrowRoulette);
    }

    private void HideParentScreen()
    {
        HideArrow(arrowRoulette);
        screenParent.SetActive(false);
        NextStage();
    }

    private void ShowArrow(GameObject _arrow)
    {
        _arrow.SetActive(true);
    }

    private void HideArrow(GameObject _arrow)
    {
        _arrow.SetActive(false);
    }
}
