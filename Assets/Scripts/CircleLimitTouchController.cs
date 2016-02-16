using UnityEngine;

public class CircleLimitTouchController : MonoBehaviour
{

    void OnMouseDown()
    {
        if (MainController.Current.IsPlayTutorial)
        {
            // Hiện Tutorial
            MainController.Current.HideHandPointButton();
            MainController.Current.StartCountDownToStartGame();
            MainController.Current.IsPlayTutorial = false;
        }

        MovePlayerControl();
    }

    private void MovePlayerControl()
    {
        if (GameData.Instance.isStartGame
            && GameData.Instance.isEndGame == false
            && GameData.Instance.canPlay == true)
        {
            var objMousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (player != null)
            {
                player.transform.position = objMousePos;
            }
        }
    }


    void OnMouseDrag()
    {
        MovePlayerControl();
    }

    public GameObject player;
}
