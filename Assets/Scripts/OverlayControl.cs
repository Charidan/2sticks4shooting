using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverlayControl : MonoBehaviour {

	protected Sprite[] overlay;
	// uses GameObject as type due to FindGameObjectsWithTag() only returning GameObject[]
	protected GameObject[] calc_hp;
	protected int toggleImage;
	protected int delay_counter;
	protected int total_player_HP;
	// used for keeping the signal lost graphic on screen for 90 updates after it is fully opaque before returning to the main menu
	protected int delay_death_counter;
	// used for converting total health to individual color values
	protected float health_to_opacity;

	void Awake()
	{
		// load all frames in overlay array
		overlay = Resources.LoadAll<Sprite>("OverlaySpriteSheet");
	}

	// Use this for initialization
	void Start () {
		toggleImage = 0;
		delay_counter = 0;
		delay_death_counter = 0;
		calc_hp = null;
		total_player_HP = 0;
		health_to_opacity = 0.0f;
	}

	/*
	 * Note that all instances of Player.getNumPlayers() will probably need to be changed once multiplayer is implemented
	 * This is due to static variables not being reset in Unity when a scene is loaded
	 */


	// Update is called once per frame
	void Update(){
		// if total_player_HP is not reset, the value will only increase and not be an aggregate of the total health
		total_player_HP = 0;
		// used to get references to all player objects for oppacity calculation
		calc_hp = GameObject.FindGameObjectsWithTag ("Player");
		// if there are players, add their total health each frame
		if (calc_hp != null) {
			for(int i = 0; i < Player.getNumPlayers(); i++)	{
				total_player_HP += calc_hp[i].GetComponent<Player>().getHP();
			}
		}
		// normalize health_to_opacity to a number between 1.0 and 0
		health_to_opacity = (total_player_HP / Player.getNumPlayers ()) / 10000f;
		if (health_to_opacity > 0) {
			// the Math.Pow is designed to cap the opacity at 50% of completely opaque to still allow for playability.
			GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, Mathf.Pow ((1.0f - health_to_opacity) / 2, 2.0f));
		} 
	}

	void FixedUpdate () {

		// fade effect for the overlay in FixedUpdate() for consistent timing
		if(health_to_opacity <= 0 && GetComponent<Image> ().color.a < 1.0f) {
			GetComponent<Image> ().color = new Color(1.0f, 1.0f, 1.0f, GetComponent<Image> ().color.a + 0.01f);
		}


		// in FixedUpdate() for consistency
		if (delay_counter == 4) {
			delay_counter = 0;
			if(toggleImage < 2){
				toggleImage++;
			}else{
				toggleImage = 0;
			}
		}

		// toggles between the two overlays every 5 updates
		GetComponent<Image> ().sprite = overlay[toggleImage];
		delay_counter++;

		if (GetComponent<Image> ().color.a >= 1.0f) {delay_death_counter++;}

		// after 90 updates, return to the main menu
		if (delay_death_counter > 90) {
			Application.LoadLevel("MainMenu");
		}
	}
}
