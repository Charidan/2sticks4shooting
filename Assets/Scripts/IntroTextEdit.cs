using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroTextEdit : MonoBehaviour {

	private int delay_text_appearance;
	private int delay_text_fade;
	private int text_fade;

	// Use this for initialization
	void Start () {
		delay_text_appearance = 0;
		delay_text_fade = 0;
		text_fade = 540;
	}
	
	// Update is called once per frame
	void Update () {
		// lets user skip the scene
		if (Input.anyKey) {
			Application.LoadLevel("MainMenu");
		}
	}

	void FixedUpdate(){
		float old_alpha = GetComponent<Text>().color.a;
		if (delay_text_appearance < 60) {
			delay_text_appearance++;	
		} else if( old_alpha < 1.0f && delay_text_fade == 0){
			GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f, old_alpha + 0.05f);
		}

		if (old_alpha >= 1.0f && delay_text_fade < text_fade) {
			delay_text_fade++;
		} else if (old_alpha > 0.0f && delay_text_fade >= text_fade) {
			GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f, old_alpha - 0.05f);
		}

		if(old_alpha <= 0.0f && delay_text_fade >= text_fade){
			Application.LoadLevel("MainMenu");
		}
	}
}
