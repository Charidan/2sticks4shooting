using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
	
	// Projectile traits
	protected int damage;
	protected player owner;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// accessor functions
	public int getProjectileDamage(){
		return damage;
	}
	
	// set functions
	
	public void setOwner(ref player newOwner){
		owner = newOwner;
	}
}
