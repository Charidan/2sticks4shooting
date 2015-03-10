using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDTextControl : MonoBehaviour {

	// uses GameObject as type due to FindGameObjectsWithTag() only returning GameObject[]
	protected GameObject[] calc_hp;
	protected int total_player_HP;
	// used for converting total health to individual color values
	protected float health_to_opacity;
	protected float health_to_percentage;
	protected Text signal_strength;

	// Use this for initialization
	void Start () {
		total_player_HP = 0;
		health_to_opacity = 0;
		health_to_percentage = 0;
		signal_strength = GetComponent<Text>();
	}

	/*
	 * Note that all instances of Player.getNumPlayers() will probably need to be changed once multiplayer is implemented
	 * This is due to static variables not being reset in Unity when a scene is loaded
	 */

	// Update is called once per frame
	void Update () {
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
		health_to_percentage = (total_player_HP / Player.getNumPlayers ()) / 100f;

		// format the string with the correct number of digits
		if (health_to_percentage == 100) {
			signal_strength.text = "Signal Strength: " + health_to_percentage.ToString ("000.00") + "%";
		} else if (health_to_percentage < 10) {
			signal_strength.text = "Signal Strength: " + health_to_percentage.ToString ("0.00") + "%";
		} else {
			signal_strength.text = "Signal Strength: " + health_to_percentage.ToString ("00.00") + "%";
		}
	}

	void FixedUpdate(){
		// color effects in FixedUpdate() for consistent timing
		if (health_to_opacity > 0) {
			signal_strength.color = new Color (1.0f - health_to_opacity, health_to_opacity, 0.0f);
		} else {
			// the fade effect for the text
			signal_strength.color = new Color (1.0f - health_to_opacity, health_to_opacity, 0.0f, signal_strength.color.a - 0.02f);
		}
	}
}
