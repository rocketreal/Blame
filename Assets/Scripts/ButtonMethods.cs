using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonMethods : MonoBehaviour
{

    public GameObject mPlayButton, mRateButton, mMainController;
    public Text TxtCountDown, TxtTime;

    //public void PlayButtonOnclick()
    //{
    //    if (mPlayButton != null && mRateButton != null && TxtCountDown != null)
    //    {
    //        // Reset Time
    //        if (mMainController != null)
    //        {
    //            mMainController.GetComponent<MainController>().ResetTime();
    //        }

    //        Debug.Log(MainController.Current.IsPlayTutorial);
            

    //        // Đặt lại giá trị cờ Kết thúc game
    //        GameData.Instance.isEndGame = false;
    //        GameData.Instance.isStartGame = true;
    //        // Ẩn các Button
    //        mPlayButton.SetActive(false);
    //        mRateButton.SetActive(false);
    //    }
    //    else
    //    {
    //        Debug.Log(gameObject.ToString() + " : Ones is null!");
    //    }
    //}

    public void RateButtonOnclick()
    {
        Debug.Log("RateButtonOnclick");
        if (mPlayButton != null && mRateButton != null)
        {
            mPlayButton.SetActive(false);
            mRateButton.SetActive(false);
        }
    }
}
