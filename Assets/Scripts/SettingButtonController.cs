using UnityEngine;
using System.Collections;

public class SettingButtonController : MonoBehaviour {

    [SerializeField]
    GameObject settingPopup;

    public void OnClick()
    {
        settingPopup.SetActive(!settingPopup.activeSelf);
    }
}
