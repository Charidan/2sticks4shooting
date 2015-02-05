using UnityEngine;
using System.Collections;

/*
 * The blueprint for weapons implementation. 
 * Need to implement Start() and Fire() in children
 */

public abstract class Weapon : MonoBehaviour {
	// Weapon Traits
	// implement in children
	protected int damagePerProjectile;
	protected int maxClipSize;
	protected int reloadSpeed;
	protected float fireRate;


	/* implement in children
	* Information provided by player:
	* 		Player Position, Reticule Position, Current Ammo
	* 		Any modifiers associated with using the weapon
	*
	* The Fire() function should be called from player on a key or mouse pressed
	* The Fire() function should only need to check if the gun can fire then spawn projectiles
	*/
	public abstract void Fire (ref Player owner);

	// included for later
	public abstract void altFire (ref Player owner);

	// accessors
	public int getDamage(){
		return damagePerProjectile;
	}

	public int getClipSize(){
		return maxClipSize;
	}

	public int getReloadSpeed(){
		return reloadSpeed;
	}

	public float getFireRate(){
		return fireRate;
	}
}
