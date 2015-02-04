using UnityEngine;
using System.Collections;

public class BasicWeapon : Weapon {

	public Transform destination;
	public BasicBullet projectile;
	public float speed;
	private float _nextShotInSeconds;
	// Use this for initialization
	void Start () {
		//_nextShotInSeconds = 0;
		speed = (float)8.0;
		fireRate = (float).25;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate() {
		//_nextShotInSeconds = fireRate;
		//shoot bullet
		Player myPlayer = gameObject.GetComponent<Player>();
		Fire (ref myPlayer);
	}

	override public void Fire (ref Player owner){
		/*
		 * The Fire() function should be called from player on a key or mouse pressed
		 * The Fire() function should only need to check if the gun can fire then spawn projectiles
		 */
		//spawn bullet on leftclick
		if (Input.GetMouseButtonDown(0)) {
			//create an instance of the bullet
			BasicBullet proj = (BasicBullet)Instantiate (projectile, transform.position, transform.rotation);
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//initialize target point & speed for bullet
			proj.Initialize (mouseLocation, speed);
		}
	}
	
	// included for later
	override public void altFire (ref Player owner){

	}
	
}
