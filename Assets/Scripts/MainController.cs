using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainController : MonoBehaviour
{
    public static MainController Current { get; set; }
    void Awake()
    {
        Current = this;
    }

    // Public Members
    public Text mTxtTime, mTxtBest;
    public GameObject mGobjPlayButton, mPlayer, mPlayerTouch, mGobjRateButton;
    public GameObject[] arrEnemies;
    [Header("Audio")]
    public GameObject BGMusicTime;
    public GameObject BGMusicBest;
    public GameObject BGMusicEnterGame;
    public GameObject Arows;
    public Sprite sprResumeButton;
    //public GameObject mPlayButton, mRateButton;
    public Text TxtCountDown, txtTutorial;

    public Button handPointButton;

    //SerializeField
    [SerializeField]
    private EndgamePopupController endgamePopupController;
    [SerializeField]
    private ItemController itemController;

    // Private Members
    float flTime, flBest;
    bool isCalledActiveEnemy, flagChangedMusic;
    string dataTxtBestTime = "dataTxtBestTime";
    bool isFirstTime;
    const int DelayTimeItem = 5;
    bool bestMusicPlaying;
    int playCount;
    bool isTutorialTextOn = true;

    public bool IsPlayTutorial
    {
        get
        {
            return PlayerPrefs.GetInt("PlayTutorial", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("PlayTutorial", value ? 1 : 0);
        }
    }
    void Start()
    {
        GetBestScoreFromPrefs();
        SetBestScore();
        CheckMusicStatus();
        AndroidGoogleAnalytics.instance.StartTracking();
        AndroidGoogleAnalytics.instance.SetTrackerID("UA-71027817-2");
        endgamePopupController.Open();
        AdmobController.Current.StartBanner();
        StartCoroutine(IEShowInterstitial());
        AndroidGoogleAnalytics.instance.SendView(KeyAnalytics.CATEGORY_GAMEPLAY);
    }

    IEnumerator IEShowInterstitial()
    {
        yield return new WaitForSeconds(4f);
        AdmobController.Current.ShowInterstitial();
    }

#if UNITY_EDITOR
    [MenuItem("GameSetting/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }
#endif

    private void GetBestScoreFromPrefs()
    {
        flBest = PlayerPrefs.GetFloat(dataTxtBestTime, 0.0f);
    }

    private void SetBestScore()
    {
        mTxtBest.text = flBest.ToString("00.##", CultureInfo.InvariantCulture) + " s";
    }


    // Update 
    void Update()
    {
        if (GameData.Instance.isStartGame)
        {
            // Time
            if (GameData.Instance.isEndGame == false
                && GameData.Instance.canPlay == true
                && GameData.Instance.playerDied == false)
            {
                CoutingTime();
            }
            // Nếu time < best thì chạy nhạc nền time, ngược lại thì chạy nhạc nền best
            if (flTime > flBest && flagChangedMusic == false && GameData.Instance.isEndGame == false)
            {
                flagChangedMusic = true;
                bestMusicPlaying = true;
                UpdateMusic();
            }
        }
        if (GameData.Instance.isEndGame)
        {
            CancelInvoke("InvokeShowItem");
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GetKeyBack();
        }
    }
    public void CheckMusicStatus()
    {
        AudioListener.pause = !GameData.Instance.Music;
    //    if (GameData.Instance.Music)
    //    {
    //        //if (bestMusicPlaying)
    //        //{
    //        //    BGMusicBest.GetComponent<AudioSource>().Play();
    //        //}
    //        //else
    //        //{
    //        //    BGMusicTime.GetComponent<AudioController>().PlayFadeIn();
    //        //}
    //    }
    //    else
    //    {
    //        //BGMusicBest.GetComponent<AudioController>().StopFadeOut();
    //        //BGMusicTime.GetComponent<AudioController>().StopFadeOut();
    //        //BGMusicEnterGame.GetComponent<AudioController>().StopFadeOut();
    //    }
    }

    private void GetKeyBack()
    {

    }
    private void UpdateMusic()
    {
        if (GameData.Instance.Music)
        {
            BGMusicTime.GetComponent<AudioSource>().Stop();
            BGMusicBest.GetComponent<AudioSource>().Play();
        }
    }

    private void InvokeShowItem()
    {
        itemController.Show();
    }

    private void CoutingTime()
    {
        flTime += Time.deltaTime;
        if (mTxtTime != null)
        {
            mTxtTime.text = flTime.ToString("00.##", CultureInfo.InvariantCulture) + " s";
        }
        else
        {
            Debug.Log("mTxtTime is null!");
        }
    }
    // End game

    private void ShowResultPopup()
    {
        endgamePopupController.Open();
        endgamePopupController.SetScore(flTime);
        endgamePopupController.SetTittle(flBest < flTime);
    }


    //** Public methods

    public void StartGame()
    {
        isCalledActiveEnemy = false;
        GameData.Instance.canPlay = true;

        // Active Enemys
        foreach (GameObject enemy in arrEnemies)
        {
            enemy.GetComponent<EnemyController>().StartGame();
        }
        mPlayer.GetComponent<PlayerController>().StartGame();
        // Show Item first time
        ShowItemDelay();

        BGMusicEnterGame.GetComponent<AudioController>().StopFadeOut();
        BGMusicTime.GetComponent<AudioController>().PlayFadeIn();

        if (isTutorialTextOn)
        {
            StartCoroutine(IEHideTutorialText());
        }
    }

    public void ShowItemDelay()
    {
        Invoke("InvokeShowItem", DelayTimeItem);
    }

    public void EndGame()
    {
        AndroidGoogleAnalytics.instance.SendTiming("Time Record", (long)(flTime * 1000));
        playCount++;
        if (playCount % GameConst.CountDieTimeToShowAds == 0 && playCount > 0)
        {
            AdmobController.Current.ShowInterstitial();
        }

        GameData.Instance.isEndGame = true;
        GameData.Instance.canPlay = false;
        // Reset Enemy position
        foreach (GameObject enemy in arrEnemies)
        {
            enemy.GetComponent<EnemyController>().EndGame();
        }

        // Reset Player values
        mPlayer.GetComponent<PlayerController>().StartGame();


        // Reset PlayerControll values
        mPlayerTouch.GetComponent<PlayerTouchController>().EndGame();

        // Reset Music
        flagChangedMusic = false;
        bestMusicPlaying = false;
        if (GameData.Instance.Music)
        {
            BGMusicEnterGame.GetComponent<AudioController>().PlayFadeIn();
            BGMusicBest.GetComponent<AudioController>().StopFadeOut();
        }

        // Set PlayButton is ResumButton
        if (isFirstTime == false)
        {
            isFirstTime = true;
            mGobjPlayButton.GetComponent<Image>().sprite = sprResumeButton;
        }

        ShowResultPopup();

        // Update Best
        if (flBest < flTime)
        {
            if (playCount > 2)
            {
                AdmobController.Current.ShowInterstitial();
            }
            flBest = flTime;// flTime la diem cua ng choi
            PlayerPrefs.SetFloat(dataTxtBestTime, flBest);
            mTxtBest.text = flBest.ToString("00.##", CultureInfo.InvariantCulture) + " s";
        }

        StopIEHideTutorialText();
    }

    public float GetBestScore()
    {
        return PlayerPrefs.GetFloat(dataTxtBestTime, 0.0f);
    }

    public void ResetTime()
    {
        flTime = 0.0f;
        mTxtTime.text = "00.00 s";
    }
    public void PlayButtonOnclick()
    {
        //Ngừng gọi quảng cáo full
        StopCoroutine("IEShowInterstitial");
        ResetTime();
        if (IsPlayTutorial)
        {
            // Hiện Tutorial
            ShowHandPointButton();
        }
        else
        {
            // gọi đếm ngược
            StartCountDownToStartGame();
        }
        // Đặt lại giá trị cờ Kết thúc game
        GameData.Instance.isEndGame = false;
        GameData.Instance.isStartGame = true;
        GameData.Instance.canPlay = false;
        if (playCount < 3)
        {
            ShowTutorialText();
        }
        AndroidGoogleAnalytics.instance.SendEvent
            (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Play", KeyAnalytics.LABEL_CLICK);
    }

    public void RateButtonOnclick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rocket.game.blame.collide.avoidance");
        AndroidGoogleAnalytics.instance.SendEvent
                (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Rate", KeyAnalytics.LABEL_CLICK);
    }

    public void PlayerDie()
    {
        mPlayer.GetComponent<PlayerController>().Die();
    }

    public void WhenPlayerDie()
    {
        //* Enemies
        foreach (GameObject enemy in arrEnemies)
        {
            enemy.GetComponent<EnemyController>().Stop();
        }

        //* Item
        CancelInvoke("InvokeShowItem");
        if (GameData.Instance.Vibrate)
        {
            Handheld.Vibrate();
        }
    }

    public void StartCountDownToStartGame()
    {
        TxtCountDown.GetComponent<Text>().enabled = true;
        TxtCountDown.GetComponent<CountDownController>().StartGame();
    }

    public void ShowHandPointButton()
    {
        handPointButton.transform.position = mPlayerTouch.transform.position;
        handPointButton.gameObject.SetActive(true);
    }

    public void HideHandPointButton()
    {
        handPointButton.gameObject.SetActive(false);
    }

    public void ShowTutorialText()
    {
        txtTutorial.DOKill();
        txtTutorial.DOFade(1, 0.5f);
        isTutorialTextOn = true;
    }

    private IEnumerator IEHideTutorialText()
    {
        yield return new WaitForSeconds(3.0f);
        txtTutorial.DOKill();
        txtTutorial.DOFade(0, 0.5f);
        isTutorialTextOn = false;
    }

    private void StopIEHideTutorialText()
    {
        StopCoroutine("IEHideTutorialText");
    }


}
