using UnityEngine;

public class GameData
{
    static GameData instance;

    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public bool isStartGame, isEndGame, canPlay, playerDied;
    public string PrefsMusic = "music";
    public string PrefsVibarate = "vibrate";
    public bool Music
    {
        get
        {
            return PlayerPrefs.GetInt(PrefsMusic,1) == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt(PrefsMusic, value ? 1 : 0);
        }
    }
    public bool Vibrate
    {
        get
        {
            return PlayerPrefs.GetInt(PrefsVibarate, 1) == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt(PrefsVibarate, value ? 1 : 0);
        }
    }

}
