using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
	
	// Projectile traits
	protected int damage;
	protected Player owner;
	
	// accessor functions
	public int getDamage (){
		return damage;
	}
	
	// set functions

	public void setOwner (Player newOwner){
		owner = newOwner;
	}
}
