using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverlayControl : MonoBehaviour {

	protected Sprite[] overlay;
	protected bool toggleImage;

	void Awake()
	{
		// load all frames in overlay array
		overlay = Resources.LoadAll<Sprite>("OverlaySpriteSheet");
	}

	// Use this for initialization
	void Start () {
		toggleImage = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// toggles between the two overlays every frame
		if (toggleImage) {
			GetComponent<Image> ().sprite = overlay[0];
			toggleImage = !toggleImage;
		} else {
			GetComponent<Image> ().sprite = overlay[1];
			toggleImage = !toggleImage;
		}
	}
}
