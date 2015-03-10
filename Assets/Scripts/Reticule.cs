﻿using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {
	
	// corresponds to the weapon index in WeaponManager and the int in WeaponPickup
	// weapon type corresponds the the first index of all_reticules[][]
	public int weapon_type;
	private Sprite[][] all_reticules;
	// curr_ammo corresponds to the second index of all_reticules[][]
	public int curr_ammo;
	private Player owner; 

	public Vector3 mouse;

	void Awake(){
		// all_reticules must be a jagged array, otherwise space will be wasted
		all_reticules = new Sprite[4][];
		all_reticules [0] = Resources.LoadAll<Sprite> ("reticlesheet1");
		all_reticules [1] = Resources.LoadAll<Sprite> ("reticlesheet4");
		all_reticules [2] = Resources.LoadAll<Sprite> ("reticlesheet2");
		all_reticules [3] = Resources.LoadAll<Sprite> ("reticlesheet3");
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		// only update the reticule if there is an owner or if the owner is alive
		if (owner != null && owner.getHP() > 0) {
			mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3(mouse.x, mouse.y);
			GetComponent<SpriteRenderer>().sprite = all_reticules[weapon_type][curr_ammo];
		}
	}

	// Must be called immediately after creating a Reticule, otherwise the reticule's owner and other attributes
	// will be incorrect
	// Allows the Reticule to properly receive its owner on creation
	public void Initialize(Player new_owner){
		owner = new_owner;
		GetComponent<SpriteRenderer> ().color = owner.getUIColor ();
		weapon_type = owner.getCurrWeapon ().getWeaponType();
		curr_ammo = owner.getAmmo();
	}

	// Call this only when the player switches weapons 
	public void setReticule(int new_weapon_type, int new_ammo_count){
		weapon_type = new_weapon_type;
		curr_ammo = new_ammo_count;
	}

	// call this when the player's ammo count is changed
	public void setAmmoCount(int new_ammo_count){
		curr_ammo = new_ammo_count;
	}
}
