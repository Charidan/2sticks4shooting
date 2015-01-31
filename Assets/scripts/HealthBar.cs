using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private Sprite[] healthSprites;
	private float playerHP;
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
		transform.position = owner.transform.position;
		playerHP = owner.getHP ();

		// change the sprite only when the player's health is
		// less than half way between 2 numbers divisible by 10
		if (playerHP % 10 > 4) {
						healthSpriteIndex = Mathf.CeilToInt (playerHP / 10);
						Debug.Log (healthSpriteIndex);
				} else {
						healthSpriteIndex = Mathf.FloorToInt (playerHP / 10);
						Debug.Log (healthSpriteIndex);
				}
		GetComponent<SpriteRenderer> ().sprite = healthSprites [healthSpriteIndex];
	}
}
