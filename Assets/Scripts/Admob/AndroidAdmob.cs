using UnityEngine;

public class AndroidAdmob
{
    private GoogleMobileAdBanner banner;
    private bool IsInterstisialsAdReady = false;

    private static AndroidAdmob instance;
    public static AndroidAdmob Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AndroidAdmob();
            }
            return instance;
        }
    }

    public AndroidAdmob()
    {

    }
    public void Init(string bannerID, string interstitialID)
    {
        AndroidAdMobController.instance.Init(bannerID);
        AndroidAdMobController.instance.SetInterstisialsUnitID(interstitialID);
        AndroidAdMobController.instance.OnInterstitialLoaded += OnInterstisialsLoaded;
        AndroidAdMobController.instance.OnInterstitialOpened += OnInterstisialsOpen;
        AndroidAdMobController.instance.OnInterstitialClosed += OnInterstisialsClose;
        StartInterstitialAd();
        LoadInterstitialAd();
    }

    private void StartInterstitialAd()
    {
        AndroidAdMobController.instance.StartInterstitialAd();
    }

    private void LoadInterstitialAd()
    {
        AndroidAdMobController.instance.LoadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        AndroidAdMobController.instance.ShowInterstitialAd();
    }

    public void CreateBannerCustomPos()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(300, 100, GADBannerSize.BANNER);
    }

    public void CreateBannerUpperLeft()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.UpperLeft, GADBannerSize.BANNER);
    }

    public void CreateBannerUpperCneter()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.BANNER);
    }

    public void CreateBannerBottomLeft()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.LowerLeft, GADBannerSize.BANNER);
    }

    public void CreateBannerBottomCenter()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.LowerCenter, GADBannerSize.BANNER);
    }

    public void CreateBannerBottomRight()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.LowerRight, GADBannerSize.BANNER);
    }

    public void HideBanner()
    {
        banner.Hide();
    }


    public void ShowBanner()
    {
        banner.Show();
    }

    public void BannerRefresh()
    {
        banner.Refresh();
    }

    public void BannerDestroy()
    {
        AndroidAdMobController.instance.DestroyBanner(banner.id);
        banner = null;
    }

    public void SmartTOP()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.SMART_BANNER);
    }

    public void SmartBottom()
    {
        banner = AndroidAdMobController.instance.CreateAdBanner(TextAnchor.LowerCenter, GADBannerSize.SMART_BANNER);
    }

    public void ChangePostToMiddle()
    {
        banner.SetBannerPosition(TextAnchor.MiddleCenter);
    }

    public void ChangePostRandom()
    {
        banner.SetBannerPosition(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
    }

    //--------------------------------------
    //  EVENTS
    //--------------------------------------

    private void OnInterstisialsLoaded()
    {
        IsInterstisialsAdReady = true;
    }

    private void OnInterstisialsOpen()
    {
        IsInterstisialsAdReady = false;
    }

    private void OnInterstisialsClose()
    {
        LoadInterstitialAd();
    }
}
