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
		calc_hp = null;
		total_player_HP = 0;
		health_to_opacity = 0.0f;
	}
	
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


		health_to_opacity = (total_player_HP / Player.getNumPlayers ()) / 10000f;
		GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, Mathf.Pow((1.0f - health_to_opacity)/2, 2.0f));
	}

	void FixedUpdate () {
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
	}
}
