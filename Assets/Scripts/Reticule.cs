using UnityEngine;
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

	}

	// Use this for initialization
	void Start () {}
	
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
