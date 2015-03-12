using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	void Update(){
		if (Input.GetKey ("escape")) {Application.LoadLevel ("MainMenu");}
	}

	// changes the scene to the scene of the name of the argument
	// pre: the name corresponds to a valid scene
	public void switchScene (string newSceneName) {
		Application.LoadLevel (newSceneName);
	}
}
