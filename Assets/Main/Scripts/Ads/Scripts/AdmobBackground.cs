using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

public class AdmobBackground : Singleton<AdmobBackground>
{
    /*public Button buttonInterstitial;
    public Button buttonRewarded;
    public Button buttonBanner;*/

    public string appId;
    public string adUnitIdInterstitial;
    public string adUnitIdRewarded;
    public string adUnitIdBanner;

    /*[Space]
    public Image imInterstitial;
    public Image imRewarded;*/

    [Space]
    public bool loadBannerOnStart = true;

    [Serializable]
    public class EventArgsUnityEvent : UnityEvent<object, EventArgs> { };
    //[Serializable]
    //public class FailedToLoadUnityEvent : UnityEvent<object, AdFailedToLoadEventArgs> { };
    //[Serializable]
    //public class RewardUnityEvent : UnityEvent<object, Reward> { };

    // banner
    //private BannerView bannerView;

    public EventArgsUnityEvent BannerEventLoaded { get; set; } = new EventArgsUnityEvent();
   // public FailedToLoadUnityEvent BannerEventFailedToLoad { get; set; } = new FailedToLoadUnityEvent();
    public EventArgsUnityEvent BannerEventOpening { get; set; } = new EventArgsUnityEvent();
    public EventArgsUnityEvent BannerEventClosed { get; set; } = new EventArgsUnityEvent();
    public EventArgsUnityEvent BannerEventLeavingApplication { get; set; } = new EventArgsUnityEvent();

    // interstitial
    //private InterstitialAd interstitial;

    public EventArgsUnityEvent InterstitialEventLoaded { get; set; } = new EventArgsUnityEvent();
   // public FailedToLoadUnityEvent InterstitialEventFailedToLoad { get; set; } = new FailedToLoadUnityEvent();
    public EventArgsUnityEvent InterstitialEventOpening { get; set; } = new EventArgsUnityEvent();
    public EventArgsUnityEvent InterstitialEventClosed { get; set; } = new EventArgsUnityEvent();
    public EventArgsUnityEvent InterstitialEventLeavingApplication { get; set; } = new EventArgsUnityEvent();

    // reward
    //private RewardBasedVideoAd rewardedVideo;

    public EventArgsUnityEvent RewardEventLoaded { get; set; } = new EventArgsUnityEvent();
  //  public FailedToLoadUnityEvent RewardEventFailedToLoad { get; set; } = new FailedToLoadUnityEvent();
    public EventArgsUnityEvent RewardEventOpening { get; set; } = new EventArgsUnityEvent();
    public EventArgsUnityEvent RewardEventStarted { get; set; } = new EventArgsUnityEvent();
 //   public RewardUnityEvent RewardEventRewarded { get; set; } = new RewardUnityEvent();
    public EventArgsUnityEvent RewardEventClosed { get; set; } = new EventArgsUnityEvent();
    public EventArgsUnityEvent RewardEventLeavingApplication { get; set; } = new EventArgsUnityEvent();

    private void Start()
    {
        /*buttonInterstitial.onClick.AddListener(ButtonInterstitial);
        buttonRewarded.onClick.AddListener(ButtonRewarded);
        buttonBanner.onClick.AddListener(ButtonBanner);*/

       /* DontDestroyOnLoad(gameObject);

        if (appId == "")
            Debug.LogError("appId is empty");
        if (adUnitIdInterstitial == "")
            Debug.LogError("adUnitIdInterstitial is empty");
        if (adUnitIdRewarded == "")
            Debug.LogError("adUnitIdRewarded is empty");
        if (adUnitIdBanner == "")
            Debug.LogError("adUnitIdBanner is empty");

        MobileAds.Initialize(appId);

        if (loadBannerOnStart)
        {
            ShowBanner();
            InitializeBannerEvents();
        }

        RequestInterstitial();
        InitializeInterstitialEvents();

        rewardedVideo = RewardBasedVideoAd.Instance;
        RequestRewardedVideo();
        InitializationRewardEvents();*/

#if !UNITY_EDITOR
      /*  StartCoroutine(ReloadAds());*/
#endif
    }

    private void InitializeBannerEvents()
    {
       /* bannerView.OnAdLoaded += (object a, EventArgs b) => BannerEventLoaded.Invoke(a, b);
        bannerView.OnAdFailedToLoad += (object a, AdFailedToLoadEventArgs b) => BannerEventFailedToLoad.Invoke(a, b);
        bannerView.OnAdOpening += (object a, EventArgs b) => BannerEventOpening.Invoke(a, b);
        bannerView.OnAdClosed += (object a, EventArgs b) => BannerEventClosed.Invoke(a, b);
        bannerView.OnAdLeavingApplication += (object a, EventArgs b) => BannerEventLeavingApplication.Invoke(a, b);*/
    }

    private void InitializeInterstitialEvents()
    {
       /* interstitial.OnAdLoaded += (object a, EventArgs b) => InterstitialEventLoaded.Invoke(a, b);
        interstitial.OnAdFailedToLoad += (object a, AdFailedToLoadEventArgs b) => InterstitialEventFailedToLoad.Invoke(a, b);
        interstitial.OnAdOpening += (object a, EventArgs b) => InterstitialEventOpening.Invoke(a, b);
        interstitial.OnAdClosed += (object a, EventArgs b) => InterstitialEventClosed.Invoke(a, b);
        interstitial.OnAdLeavingApplication += (object a, EventArgs b) => InterstitialEventLeavingApplication.Invoke(a, b);*/
    }

    private void InitializationRewardEvents()
    {
      /*  rewardedVideo.OnAdLoaded += (object a, EventArgs b) => RewardEventLoaded.Invoke(a, b);
        rewardedVideo.OnAdFailedToLoad += (object a, AdFailedToLoadEventArgs b) => RewardEventFailedToLoad.Invoke(a, b);
        rewardedVideo.OnAdOpening += (object a, EventArgs b) => RewardEventOpening.Invoke(a, b);
        rewardedVideo.OnAdStarted += (object a, EventArgs b) => RewardEventStarted.Invoke(a, b);
        rewardedVideo.OnAdRewarded += (object a, Reward b) => RewardEventRewarded.Invoke(a, b);
        rewardedVideo.OnAdClosed += (object a, EventArgs b) => RewardEventClosed.Invoke(a, b);
        rewardedVideo.OnAdLeavingApplication += (object a, EventArgs b) => RewardEventLeavingApplication.Invoke(a, b);*/
    }

   /* private IEnumerator ReloadAds()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            if (!interstitial.IsLoaded())
                RequestInterstitial();

            if (!rewardedVideo.IsLoaded())
                RequestRewardedVideo();
        }
    }*/

    /*private void Update()
    {
        if (interstitial.IsLoaded())
            imInterstitial.color = Color.green;
        else
            imInterstitial.color = Color.red;

        if (rewardBasedVideo.IsLoaded())
            imRewarded.color = Color.green;
        else
            imRewarded.color = Color.red;
    }*/

    #region interstitial

    /// <summary>
    /// Межстраничная (пропускаемое видео)
    /// </summary>
   /* public void ShowPageAds()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }*/

  /*  private void RequestInterstitial()
    {
        interstitial = new InterstitialAd(adUnitIdInterstitial);
        AdRequest requestInterstitial = new AdRequest.Builder().Build();
        interstitial.LoadAd(requestInterstitial);
    }*/

  /*  public bool IsReadyPageAds()
    {
        return interstitial.IsLoaded();
    }*/

    #endregion

    #region rewarded

    /// <summary>
    /// Не пропускаемая реклама (за вознаграждение)
    /// </summary>
  /*  public void ShowRewardedAds()
    {
        if (rewardedVideo.IsLoaded())
        {
            rewardedVideo.Show();
        }
    }*/

  /*  private void RequestRewardedVideo()
    {
        AdRequest requestRewarded = new AdRequest.Builder().Build();
        rewardedVideo.LoadAd(requestRewarded, adUnitIdRewarded);
    }*/

   /* public bool IsReadyRewardedAds()
    {
        return rewardedVideo.IsLoaded();
    }*/

    #endregion

    #region banner

    /// <summary>
    /// Отобразить банер
    /// </summary>
    /*public void ShowBanner()
    {
        bannerView = new BannerView(adUnitIdBanner, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }*/

    #endregion

    private void OnDestroy()
    {
     /*   bannerView.Destroy();
        interstitial.Destroy();*/
    }
}
