using UnityEngine;
using System.Collections;

public class BasicBullet : Projectile {

	private Vector3 _destination;
	private float _speed;
	private Vector3 _charposition;

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
		damage = 5;
		owner = GameObject.Find ("Player1").GetComponent<Player>();
	}
	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
			//coll.gameObject.SendMessage("ApplyDamage", 10);
		if(coll.gameObject.tag != "Player")
			Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		// moves projectile towards cursor coordinates
		//transform.position = Vector3.MoveTowards (transform.position, _destination, Time.deltaTime * _speed);
		//rigidbody2D.velocity = transform.TransformDirection(new Vector3(0,0, _speed));
		rigidbody2D.velocity = transform.up*_speed;
		//rigidbody2D.AddForce(transform.up);
		float distanceSquared = (_destination - transform.position).sqrMagnitude;
		// destroys projectile when it reaches the cursor's coordinates
		/*
		if (distanceSquared > (0.01f * 0.01f))
						return;
		Destroy (gameObject);
		*/
		Vector2 posOnScreen = Camera.main.WorldToScreenPoint (transform.position);
		if(posOnScreen.y > Screen.height || posOnScreen.y < 0 ||
		   posOnScreen.x > Screen.width || posOnScreen.x < 0)
			Destroy (gameObject);

	}
}
