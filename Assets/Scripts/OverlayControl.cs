using UnityEngine;
using System.Collections;

public class OverlayControl : MonoBehaviour {

	protected Sprite[] overlay;
	protected bool toggle;

	void Awake()
	{
		// load all frames in overlay array
		overlay = Resources.LoadAll<Sprite>("OverlaySpriteSheet");
	}

	// Use this for initialization
	void Start () {
		toggle = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// toggles between the two overlays every frame
		if (GetComponent<SpriteRenderer> () != null) {
			if (toggle) {
				GetComponent<SpriteRenderer> ().sprite = overlay [0];
				toggle = !toggle;
			} else {
				GetComponent<SpriteRenderer> ().sprite = overlay [1];
				toggle = !toggle;
			}
		}
	}
}
