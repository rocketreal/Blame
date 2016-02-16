using System;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class ItemController : MonoBehaviour
{
    private int totalTime = 5;
    public float r = 1.4f;

    [SerializeField]
    Text timeItem;

    [SerializeField]
    GameObject itemGroup;

    [SerializeField]
    Image filledImageItem;

    [SerializeField]
    GameObject soundObj;


    float time = 0;
    bool isItemOn;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            MainController.Current.ShowItemDelay();
            soundObj.GetComponent<AudioSource>().Play();
            Hide();
        }
    }

    public int a;
    void Update()
    {
        if (GameData.Instance.isEndGame || GameData.Instance.playerDied)
        {
            Hide();
            return;
        }
        if (isItemOn)
        {
            CountTime();
            FilledImage();
        }
    }

    internal void Show()
    {
        isItemOn = true;
        gameObject.SetActive(true);
        itemGroup.SetActive(true);
        transform.position = RandomPosInCicle();
        time = totalTime;
        oneSecond = 0;
    }

    internal void Hide()
    {
        isItemOn = false;
        itemGroup.SetActive(false);
        timeItem.text = totalTime.ToString();
        gameObject.SetActive(false);
    }

    float oneSecond = 0;
    private void CountTime()
    {
        if (oneSecond >= 1f)
        {
            timeItem.text = ((int)time+1).ToString();
            PlaySound();
            oneSecond = 0;
        }
        if (time <= 0)
        {
            WhenCoutingTimeToZero();
        }
        time -=Time.deltaTime;
        oneSecond += Time.deltaTime;
    }

    private void PlaySound()
    {
        Debug.Log("Tick "+ time);
        timeItem.gameObject.GetComponent<AudioSource>().Play();
    }

    private void FilledImage()
    {
        filledImageItem.fillAmount = (time) / totalTime;
    }

    public void StartGame()
    {
    }

    public void WhenCoutingTimeToZero()
    {
        if (false == GameData.Instance.playerDied)
        {
            MainController.Current.PlayerDie();
        }
        Hide();
    }

    Vector3 RandomPosInCicle()
    {
        float x = UnityEngine.Random.Range(-1.4f,1.4f);
        a = UnityEngine.Random.Range(0, 2);
        if (a == 0)
        {
            a = -1;
        }
        float y = a * Mathf.Sqrt(r * r - x * x);
        y += 1.5f;
        return new Vector3(x, y, 0);
    }
}
