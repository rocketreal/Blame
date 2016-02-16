using UnityEngine;
using System.Collections;

public class MusicButtonController : ButtonController {

    [SerializeField]
    GameObject imageOn, imageOff;
    public override void OnClick()
    {
        base.OnClick();
        GameData.Instance.Music = !GameData.Instance.Music;
        MainController.Current.CheckMusicStatus();
        imageOn.SetActive(GameData.Instance.Music);
        imageOff.SetActive(!GameData.Instance.Music);
    }

}
