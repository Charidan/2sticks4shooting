using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float trans_x = 0.0f;
	public float trans_y = 0.0f;
	public Vector2 speed = new Vector2(5.0f, 5.0f);
	public Vector2 movement;
	public Vector3 target_pos;
	public Vector3 ai_pos;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		get_direction();
		movement = new Vector2(trans_x * speed.x, trans_y * speed.y);
	}

	void FixedUpdate() {
		rigidbody2D.velocity = movement;
	}

	// Probably not the best method for handling this
	void get_direction() {
		target_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		ai_pos = transform.position;
		if(target_pos.x > ai_pos.x) {
			trans_x = 1.0f;
		} else if(target_pos.x < ai_pos.x) {
			trans_x = -1.0f;
		} else {
			trans_x = 0.0f;
		}
		if(target_pos.y > ai_pos.y) {
			trans_y = 1.0f;
		} else if(target_pos.y < ai_pos.y) {
			trans_y = -1.0f;
		} else {
			trans_y = 0.0f;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "Player") {
			Debug.Log ("Is touching player");
		}

	}

}
