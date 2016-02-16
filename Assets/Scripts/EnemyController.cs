using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    public void Init()
    {
        transform.position = startPos;
        transform.localScale = startScale;
    }

    public void StartGame()
    {
        var speed = 34.5f;
        switch (No)
        {
            case 1:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(speed + Random.Range(10.0f, 30.0f), -speed - Random.Range(10.0f, 30.0f))); //Tác dụng 1 lực vào vật theo phương lần lượt x,y
                break;
            case 2:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed - Random.Range(10.0f, 30.0f), -speed - Random.Range(10.0f, 30.0f)));
                break;
            case 3:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(speed + Random.Range(10.0f, 30.0f), speed + Random.Range(10.0f, 30.0f)));
                break;
            case 4:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed - Random.Range(10.0f, 30.0f), speed + Random.Range(10.0f, 30.0f)));
                break;
            default:
                break;
        }
    }

    public void EndGame()
    {
        Init();
    }

    public void Stop()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void Update()
    {
        // Scale tăng dần theo thời gian
        if (GameData.Instance.canPlay)
        {
            if (transform.localScale.x < 1.5f)
            {
                transform.localScale += new Vector3(0.0003f, 0.0003f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        OnCollisionLimitPlay(coll);
        //OnCollisionPlayer(coll);
    }

    private void OnCollisionLimitPlay(Collision2D coll)
    {
        if (coll.gameObject.tag == "LimitPlay")
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    private void OnCollisionPlayer(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    Vector2 posLimitPlay = new Vector2(0, 1.8f);
    Vector2 posDirWithLimitPlay;

    public byte No;

    Vector2 startPos, startScale;
}
