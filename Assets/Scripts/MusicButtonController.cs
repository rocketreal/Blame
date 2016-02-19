using UnityEngine;
using System.Collections;

public class MusicButtonController : ButtonController {

    [SerializeField]
    GameObject imageOn, imageOff;

    void Start()
    {
        SetStatusButton();
    }

    public override void OnClick()
    {
        base.OnClick();
        GameData.Instance.Music = !GameData.Instance.Music;
        MainController.Current.CheckMusicStatus();
        SetStatusButton();
        AndroidGoogleAnalytics.instance.SendEvent
             (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Music", KeyAnalytics.LABEL_CLICK);
    }

    private void SetStatusButton()
    {
        imageOn.SetActive(GameData.Instance.Music);
        imageOff.SetActive(!GameData.Instance.Music);
    }
}
