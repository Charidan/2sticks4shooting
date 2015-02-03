using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {

	// Index of weapon type available on pickup
	int Weapon;

	// Use this for initialization
	void Start () {
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
