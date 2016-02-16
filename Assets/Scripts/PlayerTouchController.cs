using UnityEngine;

public class PlayerTouchController : MonoBehaviour {

    void Awake()
    {
        startPos = transform.position;
    }

    public void Init() {
        transform.position = startPos;
    }

    public void EndGame() {
        Init();
    }

    Vector2 startPos;
}
