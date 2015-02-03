using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {

	public float speed = 10.0f;
	public Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		/*if(target) {
			float x = move_with(transform.position.x, target.position.x, speed);
			float y = move_with(transform.position.y, target.position.y, speed);
			transform.position = new Vector3(x, y, transform.position.z);
		}*/
	}

	void FixedUpdate() {
		if(target) {
			float x = move_with(transform.position.x, target.position.x, speed);
			float y = move_with(transform.position.y, target.position.y, speed);
			transform.position = new Vector3(x, y, transform.position.z);
		}
	}

	private float move_with(float n, float target, float a) {
		if(n == target) {
			return n;
		} else {
			float dir = Mathf.Sign (target - n);
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign (target-n))? n: target;
		}
	}
}
