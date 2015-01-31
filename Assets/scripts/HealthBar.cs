using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private Sprite[] healthSprites;
	private int playerHP;
	private Player owner;
	private int healthSpriteIndex;
	void Awake(){
		// load each sprite into HealthBar for easy updating later
		healthSprites = Resources.LoadAll<Sprite>("HealthBarSpriteSheet");
	}
	// Use this for initialization
	void Start () {
		owner = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		playerHP = owner.getHP ();
	    healthSpriteIndex = Mathf.RoundToInt (playerHP / 10);
		transform.position = owner.transform.position;
		Debug.Log (playerHP);
	}
	
	// Update is called once per frame
	void Update () {
		int mapHPtoIndex = 1000;
		transform.position = owner.transform.position;
		playerHP = owner.getHP ();

		// Only change the sprite when the player health is < 1/2 the value
		// of the ceiling of its integer value divided by mapHPtoIndex
		// Example:
		// playerHP = 9400, healthSpriteIndex = 9
		// playerHP = 9600, healthSpriteIndex = 10
		if (playerHP % mapHPtoIndex > 499) {
			healthSpriteIndex = playerHP/mapHPtoIndex + 1;
		} else {
			healthSpriteIndex = playerHP/mapHPtoIndex; 
		}
		GetComponent<SpriteRenderer> ().sprite = healthSprites [healthSpriteIndex];
	}
}
