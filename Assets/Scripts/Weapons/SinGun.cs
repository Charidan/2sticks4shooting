using UnityEngine;
using System.Collections;

public class SinGun : Weapon {

	
	public Transform destination;
	public SinBullet projectile;
	public float speed;
	GameObject reticule;

	// Use this for initialization
	void Start () {
		speed = 8.0f;
		maxClipSize = 5;
		// should take 0.75 seconds to reload (to be balanced later)
		reloadSpeed = 45;
		curr_reload = 45;
		// should be able to fire about 5x a second (to be balanced later)
		fireRate = 12;
		curr_ROF = 12; 
		damagePerProjectile = 2000;
		weapon_type = 1;
		reticule = GameObject.Find("Reticule(Clone)");
	}

	// FixedUpdate used for consistency
	void FixedUpdate () {
		if (curr_reload != reloadSpeed) {curr_reload++;}
		if (curr_ROF != fireRate) {curr_ROF++;}
	}

	/*
	* The Fire() function should be called from player on a key or mouse pressed
	* The Player should check if it has enough ammo to fire
	* Fire() should check if the gun is reloading or on cooldown from the Rate of Fire variable
	*/
	override public void Fire (Player owner){
		// make bullet only when the gun isn't reloading or already fired
		if (curr_reload == reloadSpeed && fireRate == curr_ROF) {
			curr_ROF = 0;
			//create an instance of the bullet
			SinBullet proj = (SinBullet)Instantiate (Resources.Load<SinBullet>("Prefabs/SinBullet"), transform.position, transform.rotation);
			proj.setOwner(owner);
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//initialize target point & speed for bullet
			Vector3 reticulePos = new Vector3(reticule.transform.position.x,reticule.transform.position.y, -10);
			proj.Initialize (reticulePos, speed);

			Vector3 antiReticule = new Vector3(transform.position.x - (reticule.transform.position.x - transform.position.x), transform.position.y - (reticule.transform.position.y - transform.position.y), -10);
			SinBullet proj2 = (SinBullet)Instantiate (Resources.Load<SinBullet>("Prefabs/SinBullet"), transform.position, transform.rotation);
			proj2.setOwner(owner);
			proj2.Initialize (antiReticule, speed);
		}
	}
	
	// included for later
	override public void altFire (Player owner){
		
	}
}
