using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class EndgamePopupController : MonoBehaviour
{
    [SerializeField]
    Text txtScore;
    [SerializeField]
    Text txtTittle;

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
        }
        else
        {
            txtTittle.text = "Score";
            txtTittle.color = new Color(188f / 255, 246f / 255, 255f / 255);
            txtScore.color = new Color(188f / 255, 246f / 255, 255f / 255);
        }

    }

}
