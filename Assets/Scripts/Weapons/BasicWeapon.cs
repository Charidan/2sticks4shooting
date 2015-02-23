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
		curr_reload = 60;
		// the cap to the mortar is the reload speed, not the fire rate
		fireRate = 0;
		curr_ROF = 0; 
		damagePerProjectile = 2500;
		weapon_type = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (curr_reload != reloadSpeed) {curr_reload++;}
	}

	/*
	* The Fire() function should be called from player on a key or mouse pressed
	* The Player should check if it has enough ammo to fire
	* Fire() should check if the gun is reloading or on cooldown from the Rate of Fire variable
	*/
	override public void Fire (Player owner){
		// make bullet only when the gun isn't reloading or already fired
		if (curr_reload == reloadSpeed && fireRate == curr_ROF) {
			curr_reload = 0; 
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
