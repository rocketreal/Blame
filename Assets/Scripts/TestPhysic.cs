using UnityEngine;
using System.Collections;

public class TestPhysic : MonoBehaviour {
    public Vector3 Vel;
	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody2D>().velocity = Vel;
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody2D>().velocity = Vel;
	}
}
