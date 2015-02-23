using UnityEngine;
using System.Collections;

public class SinGun : Weapon {

	
	public Transform destination;
	public SinBullet projectile;
	public float speed;

	// Use this for initialization
	void Start () {
		speed = 8.0f;
		maxClipSize = 5;
		// should take 0.75 seconds to reload (to be balanced later)
		reloadSpeed = 45;
		// should be able to fire about 5x a second (to be balanced later)
		fireRate = 12;
		damagePerProjectile = 2000;
		weapon_type = 1;
	}

	void Update (){}

	/*
	* The Fire() function should be called from player on a key or mouse pressed
	*/
	override public void Fire (Player owner){
		//spawn bullet on leftclick
		if (Input.GetMouseButtonDown(0)) {
			//create an instance of the bullet
			SinBullet proj = (SinBullet)Instantiate (Resources.Load<SinBullet>("Prefabs/SinBullet"), transform.position, transform.rotation);
			proj.setOwner(owner);
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//initialize target point & speed for bullet
			proj.Initialize (mouseLocation, speed);
		}
	}
	
	// included for later
	override public void altFire (Player owner){
		
	}
}
