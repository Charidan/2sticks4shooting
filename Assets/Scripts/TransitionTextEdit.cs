using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionTextEdit : MonoBehaviour {

	private string full_text;
	private string text_to_print;
	// delays the addition of a character
	private int delay_char;
	private int substr_length;

	// Use this for initialization
	void Start () {
		delay_char = 0;
		substr_length = 1;
		text_to_print = "";
		full_text = "Initializing...\nVideo Feed...Done\nAudio Feed...Failed\nAttempting Repair...\nRepairing Audio Playback...Success\nRepairing Audio Input...Failed\nVideo Playback...Started\n";
	}

	void Update(){
		// lets user skip the scene
		if (Input.anyKey) {
			Application.LoadLevel("roomTest");
		}
	}

	void FixedUpdate () {
		if (delay_char < 3) {
			delay_char++;
		} else if (substr_length <= full_text.Length) {
			text_to_print = full_text.Substring (0, substr_length++);
			delay_char = 0;
			GetComponent<Text> ().text = text_to_print;
		} else {
			Application.LoadLevel("roomTest");
		}

	}
}
