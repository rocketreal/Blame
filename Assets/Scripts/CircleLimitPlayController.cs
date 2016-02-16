using UnityEngine;
public class CircleLimitPlayController : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag=="Enemy") {
            if(gameObject.GetComponent<SpriteRenderer>() != null 
                && col.gameObject.GetComponent<SpriteRenderer>()!= null) {
                gameObject.GetComponent<SpriteRenderer>().color 
                    = col.gameObject.GetComponent<SpriteRenderer>().color;
            }
        }
    }
}
