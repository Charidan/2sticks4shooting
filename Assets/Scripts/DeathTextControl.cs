using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathTextControl : MonoBehaviour {

	// uses GameObject as type due to FindGameObjectsWithTag() only returning GameObject[]
	protected GameObject[] calc_hp;
	protected int total_player_HP;
	// used for converting total health to individual color values
	protected float health_to_opacity;
	protected Text signal_lost;
	// Use this for initialization
	void Start () {
		total_player_HP = 0;
		health_to_opacity = 0;
		signal_lost = GetComponent<Text>();
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
	}

	void FixedUpdate(){
		// fade in effect in FixedUpdate() for consistent timing
		if (health_to_opacity <= 0 && signal_lost.color.a < 1.0f) {
			signal_lost.color = new Color(1.0f, 0.0f, 0.0f, signal_lost.color.a + 0.01f);
		}
	}
}
