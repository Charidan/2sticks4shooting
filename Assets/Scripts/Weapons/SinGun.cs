using UnityEngine;
using System.Collections;

public class SinGun : Weapon {

	
	public Transform destination;
	public SinBullet projectile;
	public float speed;
	private float _nextShotInSeconds;

	void Awake() {
		//_nextShotInSeconds = 0;
		speed = 8.0f;
		fireRate = 0.25f;
	}
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate() {
		//_nextShotInSeconds = fireRate;
		//shoot bullet
		Player myPlayer = gameObject.GetComponent<Player>();
		Fire (myPlayer);
	}
	
	override public void Fire (Player owner){
		/*
		 * The Fire() function should be called from player on a key or mouse pressed
		 */
		//spawn bullet on leftclick
		if (Input.GetMouseButtonDown(0)) {
			//create an instance of the bullet
			SinBullet proj = (SinBullet)Instantiate (Resources.Load<SinBullet>("Prefabs/SinBullet"), transform.position, transform.rotation);
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//initialize target point & speed for bullet
			proj.Initialize (mouseLocation, speed);
		}
	}
	
	// included for later
	override public void altFire (Player owner){
		
	}
}
