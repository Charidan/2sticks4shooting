using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	void Update(){
		if (Input.GetKey ("escape")) {Application.LoadLevel ("MainMenu");}
	}

	public void QuitGame () {
		Application.Quit ();
	}
}
