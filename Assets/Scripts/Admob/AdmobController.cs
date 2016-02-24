using System.IO;
using UnityEngine;

public class AdmobController : MonoBehaviour
{
    public static AdmobController Current { set; get; }
    public string androidBannerID = "";
    public string androidInterstitalID = "";
    void Awake()
    {
        Current = this;
#if UNITY_ANDROID
        AndroidAdmob.Instance.Init(androidBannerID, androidInterstitalID);
#elif UNITY_IOS

#elif UNITY_WP8

#endif
    }

    public void StartBanner()
    {
#if UNITY_ANDROID
        AndroidAdmob.Instance.SmartTOP();
#elif UNITY_IOS

#elif UNITY_WP8

#endif
    }

    public void ShowBanner()
    {
#if UNITY_ANDROID
        AndroidAdmob.Instance.ShowBanner();
#elif UNITY_IOS

#elif UNITY_WP8

#endif
    }

    public void HiheBanner()
    {
#if UNITY_ANDROID
        AndroidAdmob.Instance.HideBanner();
#elif UNITY_IOS

#elif UNITY_WP8

#endif
    }

    public void ShowInterstitial()
    {
#if UNITY_ANDROID
        AndroidAdmob.Instance.ShowInterstitialAd();
        
#elif UNITY_IOS

#elif UNITY_WP8

#endif
    }
}
