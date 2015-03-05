using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {

	// weapon type available on pickup
	// corresponds to array index in WeaponManager
	int weapon_type;

	// Use this for initialization
	void Start () {
		weapon_type = 1;
	}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter2D(Collider2D obj) {
		if(obj.gameObject.tag == "Player") {
			Debug.Log("Weapon Pickup");

			// Transfer weapon attributes to destination from here
			Player destination = obj.gameObject.GetComponent<Player> (); 

			destination.swapWeapon(weapon_type);

			Destroy(gameObject);
		}
	}
}
