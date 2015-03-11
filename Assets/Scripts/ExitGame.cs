using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	// Update is called once per frame
	void Update(){
		if (Input.GetKey ("escape")) {Application.Quit ();}
	}

	public void QuitGame () {
		Application.Quit ();
	}
}
