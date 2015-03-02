using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	protected Transform player;
	protected float speed = 4.0f;
	protected float max_distance = 10.0f;
	protected float min_distance = 5.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {}

	void FixedUpdate() {
		move_towards_player();
	}

	void move_towards_player() {
		Vector3 diff = (player.position - transform.position);
		diff.Normalize();
		float rotate_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		if(Vector3.Distance(transform.position, player.position) <= min_distance) {
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotate_z - 90.0f);
			if(diff.x > 0) diff.x = 1.0f;
			if(diff.y > 0) diff.y = 1.0f;
			transform.position += diff * speed * Time.deltaTime;
		}
		Debug.DrawRay(transform.position, diff, Color.red);
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "Player") {
			Debug.Log ("Is touching player");
		}

	}

}
