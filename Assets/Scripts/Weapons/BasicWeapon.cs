//@script RequireComponent(AudioSource)

using UnityEngine;
using System.Collections;

public class BasicWeapon : Weapon {

	public Transform destination;
	public BasicBullet projectile;
	public float speed;
	public GameObject reticule;

	//public AudioClip bAudio;
	
	// Use this for initialization
	void Start () {
		speed = 8.0f;
		weapon_type = 0;
		maxClipSize = 4;
		reloadSpeed = 60;
		curr_reload = 60;
		// the cap to the mortar is the reload speed, not the fire rate
		fireRate = 0;
		curr_ROF = 0; 
		damagePerProjectile = 2500;
		weapon_type = 0;
		reticule = GameObject.Find("Reticule(Clone)");
	}

	// FixedUpdate used for consistency
	void FixedUpdate () {
		if (curr_reload != reloadSpeed) {curr_reload++;}
	}

	/*
	* The Fire() function should be called from player on a key or mouse pressed
	* The Player should check if it has enough ammo to fire
	* Fire() should check if the gun is reloading or on cooldown from the Rate of Fire variable
	*/
	override public void Fire (Player owner){
		Vector3 mouseScreen = Input.mousePosition;
		Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		float arcTan = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90;

		// make bullet only when the gun isn't reloading or already fired
		if (curr_reload == reloadSpeed && fireRate == curr_ROF) {
			//create an instance of the bullet
			//BasicBullet proj = (BasicBullet)Instantiate (Resources.Load<BasicBullet>("Prefabs/Bullet"), transform.position, transform.rotation);
			BasicBullet proj = (BasicBullet)Instantiate (Resources.Load<BasicBullet>("Prefabs/Bullet"), transform.position, Quaternion.Euler(0, 0, arcTan));

			//audio.PlayOneShot(bAudio);
			audio.Play();

			//Debug.Log("POW");

			proj.setOwner(owner);
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//initialize target point & speed for bullet
			Vector3 reticulePos = new Vector3(reticule.transform.position.x,reticule.transform.position.y, -10);
			proj.Initialize (reticulePos, speed);
		}
	}
	
	// included for later
	override public void altFire (Player owner){

	}
}
