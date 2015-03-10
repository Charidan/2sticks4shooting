using UnityEngine;
using System.Collections;

public class ReturnFromMenu : MonoBehaviour {
	
	// will always return to the main menu when any key or button press is detected
	void Update () {
		if (Input.anyKey) {
			Application.LoadLevel("MainMenu");	
		}
	}
}
