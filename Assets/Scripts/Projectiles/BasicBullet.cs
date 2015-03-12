using UnityEngine;
using System.Collections;

public class BasicBullet : Projectile {

	private Vector3 _destination;
	private float _speed;



	public void Initialize(Vector3 destination, float speed){
		_destination = destination;
		_speed = speed;
		float arcTan = Mathf.Atan2(_destination.y - transform.position.y, _destination.x - transform.position.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler(0, 0, arcTan);
	}
	// Use this for initialization
	void Start () {
		//Vector3 mouseScreen = Input.mousePosition;
		//Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		//_destination = mouse;
		// Projectile traits
		//set damage value
		damage = 2500;

	}
	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
			//coll.gameObject.SendMessage("ApplyDamage", 10);
		if (coll.gameObject.tag == "Enemy") {
			Destroy (coll.gameObject);
		}
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		// moves projectile towards cursor coordinates
		rigidbody2D.velocity = transform.up*_speed;

		//distance between bullet and destination
		Vector3 distance = transform.position - _destination;
		Vector2 posOnScreen = _destination;
		//destroy bullet if it reaches destinatio
		if(distance.sqrMagnitude < 100.01f)//not sure why but this is a good number, tested with debug.log to find a good number
			Destroy (gameObject);
		//destroy bullet if it goes off screen
		else if(!gameObject.renderer.isVisible){
			Destroy (gameObject);
		}
		/*
		else if(posOnScreen.y > Screen.height || posOnScreen.y < 0 ||
		        posOnScreen.x > Screen.width || posOnScreen.x < 0)
			Destroy (gameObject);
		*/
	}
}
