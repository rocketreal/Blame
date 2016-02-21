using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    float oneSecond = 0;
    public void StartGame()
    {
        flCountDown = 3.99f;
        gameObject.GetComponent<Text>().text = (int)flCountDown + "";
        isStartCount = true;
    }

    void Update()
    {
        // Đếm ngược
        if (isStartCount)
        {
            if (flCountDown > 0)
            {
                flCountDown -= Time.deltaTime;
                oneSecond += Time.deltaTime;
                if (oneSecond >= 1.0f)
                {
                    oneSecond = 0;
                    gameObject.GetComponent<Text>().text = (int)flCountDown + "";
                    PlaySound();
                }
                if (flCountDown <= 0)
                {// Đếm đến 0
                    gameObject.GetComponent<Text>().text = "";
                    GameData.Instance.isStartGame = true;
                    isStartCount = false;
                    // Gọi hàm Start Game
                    GobjMainController.GetComponent<MainController>().StartGame();
                }
            }
        }
    }

    private void PlaySound()
    {
        audio.Play();
    }

    float flCountDown;
    bool isStartCount;

    public GameObject GobjMainController;
    public AudioSource audio;
}
