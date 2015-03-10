using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	// changes the scene to the scene of the name of the argument
	// pre: the name corresponds to a valid scene
	public void switchScene (string newSceneName) {
		Application.LoadLevel (newSceneName);
	}
}
