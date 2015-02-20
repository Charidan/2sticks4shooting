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
	// type corresponds to the weapon's index in the WeaponManager List
	// type also corresponds to the integer stored in the WeaponPickup
	protected int weapon_type;
	protected float fireRate;


	/* implement in children
	* Information provided by player:
	* 		Player Position, Reticule Position, Current Ammo
	* 		Any modifiers associated with using the weapon
	*
	* The Fire() function should be called from player on a key or mouse pressed
	*/
	public abstract void Fire (Player owner);

	// included for later
	public abstract void altFire (Player owner);

	// accessors
	public int getDamage(){
		return damagePerProjectile;
	}

	// type corresponds to the weapon's index in the WeaponManager List
	// type also corresponds to the integer stored in the WeaponPickup
	public int getWeaponType(){
		return weapon_type;
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
