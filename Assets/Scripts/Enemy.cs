using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	protected GameObject player;
	protected float speed = 4.0f;
	protected float max_distance = 10.0f;
	protected float min_distance = 5.0f;

	protected Player player_ref;

	public Sprite[] pSprites;
	SpriteRenderer sr;

	void Awake()
	{
		// load all frames in fruitsSprites array
		pSprites = Resources.LoadAll<Sprite>("enemysheet1");
	}


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		GetComponent<SpriteRenderer> ().sprite = pSprites [1];

		player_ref = (Player)player.GetComponent("Player");
	}
	
	// Update is called once per frame
	void Update () {}

	void FixedUpdate() {
		move_towards_player();

		if(player != null && (Vector3.Distance(player.transform.position, transform.position) < 1.0f)) {
			player_ref.adj_hp(-50);
		}
	}

	void move_towards_player() {
		Vector3 diff = (player.transform.position - transform.position);
		diff.Normalize();
		float rotate_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		if(Vector3.Distance(transform.position, player.transform.position) <= min_distance) {
			//transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotate_z - 90.0f);
			rotateEnemy();
			if(diff.x > 0) diff.x = 1.0f;
			if(diff.y > 0) diff.y = 1.0f;
			transform.position += diff * speed * Time.deltaTime;
		}
		Debug.DrawRay(transform.position, diff, Color.red);
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "Player") {
			Debug.Log ("Is touching player");
		}

	}

	void rotateEnemy(){
		float arcTan = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
		//convention is counterclockwise point is <equal, clockwise is just <
		//face North
		if (arcTan < 22.5 && arcTan >= -22.5)
			//arcTan = 0;
			GetComponent<SpriteRenderer> ().sprite = pSprites [2];
		//north-east
		else if (arcTan < -22.5 && arcTan >= -67.5)
			//arcTan = -45;
			GetComponent<SpriteRenderer> ().sprite = pSprites [0];
		//east
		else if (arcTan < -67.5 && arcTan >= -112.5)
			//arcTan = -90;
			GetComponent<SpriteRenderer> ().sprite = pSprites [5];
		//south-east
		else if (arcTan < -112.5 && arcTan >= -157.5)
			//arcTan = -135;
			GetComponent<SpriteRenderer> ().sprite = pSprites [7];
		//south
		else if (arcTan < -157.5 && arcTan >= -202.5)
			//arcTan = -180;
			GetComponent<SpriteRenderer> ().sprite = pSprites [6];
		//south-west
		else if (arcTan < -202.5 && arcTan >= -247.5)
			//arcTan = -225;
			GetComponent<SpriteRenderer> ().sprite = pSprites [4];
		//west
		else if ((arcTan >= -270 && arcTan < -247.5) || (arcTan <= 90 && arcTan >= 67.5))
			//arcTan = 90;
			GetComponent<SpriteRenderer> ().sprite = pSprites [3];
		//north-west
		else if (arcTan < 67.5 && arcTan >= 22.5)
			//arcTan = 45;
			GetComponent<SpriteRenderer> ().sprite = pSprites [1];
		
		//old code
		//rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, arcTan);
		//Debug.Log (Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
	}

}
