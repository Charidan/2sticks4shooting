using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	// Initialize vectors
	public Vector2 speed = new Vector2(10, 10);
	public Vector2 movement;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		// Detect keys
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// Calculate the movement vector
		movement = new Vector2(speed.x * inputX, speed.y * inputY);
	}

	void FixedUpdate() {
		// Do the movement
		rigidbody2D.velocity = movement;
	}
}

