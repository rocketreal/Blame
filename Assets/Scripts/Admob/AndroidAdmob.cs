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
        AndroidAdMobController.Instance.Init(bannerID);
        AndroidAdMobController.Instance.SetInterstisialsUnitID(interstitialID);
        AndroidAdMobController.Instance.OnInterstitialLoaded += OnInterstisialsLoaded;
        AndroidAdMobController.Instance.OnInterstitialOpened += OnInterstisialsOpen;
        AndroidAdMobController.Instance.OnInterstitialClosed += OnInterstisialsClose;
        StartInterstitialAd();
        LoadInterstitialAd();
    }

    private void StartInterstitialAd()
    {
        AndroidAdMobController.Instance.StartInterstitialAd();
    }

    private void LoadInterstitialAd()
    {
        AndroidAdMobController.Instance.LoadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        AndroidAdMobController.Instance.ShowInterstitialAd();

    }

    public void CreateBannerCustomPos()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(300, 100, GADBannerSize.BANNER);
    }

    public void CreateBannerUpperLeft()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.UpperLeft, GADBannerSize.BANNER);
    }

    public void CreateBannerUpperCneter()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.BANNER);
    }

    public void CreateBannerBottomLeft()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.LowerLeft, GADBannerSize.BANNER);
    }

    public void CreateBannerBottomCenter()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.LowerCenter, GADBannerSize.BANNER);
    }

    public void CreateBannerBottomRight()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.LowerRight, GADBannerSize.BANNER);
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
        AndroidAdMobController.Instance.DestroyBanner(banner.id);
        banner = null;
    }

    public void SmartTOP()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.SMART_BANNER);
    }

    public void SmartBottom()
    {
        banner = AndroidAdMobController.Instance.CreateAdBanner(TextAnchor.LowerCenter, GADBannerSize.SMART_BANNER);
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
