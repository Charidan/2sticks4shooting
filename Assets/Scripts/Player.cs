using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {

	// used for determining the number of players will most likely need to be removed due to Unity's handling of static variables
	protected static int num_players;

	// Initialize player attributes
	private bool weapon_initialize;
	// used for tracking the reloading state of the current weapon
	private bool reloading;
	protected int max_hp;
	protected int curr_hp;
	public int curr_ammo;
	protected int curr_ammo_weapon0;
	protected int curr_ammo_weapon1;
	protected int reload_timer;
	// used denote when a shot is loaded into the clip
	protected int reload_timer_increment;
	// fields of type "Weapon" are pointers to instances of Weapons inside class WeaponManager
	public Weapon curr_weapon;
	protected Weapon[] held_weapons;
	public Vector2 speed;
	public Vector2 movement;

	protected Color UI_color;

	protected HealthBar hit_points;
	protected Reticule gun_cursor; 

	public Sprite[] pSprites;
	SpriteRenderer sr;

	public GameObject reticule;

	void Awake()
	{
		num_players = 0;
		max_hp = 10000;
		curr_hp = 10000;
		
		reload_timer = 0;
		reload_timer_increment = 0;
		
		weapon_initialize = false; 
		
		// should be edited after creation with the appropriate player HUD color
		UI_color = new Color (0, 1, 1, 1);
		// load all frames in playersheet array
		pSprites = Resources.LoadAll<Sprite>("playersheet");
	}

	// Use this for initialization
	void Start () {
		speed = new Vector2 (10, 10);

		num_players++; 

		// creates an instance of HealthBar for the specific player and sets its color to the default UI_color
		hit_points = (HealthBar) Instantiate(Resources.Load<HealthBar>("Prefabs/HealthBar"));
		hit_points.Initialize (this);
		hit_points.setColor (UI_color);

		// creates an instance of Reticule for the specific player and sets its color to the default UI_color
		gun_cursor = (Reticule) Instantiate(Resources.Load<Reticule>("Prefabs/Reticule"));
		gun_cursor.setColor (UI_color);

		GetComponent<SpriteRenderer> ().sprite = pSprites [1];

		Debug.Log("Created new player");
		reticule = GameObject.Find("Reticule(Clone)");
	}

	// Update is called once per frame
	void Update () {
		// outside of alive check since initialization should happen regardless of player status
		if (!weapon_initialize) {
			InitializeWeapons ();
			weapon_initialize = !weapon_initialize;
		}
		// player can only do things if they are alive
		if (curr_hp > 0) {
			// Detect keys
			float inputX = Input.GetAxis("Horizontal");
			float inputY = Input.GetAxis("Vertical");

			// Calculate the movement vector
			movement = new Vector2(speed.x * inputX, speed.y * inputY);
			// to calculate the interval in which to add a bullet to curr_ammo
			if(weapon_initialize)
				reload_timer_increment = curr_weapon.getReloadSpeed () / curr_weapon.getClipSize();

			// Starts the reload process for the player only if their clip is full
			if(Input.GetButtonDown("Reload")&& curr_ammo != curr_weapon.getClipSize()){
				curr_weapon.reload();
				reloading = true; 
				reload_timer = 0;
				curr_ammo = 0;
				gun_cursor.setAmmoCount(curr_ammo);
			}

			// reloads current ammo 1 shot at a time uses the precondition increment to save space
			// reload_timer == curr_weapon.getReloadSpeed () / curr_weapon.getClipSize() usage below:
			// checks if reload_timer has 
			if (reloading && curr_ammo < curr_weapon.getClipSize() && reload_timer == reload_timer_increment) {
				gun_cursor.setAmmoCount(++curr_ammo);
				if(curr_weapon == held_weapons[0]){curr_ammo_weapon0 = curr_ammo;}
				else{curr_ammo_weapon1 = curr_ammo;}
				reload_timer = 0;
			}

			if(reloading && curr_weapon.getReloadSpeed() == curr_weapon.getReload() && curr_ammo == curr_weapon.getClipSize()){
				reloading = false;
			}
			
			// decrease ammo for semi-automatic weapons (all weapons not the Sin Wave Gun)
			if (Input.GetButtonDown("Fire") && curr_ammo != 0 && curr_weapon.canFire() && !reloading && curr_weapon.getWeaponType() != 1) {
				curr_weapon.Fire(this);
				curr_ammo--;
				gun_cursor.setAmmoCount(curr_ammo);
				if(curr_weapon == held_weapons[0]){curr_ammo_weapon0--;}
				else{curr_ammo_weapon1--;}
			}
			
			// used to allow the Sin Wave Gun to fire automatically 
			if (Input.GetButton("Fire") && curr_ammo != 0 && curr_weapon.canFire() && !reloading && curr_weapon.getWeaponType () == 1) {
				curr_weapon.Fire(this);
				curr_ammo--;
				gun_cursor.setAmmoCount(curr_ammo);
				if(curr_weapon == held_weapons[0]){curr_ammo_weapon0--;}
				else{curr_ammo_weapon1--;}		
			}
			
			// Q key allows the player to switch weapons only if they aren't reloading
			if (Input.GetButtonDown("Switch Weapons")&& !reloading) {
				if(curr_weapon == held_weapons[0]){
					curr_weapon = held_weapons[1];
					curr_ammo = curr_ammo_weapon1;
					gun_cursor.setReticule(curr_weapon.getWeaponType(), curr_ammo);
				}else{
					curr_weapon = held_weapons[0];
					curr_ammo = curr_ammo_weapon0;
					gun_cursor.setReticule(curr_weapon.getWeaponType(), curr_ammo);
				}
			}
			// TEST ROOM CODE
			if (Input.GetKeyDown(KeyCode.P)) {
				Debug.Log("unexplored rooms =" + AssemblyCSharp.FloorManager.singleton.unexploredDoors);
			}
			if (Input.GetKeyDown(KeyCode.L)) {
				Debug.Log("roomCount =" + AssemblyCSharp.FloorManager.singleton.roomCount);
			}
		}
	}
	
	// Use for updates in the players physical movements
	void FixedUpdate() {
		// player can only get statuses updated if they are alive
		if (curr_hp > 0) {
			rigidbody2D.velocity = movement;
			rotatePlayer();

			if(reload_timer < reload_timer_increment && reloading){
				reload_timer++;
			}

			//Allows only partial health regeneration up to the nearest 10
			// Lets current return to 10000
			if(Mathf.RoundToInt(curr_hp) == max_hp)
				curr_hp = max_hp;
			else if (Mathf.RoundToInt (curr_hp) % 1000 != 0) 
				adj_hp (5);
		}
	}

	// Adjust hp based on integer value
	public void adj_hp(int adj) {
		curr_hp += adj;
		if(curr_hp < 0) curr_hp = 0;
		if(curr_hp > max_hp) curr_hp = max_hp;
		if(max_hp < 1) max_hp = 1;
	}
	
	// 8-directional player rotation
	void rotatePlayer(){
		Vector3 mouseScreen = Input.mousePosition;
		Vector3 reticulePos = reticule.transform.position;
		Vector3 mouse = new Vector3(reticulePos.x, reticulePos.y, -10);
		//mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		float arcTan = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90;
		//convention is counterclockwise point is <equal, clockwise is just <
		//face North
		if (arcTan < 22.5 && arcTan >= -22.5)
			//arcTan = 0;
			GetComponent<SpriteRenderer> ().sprite = pSprites [4];
		//north-east
		else if (arcTan < -22.5 && arcTan >= -67.5)
			//arcTan = -45;
			GetComponent<SpriteRenderer> ().sprite = pSprites [3];
		//east
		else if (arcTan < -67.5 && arcTan >= -112.5)
			//arcTan = -90;
			GetComponent<SpriteRenderer> ().sprite = pSprites [2];
		//south-east
		else if (arcTan < -112.5 && arcTan >= -157.5)
			//arcTan = -135;
			GetComponent<SpriteRenderer> ().sprite = pSprites [1];
		//south
		else if (arcTan < -157.5 && arcTan >= -202.5)
			//arcTan = -180;
			GetComponent<SpriteRenderer> ().sprite = pSprites [0];
		//south-west
		else if (arcTan < -202.5 && arcTan >= -247.5)
			//arcTan = -225;
			GetComponent<SpriteRenderer> ().sprite = pSprites [7];
		//west
		else if ((arcTan >= -270 && arcTan < -247.5) || (arcTan <= 90 && arcTan >= 67.5))
			//arcTan = 90;
			GetComponent<SpriteRenderer> ().sprite = pSprites [6];
		//north-west
		else if (arcTan < 67.5 && arcTan >= 22.5)
			//arcTan = 45;
			GetComponent<SpriteRenderer> ().sprite = pSprites [5];
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

		reloading = false;

		// must be initialized here to avoid incorrect passing of information
		// if this call is in update the weapons aren't properly initialized yet
		gun_cursor.Initialize (this);
	}

	// edit functions

	// setUIColor should only be called immediately after construction 
	// this function is only supposed to change the UI color (as its name implies)
	public void setUIColor(Color player_UI_color){
		UI_color = player_UI_color;
		gun_cursor.setColor (UI_color);
		hit_points.setColor (UI_color);
	}

	// the player swaps the current weapon they are holding with the weapon specified by int type
	public void swapWeapon(int new_type){
		WeaponManager tmp_mgr = GetComponent <WeaponManager>();
		if (curr_weapon == held_weapons [0]) {
			curr_weapon = held_weapons [0] = tmp_mgr.weapon_list [new_type];
			curr_ammo = curr_ammo_weapon0 = held_weapons[0].getClipSize ();
		} else {
			curr_weapon = held_weapons [1] = tmp_mgr.weapon_list [new_type];
			curr_ammo = curr_ammo_weapon1 = held_weapons[1].getClipSize ();
		}
		gun_cursor.setReticule(new_type, curr_ammo);
	}

	// accessor functions

	public int getHP(){
		return curr_hp;
	}
	
	public int getMaxHP(){
		return max_hp;
	}

	public Color getUIColor(){
		return UI_color;
	}

	public int getAmmo(){
		return curr_ammo;
	}

	public Weapon getCurrWeapon(){
		return curr_weapon;
	}

	public bool isReloading(){
		return reloading;
	}

	public static int getNumPlayers(){
		return num_players;
	}
}

