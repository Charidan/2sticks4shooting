using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {

	// Weapon list index. Random with lower probability on more valuable weapons
	int Weapon;

	// Use this for initialization
	void Start () {
		// Define type of weapon pickup
	}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter2D(Collider2D obj) {
		if(obj.gameObject.tag == "Player") {
			Debug.Log("Weapon Pickup");

			// Transfer weapon attributes to destination from here

			Destroy(gameObject);
		}
	}
}
