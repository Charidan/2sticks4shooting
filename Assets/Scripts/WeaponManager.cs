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
	public static Weapon[] weapon_list; 

	// Use this for initialization
	void Start () {
		// will become an initializer list when instantiable weapon classes are implemented
		weapon_list = new Weapon[2];
		weapon_list[0] = new BasicWeapon();
	}
	
	// Update is called once per frame
	void Update () {}
}
