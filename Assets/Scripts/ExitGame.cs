using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	void Update(){
		if (Input.GetButton ("Main Menu")) {Application.LoadLevel ("MainMenu");}
	}

	public void QuitGame () {
		Application.Quit ();
	}
}
