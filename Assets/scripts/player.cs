using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	// Initialize player attributes
	public float max_hp = 100.0f;
	public float curr_hp = 100.0f;
	public Vector2 speed = new Vector2(10, 10);
	public Vector2 movement;

	// Use this for initialization
	void Start () {
		Debug.Log("Created new player");
	}
	
	// Update is called once per frame
	void Update () {
		// Detect keys
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// Calculate the movement vector
		movement = new Vector2(speed.x * inputX, speed.y * inputY);

		// Test hp (-5 on space press, +0.05 per update cycle)
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("Ouch, my hp is going down :(");
			adj_hp (-5.0f);
		}
		adj_hp(0.05f);
		// Test hp
		if (Input.GetKeyUp (KeyCode.Space)) adj_hp(-1.0f);
	}

	// Use for updates in the players physical movements
	void FixedUpdate() {
		rigidbody2D.velocity = movement;
		rotatePlayer();
	}

	// Adjust hp based on float value
	void adj_hp(float adj) {
		curr_hp += adj;
		if(curr_hp < 0) curr_hp = 0;
		if(curr_hp > max_hp) curr_hp = max_hp;
		if(max_hp < 1) max_hp = 1;
	}

	// 8-directional player rotation
	void rotatePlayer(){
		Vector3 mouseScreen = Input.mousePosition;
		Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		float arcTan = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90;
		//convention is counterclockwise point is <equal, clockwise is just <
		//face North
		if (arcTan < 22.5 && arcTan >= -22.5)
						arcTan = 0;
				//north-east
				else if (arcTan < -22.5 && arcTan >= -67.5)
						arcTan = -45;
				//east
				else if (arcTan < -67.5 && arcTan >= -112.5)
						arcTan = -90;
				//south-east
				else if (arcTan < -112.5 && arcTan >= -157.5)
						arcTan = -135;
				//south
				else if (arcTan < -157.5 && arcTan >= -202.5)
						arcTan = -180;
				//south-west
				else if (arcTan < -202.5 && arcTan >= -247.5)
						arcTan = -225;
				//west
				else if ((arcTan >= -270 && arcTan < -247.5) || (arcTan <= 90 && arcTan >= 67.5))
						arcTan = 90;
				//north-west
				else if (arcTan < 67.5 && arcTan >= 22.5)
						arcTan = 45;
		rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, arcTan);
		//Debug.Log (Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
	}
}

