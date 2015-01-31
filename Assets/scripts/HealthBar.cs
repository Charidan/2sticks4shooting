using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private Sprite[] healthSprites;
	private float playerHP;
	private Player owner;
	void Awake(){
		// load each sprite into HealthBar for easy updating later
		healthSprites = Resources.LoadAll<Sprite>("HealthBarSpriteSheet");
	}
	// Use this for initialization
	void Start () {
		//playerHP = 100;
		owner = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		playerHP = owner.getHP ();
		transform.position = owner.transform.position;
		Debug.Log (playerHP);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = owner.transform.position;
		playerHP = owner.getHP ();
		int healthSpriteIndex = Mathf.RoundToInt (playerHP / 10);
		GetComponent<SpriteRenderer> ().sprite = healthSprites [healthSpriteIndex];
	}
}
