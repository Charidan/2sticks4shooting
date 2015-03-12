using UnityEngine;
using System.Collections;

public class ReverseShotgunBullet : Projectile {

	private Vector3 _destination;
	private float _speed;
	private Vector3 _charposition;
	
	public void Initialize(Vector3 destination, float speed, int spread){
		_destination = destination;
		_speed = speed;
		//shift the bullet over horizontally based on which bullet it is in the spread
		transform.Translate (Vector2.right * spread);
		float arcTan = Mathf.Atan2(_destination.y - transform.position.y, _destination.x - transform.position.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler(0, 0, arcTan);
	}
	// Use this for initialization
	void Start () {
		damage = 400;
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
		rigidbody2D.velocity = transform.up*_speed;
		//destroy it once it reaches the destination
		Vector3 distance = transform.position - _destination;
		Vector2 posOnScreen = Camera.main.WorldToScreenPoint (transform.position);
		//destroy bullet if it reaches destinatio
		if(distance.sqrMagnitude < 100.01f)//not sure why but this is a good number, tested with debug.log to find a good number
			Destroy (gameObject);
		//destroy bullet if it goes off screen
		else if(!gameObject.renderer.isVisible){
			Destroy (gameObject);
		}
	}

}
