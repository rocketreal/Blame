using UnityEngine;
using System.Collections;

public class VibrateButtonController : ButtonController {

    [SerializeField]
    GameObject imageOn, imageOff;
    void Start()
    {
        SetStatusButton();
    }
    public override void OnClick()
    {
        base.OnClick();
        GameData.Instance.Vibrate = !GameData.Instance.Vibrate;
        SetStatusButton();
        AndroidGoogleAnalytics.instance.SendEvent
             (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Vibrate", KeyAnalytics.LABEL_CLICK);
    }

    private void SetStatusButton()
    {
        imageOn.SetActive(GameData.Instance.Vibrate);
        imageOff.SetActive(!GameData.Instance.Vibrate);
    }
}
