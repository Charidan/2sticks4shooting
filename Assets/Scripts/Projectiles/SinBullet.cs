using UnityEngine;
using System.Collections;

public class SinBullet : Projectile {
	public float MoveSpeed = 1.0f;
	
	public float frequency = 5f;  // Speed of sine movement
	public float magnitude = 5f;   // Size of sine movement
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
		if (coll.gameObject.tag == "Enemy") {
			Destroy (coll.gameObject);
		}
		if(coll.gameObject.name == "Wall(Clone)"){
			Destroy (gameObject);
		}
	}
	void Update () {
		pos += transform.up * Time.deltaTime * MoveSpeed;
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;

		Vector3 distance = transform.position - _destination;
		Vector2 posOnScreen = Camera.main.WorldToScreenPoint (transform.position);

		//destroy if it goes of screen
		if(!gameObject.renderer.isVisible){
			Destroy (gameObject);
		}

	}
}
