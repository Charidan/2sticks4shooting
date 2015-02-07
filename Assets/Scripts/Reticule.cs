using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {

	private int sprite_index;
	private Sprite[][] all_reticules;
	private int curr_ammo;
	private Player owner; 

	void Awake(){

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(string owner_name){
		owner = GameObject.Find (owner_name).GetComponent<Player>();
		curr_ammo = owner.getAmmo();
	}
}
