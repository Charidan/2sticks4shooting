using UnityEngine;
using System.Collections;

public class ReverseShotgun : Weapon {

	public Transform destination;
	public ReverseShotgunBullet projectile;
	public float speed;
	
	// Use this for initialization
	void Start () {
		speed = 8.0f;
		maxClipSize = 7;
		// should take 1.5 seconds to reload (to be balanced later)
		reloadSpeed = 90;
		curr_reload = 90;
		// should be able to fire about 2x a second (to be balanced later)
		fireRate = 30;
		curr_ROF = 30;
		damagePerProjectile = 400;
		weapon_type = 2;
	}
	
	// Update is called once per frame
	void Update () {
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
			for(int i = -2; i < 3; i++){
				ReverseShotgunBullet proj = (ReverseShotgunBullet)Instantiate (Resources.Load<ReverseShotgunBullet>("Prefabs/ReverseShotgunBullet"), transform.position, transform.rotation);
				proj.setOwner(owner);
				Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//initialize target point & speed for bullet
				proj.Initialize (mouseLocation, speed, i);
			}
		}
	}
	
	// included for later
	override public void altFire (Player owner){
		
	}
}
