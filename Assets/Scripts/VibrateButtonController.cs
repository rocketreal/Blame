using UnityEngine;
using System.Collections;

public class VibrateButtonController : ButtonController {

    [SerializeField]
    GameObject imageOn, imageOff;
    public override void OnClick()
    {
        base.OnClick();
        GameData.Instance.Vibrate = !GameData.Instance.Vibrate;
        imageOn.SetActive(GameData.Instance.Vibrate);
        imageOff.SetActive(!GameData.Instance.Vibrate);
    }
}
