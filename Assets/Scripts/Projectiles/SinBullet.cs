using UnityEngine;
using System.Collections;

public class SinBullet : Projectile {
	public float MoveSpeed = 5.0f;
	
	public float frequency = 10f;  // Speed of sine movement
	public float magnitude = 10f;   // Size of sine movement
	private Vector3 axis;

	private Vector3 _destination;
	private float _speed;
	
	private Vector3 pos;
	
	void Start () {
		pos = transform.position;
		axis = transform.right;
	}

	public void Initialize(Vector3 destination, float speed){
		_destination = destination;
		_speed = speed;
		//make the bullet angled towards the destination
		float arcTan = Mathf.Atan2(_destination.y - transform.position.y, _destination.x - transform.position.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler(0, 0, arcTan);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
		//coll.gameObject.SendMessage("ApplyDamage", 10);
		Destroy (gameObject);
	}
	void Update () {
		pos += transform.up * Time.deltaTime * MoveSpeed;
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;

		Vector3 distance = transform.position - _destination;
		Vector2 posOnScreen = Camera.main.WorldToScreenPoint (transform.position);
		//destroy bullet if it reaches destinatio
		if(distance.sqrMagnitude < 100.2f)//not sure why but this is a good number, tested with debug.log to find a good number
			Destroy (gameObject);

		//destroy bullet if it goes off screen
		else if(posOnScreen.y > Screen.height || posOnScreen.y < 0 ||
		        posOnScreen.x > Screen.width || posOnScreen.x < 0)
			Destroy (gameObject);

	}
}
