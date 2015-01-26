using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	// Initialize player attributes
	public float max_hp = 100.0f;
	public float curr_hp = 100.0f;
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

		// Test hp
		if (Input.GetKeyUp (KeyCode.Space)) adj_hp(-1.0f);
	}

	void FixedUpdate() {
		// Do the movement
		rigidbody2D.velocity = movement;
	}

	void adj_hp(float adj) {
		curr_hp += adj;
		if(curr_hp < 0) curr_hp = 0;
		if(curr_hp > max_hp) curr_hp = max_hp;
		if(max_hp < 1) max_hp = 1;
	}
}

