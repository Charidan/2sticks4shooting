using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {

	private int sprite_index;
	// corresponds to the weapon index in WeaponManager and the int in WeaponPickup
	private int weapon_type;
	private Sprite[][] all_reticules;
	private int curr_ammo;
	private Player owner; 

	public Vector3 mouse;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (owner != null) {
			mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3(mouse.x, mouse.y);
		}
	}

	// Must be called immediately after creating a Reticule, otherwise the reticule's owner and other attributes
	// will be incorrect
	// Allows the Reticule to properly receive its owner on creation
	public void Initialize(string owner_name){
		owner = GameObject.Find (owner_name).GetComponent<Player>();
		// assumes the player has a weapon (currently throws an exception due to player not having a weapon)
		weapon_type = owner.getCurrWeapon ().getWeaponType();
		sprite_index = curr_ammo = owner.getAmmo();
	}
}
