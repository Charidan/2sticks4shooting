using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private Sprite[] healthSprites;
	private int playerHP;
	private player owner;
	void Awake(){
		// load each sprite into HealthBar for easy updating later
		healthSprites = Resources.LoadAll<Sprite>("HealthBarSpriteSheet");
	}
	// Use this for initialization
	void Start () {
		playerHP = 0; 
		//owner = 
		//playerHP = owner.getHP ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHP < 10)
			playerHP++;
		else
			playerHP = 0;

		GetComponent<SpriteRenderer> ().sprite = healthSprites [playerHP];
	}
}
