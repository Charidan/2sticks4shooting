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
	void Start () {}
		
	// Update is called once per frame
	void Update () {
		// if statement for the frames before HealthBar is actually initialized so the game doesn't crash
		// only update when the owner is alive
		if (owner != null && owner.getHP() > 0) {
			int mapHPtoIndex = 1000;
			transform.position = owner.transform.position;
			playerHP = owner.getHP ();
			
			// Only change the sprite when the player health is < 1/2 the value
			// of the ceiling of its integer value divided by mapHPtoIndex
			// Example:
			// playerHP = 9400, healthSpriteIndex = 9
			// playerHP = 9600, healthSpriteIndex = 10
			if (playerHP % mapHPtoIndex > 499) {
				healthSpriteIndex = playerHP / mapHPtoIndex + 1;
			} else {
				healthSpriteIndex = playerHP / mapHPtoIndex; 
			}
			GetComponent<SpriteRenderer> ().sprite = healthSprites [healthSpriteIndex];
		}
	}

	// Note: this function must be called immediately after a HealthBar is made, otherwise the instantiated HealthBar 
	// will not be correct
	// Allows the HealthBar to properly receive its owner instead of searching for the first player it sees
	public void Initialize(Player new_owner){
		owner = new_owner;
		playerHP = owner.getHP ();
		healthSpriteIndex = Mathf.RoundToInt (playerHP / 10);
		transform.position = owner.transform.position;
	}

	// should only be called by the owner when the owner is initializing values or when the owner's UI_color variable is being changed
	public void setColor(Color new_color){
		GetComponent<SpriteRenderer> ().color = new_color;
	}
}
