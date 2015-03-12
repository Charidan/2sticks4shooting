using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour {

	public bool is_visible;
	void Awake(){
		Screen.showCursor = is_visible;
	}
}
