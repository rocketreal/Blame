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
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Adunion4Unity.Instance.preloadBannerAd();
        AndroidGoogleAnalytics.Instance.StartTracking();
        AndroidGoogleAnalytics.Instance.SetTrackerID("UA-71249994-4");
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
    bool isFirstTime;
    const int DelayTimeItem = 5;
    bool bestMusicPlaying;
    int playCount;
    bool isTutorialTextOn = true;
    private float timeDelayShowAds = 4;
    private float gameTime;

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
        endgamePopupController.Open();
        Adunion4Unity.Instance.showBannerAd(Adunion4Unity.BAD_POS_TOP_CENTER);
        //AdmobController.Current.StartBanner();
        AndroidGoogleAnalytics.Instance.SendView(KeyAnalytics.CATEGORY_GAMEPLAY);
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
        flBest = PlayerPrefs.GetFloat(KeyPlayerPrefs.dataTxtBestTime, 0.0f);
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

        UpdateCallShowAds();
    }

    private void UpdateCallShowAds()
    {
        if (false == GameData.Instance.isStartGame)
        {
            if (Time.time - gameTime > timeDelayShowAds)
            {
                gameTime = Time.time;
                AndroidGoogleAnalytics.Instance.SendEvent("Interstitial", "Show", "UpdateCallShowAds");
                AdmobController.Current.ShowInterstitial();
            }
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
        gameTime = Time.time + 5;
        AndroidGoogleAnalytics.Instance.SendTiming("Time Record", (long)(flTime * 1000));
        playCount++;


        GameData.Instance.isEndGame = true;
        GameData.Instance.canPlay = false;
        GameData.Instance.isStartGame = false;
        // Reset Enemy 
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
        }

        ShowResultPopup();

        // Update Best
        if (flBest < flTime)
        {
            if (playCount > 0)
            {
                AndroidGoogleAnalytics.Instance.SendEvent("Interstitial", "Show", "ShowInterstitial Normal Score");
                AdmobController.Current.ShowInterstitial();
                gameTime = Time.time + 20;
            }
            flBest = flTime;// flTime la diem cua ng choi
            PlayerPrefs.SetFloat(KeyPlayerPrefs.dataTxtBestTime, flBest);
            mTxtBest.text = flBest.ToString("00.##", CultureInfo.InvariantCulture) + " s";
        }
        if (playCount % GameConst.CountDieTimeToShowAds == 0)
        {
            AndroidGoogleAnalytics.Instance.SendEvent("Interstitial", "Show", "ShowInterstitial Best Score");
            AdmobController.Current.ShowInterstitial();
            gameTime = Time.time + 20;
        }
        StopIEHideTutorialText();

        //StartCoroutine(IEShowInterstitial());
    }

    public float GetBestScore()
    {
        return PlayerPrefs.GetFloat(KeyPlayerPrefs.dataTxtBestTime, 0.0f);
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

        foreach (GameObject enemy in arrEnemies)
        {
            enemy.GetComponent<EnemyController>().EndGame();
        }

        if (playCount < 3)
        {
            ShowTutorialText();
        }
        AndroidGoogleAnalytics.Instance.SendEvent
            (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Play", KeyAnalytics.LABEL_CLICK);
    }

    public void RateButtonOnclick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rocket.cocangua.ludo.horseracing");
        AndroidGoogleAnalytics.Instance.SendEvent
                (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Rate", KeyAnalytics.LABEL_CLICK);
    }

    public void OnClickButtonMoregame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rocket.ludo.cangua");
        AndroidGoogleAnalytics.Instance.SendEvent
                (KeyAnalytics.CATEGORY_GAMEPLAY, KeyAnalytics.ACTION_BUTTON_CLICK + "Moregame", KeyAnalytics.LABEL_CLICK);
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
