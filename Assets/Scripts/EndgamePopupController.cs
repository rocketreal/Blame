using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndgamePopupController : MonoBehaviour
{
    [SerializeField]
    Text txtScore;
    [SerializeField]
    Text txtTittle;
    [SerializeField]
    Text txtRate;

    private float score;

    public void Start()
    {
        SetScore(MainController.Current.GetBestScore());
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnClickReplayButton()
    {
        MainController.Current.PlayButtonOnclick();
        Close();
    }

    public void SetScore(float score)
    {
        this.score = score;
        txtScore.text = this.score.ToString("00.00", CultureInfo.InvariantCulture) + " s";
    }

    public void SetTittle(bool isBestScore)
    {
        if (isBestScore)
        {
            txtTittle.text = "Best score";
            txtTittle.color = new Color(255f / 255, 244f / 255, 98f / 255);
            txtScore.color = new Color(255f / 255, 244f / 255, 98f / 255);
            txtRate.text = "";
            txtRate.DOText("Rate game!", 1);
            txtRate.transform.DOScale(1.3f, 0.7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            txtTittle.text = "Score";
            txtTittle.color = new Color(188f / 255, 246f / 255, 255f / 255);
            txtScore.color = new Color(188f / 255, 246f / 255, 255f / 255);
            DOTween.Kill(txtRate);
            DOTween.Kill(txtRate.transform);
        }

    }

}
