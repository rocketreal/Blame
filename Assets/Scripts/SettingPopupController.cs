using UnityEngine;
using System.Collections;

public class SettingPopupController : PopupController
{
    public override void Open()
    {
        base.Open();
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        base.Close();
        gameObject.SetActive(false);
    }

    protected override void OnClickOutside()
    {
        base.OnClickOutside();
        Close();
    }
}
