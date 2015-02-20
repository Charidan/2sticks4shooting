using UnityEngine;
using System.Collections;

public class SinBullet : Projectile {
	public float MoveSpeed = 5.0f;
	
	public float frequency = .1f;  // Speed of sine movement
	public float magnitude = 100f;   // Size of sine movement
	private Vector3 axis;
	
	private Vector3 pos;
	
	void Start () {
		pos = transform.position;
		axis = transform.right;
		
	}
	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
		//coll.gameObject.SendMessage("ApplyDamage", 10);
		Destroy (gameObject);
	}
	void Update () {
		pos += transform.up * Time.deltaTime * MoveSpeed;
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;

		//destroy the bullet when it goes off screen
		Vector2 posOnScreen = Camera.main.WorldToScreenPoint (transform.position);
		if(posOnScreen.y > Screen.height || posOnScreen.y < 0 ||
		   posOnScreen.x > Screen.width || posOnScreen.x < 0)
			Destroy (gameObject);

	}
}
