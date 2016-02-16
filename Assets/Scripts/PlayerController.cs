using UnityEngine;

//[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{

    public GameObject playerControl;

    [SerializeField]
    ParticaleDieController particaleDieController;

    private Vector2 startPos;
    private bool isDie;


    void Start()
    {
        startPos = transform.position;
    }

    public void StartGame()
    {
        isDie = false;
        GameData.Instance.playerDied = isDie;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        transform.position = startPos;
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void Update()
    {
        if (false == isDie 
            && GameData.Instance.isStartGame 
            && GameData.Instance.isEndGame == false 
            && GameData.Instance.canPlay == true)
        {
            SynPos();
        }
    }

    public void Die()
    {
        isDie = true;
        GameData.Instance.playerDied = isDie;
        GameData.Instance.canPlay = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        PlayCollisionAnimation();
        MainController.Current.WhenPlayerDie();
    }

    // Va chạm
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy" || col.transform.tag == "LimitPlay")
        {
            if (GameData.Instance.Music)
            {
                PlayCollisionSound();
            }
            Die();
        }
    }

    void PlayCollisionAnimation()
    {
        particaleDieController.Play();
    }

    void PlayCollisionSound()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    [ContextMenu("SynPos")]
    public void SynPos()
    {
        if (playerControl != null)
        {
            transform.position = 
                new Vector2(playerControl.transform.position.x, playerControl.transform.position.y + 4.5f);
        }
    }

}