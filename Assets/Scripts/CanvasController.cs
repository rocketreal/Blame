using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	CanvasController Current { get; set; }
    void Awake()
    {
        Current = this;
    }

}
