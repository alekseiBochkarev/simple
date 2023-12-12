using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AdsManager : Singleton<AdsManager>
{
    public bool isShowPanelQuestion;
    public Animator panelQuestionRewardsAds;

    public bool isShowPanelLoad;
    public Animator panelLoad;

    public bool isShowPanelFinish;
    public Animator panelFinish;

    public bool isShowPanelNoFinish;
    public Animator panelNoFinish;

    public bool isShowPanelVideoNoLoaded;
    public Animator panelVideoNoLoaded;

    private Action funcClosePanelLoad;
    private bool isShowQuestion = false;

    private int countPageAds = 0;

    private void ShowRewardedVideoLocal(Action _funcAfterVideo, Action _funcButtonNo = null, Action _funcClosePanelLoad = null, bool rewardIsCharge = false)
    {
        /*if (rewardIsCharge)
        {
            panelQuestionRewardsAds.transform.Find("TextProduct").gameObject.SetActive(false);
            panelQuestionRewardsAds.transform.Find("TextCharge").gameObject.SetActive(true);
        }
        else
        {
            panelQuestionRewardsAds.transform.Find("TextProduct").gameObject.SetActive(true);
            panelQuestionRewardsAds.transform.Find("TextCharge").gameObject.SetActive(false);
        }*/

        if (_funcClosePanelLoad != null)
        {
            funcClosePanelLoad = _funcClosePanelLoad;
        }

        ShowPanel(panelQuestionRewardsAds);
        isShowQuestion = true;
        panelQuestionRewardsAds.transform.Find("OK").GetComponent<Button>().onClick.RemoveAllListeners();
        panelQuestionRewardsAds.transform.Find("NO").GetComponent<Button>().onClick.RemoveAllListeners();
        panelQuestionRewardsAds.transform.Find("OK").GetComponent<Button>().onClick.AddListener(delegate { ButtonAdsOk(_funcAfterVideo); });
        panelQuestionRewardsAds.transform.Find("NO").GetComponent<Button>().onClick.AddListener(delegate { ButtonAdsNo(_funcButtonNo); });
    }

    private void ShowRewardedVideoWithoutConfirmationLocal(Action _funcAfterVideo)
    {
        ButtonAdsOk(_funcAfterVideo);
    }

    private void ShowPageBannerLocal()
    {
        AdmobBackground.Instance.ShowPageAds();
    }

    private void ButtonAdsOk(Action _funcAfterVideo)
    {
        if (isShowQuestion)
        {
            panelQuestionRewardsAds.SetTrigger("Hide");
        }
        isShowQuestion = false;

#if UNITY_EDITOR
        _funcAfterVideo.Invoke();
        return;
#endif

        AdmobBackground.Instance.RewardEventRewarded.RemoveAllListeners();
        //AdmobBackground.Instance.RewardedVideoSkipped.RemoveAllListeners();

        AdmobBackground.Instance.RewardEventRewarded.AddListener(delegate { OnVideoFinished(_funcAfterVideo); });
        //AdmobBackground.Instance.RewardedVideoSkipped.AddListener(delegate { OnVideoNoFinished(); });

        AdmobBackground.Instance.ShowRewardedAds();

        if (isShowPanelLoad)
        {
            ShowPanel(panelLoad);

            if (funcClosePanelLoad != null)
            {
                panelLoad.transform.Find("Close").GetComponent<Button>().onClick.RemoveAllListeners();
                panelLoad.transform.Find("Close").GetComponent<Button>().onClick.AddListener(funcClosePanelLoad.Invoke);
                panelLoad.transform.Find("Close").GetComponent<Button>().onClick.AddListener(delegate { panelLoad.SetTrigger("Hide"); });
            }
            else
            {
                panelLoad.transform.Find("Close").GetComponent<Button>().onClick.RemoveAllListeners();
                panelLoad.transform.Find("Close").GetComponent<Button>().onClick.AddListener(delegate { panelLoad.SetTrigger("Hide"); });
            }
        }
    }

    private void OnVideoFinished(Action _funcAfterVideo)
    {
        if (isShowPanelLoad)
        {
            funcClosePanelLoad = null;
            if (panelLoad.isActiveAndEnabled)
                panelLoad.SetTrigger("Hide");
        }

        if (isShowPanelFinish)
            ShowPanel(panelFinish);

        if (_funcAfterVideo != null)
            _funcAfterVideo.Invoke();
    }

    private void OnVideoNoFinished()
    {
        if (isShowPanelLoad)
        {
            funcClosePanelLoad = null;
            if (panelLoad.isActiveAndEnabled)
                panelLoad.SetTrigger("Hide");
        }

        if (isShowPanelNoFinish)
            ShowPanel(panelNoFinish);
    }

    private void OnVideoNoLoaded()
    {
        if (isShowPanelLoad)
        {
            funcClosePanelLoad = null;
            if (panelLoad.isActiveAndEnabled)
                panelLoad.SetTrigger("Hide");
        }

        if (isShowPanelVideoNoLoaded)
            ShowPanel(panelVideoNoLoaded);
    }

    private void ButtonAdsNo(Action _funcButtonNo)
    {
        if (_funcButtonNo != null)
            _funcButtonNo.Invoke();
        funcClosePanelLoad = null;
        panelQuestionRewardsAds.SetTrigger("Hide");
        isShowQuestion = false;
    }

    private void CloseCurrentPanelQuestionLocal()
    {
        if (!isShowQuestion)
            return;

        ButtonAdsNo(null);
    }

    private void ShowPanel(Animator _anim)
    {
        //_anim.transform.localScale = new Vector3(1.6f, 0.5f, 1f);
        _anim.gameObject.SetActive(true);
        _anim.SetTrigger("Show");
    }

    /// <summary>
    /// Не пропускаемая реклама с подтверждением
    /// </summary>
    public static void ShowRewardedVideo(Action _funcAfterVideo, Action _funcButtonNo = null, Action _funcClosePanelLoad = null, bool rewardIsCharge = false)
    {
        if (!Instance.isShowPanelQuestion)
        {
            Debug.LogError("isShowPanelQuestion = false");
            return;
        }

        Instance.ShowRewardedVideoLocal(_funcAfterVideo, _funcButtonNo, _funcClosePanelLoad, rewardIsCharge);
    }

    /// <summary>
    /// Не пропускаемая реклама без подтверждения
    /// </summary>
    public static void ShowRewardedVideoWithoutConfirmation(Action _funcAfterVideo)
    {
        Instance.ShowRewardedVideoWithoutConfirmationLocal(_funcAfterVideo);
    }

    /// <summary>
    /// Можно ли показать рекламу
    /// </summary>
    public static bool IsReadyRewardedVideo()
    {
        return AdmobBackground.Instance.IsReadyRewardedAds();
    }

    /// <summary>
    /// Межстраничная реклама
    /// </summary>
    public static void ShowPageBanner()
    {
        if (Instance.countPageAds < 2)
        {
            Instance.countPageAds++;
            return;
        }
        else
        {
            Instance.countPageAds = 0;
        }

        Instance.ShowPageBannerLocal();
    }

    /// <summary>
    /// Можно ли показать межстраничку
    /// </summary>
    public static bool IsReadyPageBanner()
    {
        return AdmobBackground.Instance.IsReadyPageAds();
    }

    /// <summary>
    /// Закрыть текущее окно подтверждения просмотра рекламы
    /// </summary>
    public static void CloseCurrentPanelQuestion()
    {
        Instance.CloseCurrentPanelQuestionLocal();
    }
}
