using UnityEngine;
using System.Collections;

public class BasicWeapon : Weapon {

	public Transform destination;
	public BasicBullet projectile;
	public float speed;

	// Use this for initialization
	void Start () {
		speed = 8.0f;
		weapon_type = 0;
		maxClipSize = 1;
		reloadSpeed = 60;
		// the cap to the mortar is the reload speed, not the fire rate
		fireRate = 0;
		damagePerProjectile = 2500;
		weapon_type = 0;
	}
	
	// Update is called once per frame
	void Update () {}

	/*
	* The Fire() function should be called from player on a key or mouse pressed
	*/
	override public void Fire (Player owner){
		//spawn bullet on leftclick
		if (Input.GetMouseButtonDown(0)) {
			//create an instance of the bullet
			BasicBullet proj = (BasicBullet)Instantiate (Resources.Load<BasicBullet>("Prefabs/Bullet"), transform.position, transform.rotation);
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
