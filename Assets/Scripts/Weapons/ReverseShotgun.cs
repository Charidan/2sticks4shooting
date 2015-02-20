using UnityEngine;
using System.Collections;

public class ReverseShotgun : Weapon {

	public Transform destination;
	public ReverseShotgunBullet projectile;
	public float speed;
	private float _nextShotInSeconds;
	Player myPlayer;
	void Awake() {
		//_nextShotInSeconds = 0;
		speed = 8.0f;
		fireRate = 0.25f;
	}
	
	// Use this for initialization
	void Start () {
		myPlayer = gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate() {
		//_nextShotInSeconds = fireRate;
		//shoot bullet
		Fire (myPlayer);
	}
	
	override public void Fire (Player owner){
		/*
		 * The Fire() function should be called from player on a key or mouse pressed
		 */
		//spawn bullet on leftclick
		if (Input.GetMouseButtonDown(0)) {
			//create an instance of the bullet
			for(int i = -2; i < 3; i++){
				ReverseShotgunBullet proj = (ReverseShotgunBullet)Instantiate (Resources.Load<ReverseShotgunBullet>("Prefabs/ReverseShotgunBullet"), transform.position, transform.rotation);
				Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//initialize target point & speed for bullet
				proj.Initialize (mouseLocation, speed, i);
			}
		}
	}
	
	// included for later
	override public void altFire (Player owner){
		
	}

}
