using UnityEngine;
using System.Collections;

public class BasicBullet : Projectile {

	private Vector3 _destination;
	private float _speed;

	public void Initialize(Vector3 destination, float speed){
		_destination = destination;
		_speed = speed;
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
			Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		// moves projectile towards cursor coordinates
		//transform.position = Vector3.MoveTowards (transform.position, _destination, Time.deltaTime * _speed);
		//rigidbody2D.velocity = transform.TransformDirection(new Vector3(0,0, _speed));
		rigidbody2D.velocity = transform.up*_speed;

		//distance between bullet and destination
		Vector3 distance = transform.position - _destination;
		Vector2 posOnScreen = Camera.main.WorldToScreenPoint (transform.position);
		//destroy bullet if it reaches destinatio
		if(distance.sqrMagnitude < 100.01f)//not sure why but this is a good number, tested with debug.log to find a good number
			Destroy (gameObject);
		//destroy bullet if it goes off screen
		else if(posOnScreen.y > Screen.height || posOnScreen.y < 0 ||
		        posOnScreen.x > Screen.width || posOnScreen.x < 0)
			Destroy (gameObject);

	}
}
