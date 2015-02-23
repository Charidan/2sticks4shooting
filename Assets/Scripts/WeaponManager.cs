using UnityEngine;
using System.Collections;

public class WeaponManager: MonoBehaviour {

	/*
	 * Weapon List index organization:
	 * 0 - Mortar
	 * 1 - Sin-Wave Gun
	 * 2 - Reverse Shotgun
	 * ...
	 */
	public Weapon[] weapon_list; 
	void Start() {
		// Create instances of each weapon within the weapon list here
		weapon_list = new Weapon[3];
		weapon_list [0] = (BasicWeapon)gameObject.AddComponent ("BasicWeapon");
		weapon_list [1] = (SinGun)gameObject.AddComponent ("SinGun");
		weapon_list [2] = (ReverseShotgun)gameObject.AddComponent ("ReverseShotgun");
	}
}
