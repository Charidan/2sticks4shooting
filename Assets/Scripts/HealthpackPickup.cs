using UnityEngine;
using System.Collections;

public class HealthpackPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if(obj.gameObject.tag == "Player") {
			Debug.Log("Healthpack Pickup");
			
			// Transfer weapon attributes to destination from here
			Player myPlayer = obj.gameObject.GetComponent<Player> (); 
			myPlayer.adj_hp(1000);
			Destroy(gameObject);
		}
	}
}
