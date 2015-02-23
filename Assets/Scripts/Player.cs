﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// used for determining the number of players
	public static int num_players = 0;

	// Initialize player attributes
	private bool weapon_initialize;
	protected int max_hp;
	protected int curr_hp;
	public int curr_ammo;
	public int curr_ammo_weapon0;
	public int curr_ammo_weapon1;
	// fields of type "Weapon" are pointers to instances of Weapons inside class WeaponManager
	public Weapon curr_weapon;
	public Weapon[] held_weapons;
	public Vector2 speed;
	public Vector2 movement;

	protected HealthBar hit_points;
	protected Reticule gun_cursor; 

	// Use this for initialization
	void Start () {
		max_hp = 10000;
		curr_hp = 10000;

		num_players++; 

		// creates an instance of HealthBar for the specific player
		hit_points = (HealthBar) Instantiate(Resources.Load<HealthBar>("Prefabs/HealthBar"));
		hit_points.Initialize (this.name);

		weapon_initialize = false; 

		speed = new Vector2 (10, 10);

		// creates an instance of Reticule for the specific player
		gun_cursor = (Reticule) Instantiate(Resources.Load<Reticule>("Prefabs/Reticule"));
		gun_cursor.Initialize (this.name);

		Debug.Log("Created new player");
	}

	// Update is called once per frame
	void Update () {
		if (!weapon_initialize) {
			InitializeWeapons ();
			weapon_initialize = !weapon_initialize;
		}
		// Detect keys
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// Calculate the movement vector
		movement = new Vector2(speed.x * inputX, speed.y * inputY);
		
		// Allows only partial health regeneration up to the nearest 10
		// Lets current return to 10000
		if(Mathf.RoundToInt(curr_hp) == max_hp)
			curr_hp = max_hp;
		else if (Mathf.RoundToInt (curr_hp) % 1000 != 0) 
			adj_hp (5);

		// Q key allows the player to switch weapons
		if (Input.GetKeyDown (KeyCode.Q)) {
			if(curr_weapon == held_weapons[0]){
				curr_weapon = held_weapons[1];
				curr_ammo = curr_ammo_weapon1;
			}else{
				curr_weapon = held_weapons[0];
				curr_ammo = curr_ammo_weapon0;
			}
		}
		
		// Test hp (-5 on space press, +0.05 per update cycle)
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("Ouch, my hp is going down :(" + curr_hp + ")");
			adj_hp (-500);
		}
	}
	
	// Use for updates in the players physical movements
	void FixedUpdate() {
		rigidbody2D.velocity = movement;
		rotatePlayer();
	}

	// Adjust hp based on integer value
	void adj_hp(int adj) {
		curr_hp += adj;
		if(curr_hp < 0) curr_hp = 0;
		if(curr_hp > max_hp) curr_hp = max_hp;
		if(max_hp < 1) max_hp = 1;
	}
	
	// 8-directional player rotation
	void rotatePlayer(){
		Vector3 mouseScreen = Input.mousePosition;
		Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		float arcTan = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90;
		//convention is counterclockwise point is <equal, clockwise is just <
		//face North
		if (arcTan < 22.5 && arcTan >= -22.5)
			arcTan = 0;
		//north-east
		else if (arcTan < -22.5 && arcTan >= -67.5)
			arcTan = -45;
		//east
		else if (arcTan < -67.5 && arcTan >= -112.5)
			arcTan = -90;
		//south-east
		else if (arcTan < -112.5 && arcTan >= -157.5)
			arcTan = -135;
		//south
		else if (arcTan < -157.5 && arcTan >= -202.5)
			arcTan = -180;
		//south-west
		else if (arcTan < -202.5 && arcTan >= -247.5)
			arcTan = -225;
		//west
		else if ((arcTan >= -270 && arcTan < -247.5) || (arcTan <= 90 && arcTan >= 67.5))
			arcTan = 90;
		//north-west
		else if (arcTan < 67.5 && arcTan >= 22.5)
			arcTan = 45;
		rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, arcTan);
		//Debug.Log (Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
	}

	// must be called once in Update() to allow for the weapons to correctly give the following attributes their correct values:
	// curr_ammo
	// curr_ammo_weapon0
	// curr_ammo_weapon1
	private void InitializeWeapons(){
		// Initialize player's invetory to guns in weapon manager
		WeaponManager tmp_mgr = GetComponent <WeaponManager>();
		held_weapons = new Weapon[2];
		// gives the player a Mortar and Reverse Shotgun in their inventory
		curr_weapon = held_weapons [0] = tmp_mgr.weapon_list[0];
		held_weapons [1] = tmp_mgr.weapon_list [2];
		
		// initialize the current ammo for each weapon to the correct value
		curr_ammo = curr_ammo_weapon0 = held_weapons [0].getClipSize ();
		curr_ammo_weapon1 = held_weapons [1].getClipSize ();
	}

	// accessor functions
	public int getHP(){
		return curr_hp;
	}
	
	public int getMaxHP(){
		return max_hp;
	}
	
	public int getAmmo(){
		return curr_ammo;
	}

	public Weapon getCurrWeapon(){
		return curr_weapon;
	}
}

