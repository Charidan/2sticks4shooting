using UnityEngine;
using System.Collections;

public class BasicBullet : Projectile {

	private Vector3 _destination;
	private float _speed = (float)1.5;

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
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, _destination, Time.deltaTime * _speed);

		float distanceSquared = (_destination - transform.position).sqrMagnitude;
		if (distanceSquared > (.01f * .01f))
						return;
		Destroy (gameObject);
	}
}
