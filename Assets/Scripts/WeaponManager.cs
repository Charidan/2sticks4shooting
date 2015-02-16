using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	/*
	 * Weapon List index organization:
	 * 0 - Mortar
	 * 1 - Sin-Wave Gun
	 * 2 - Reverse Shotgun
	 * ...
	 */
	public Weapon[] weapon_list;

	void Awake() {
		// Create instances of each weapon within the weapon list here
		weapon_list = new Weapon[2];
		//weapon_list[0] = gameObject.AddComponent("BasicWeapon") as BasicWeapon;
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
