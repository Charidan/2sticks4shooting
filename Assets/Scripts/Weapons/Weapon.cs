using UnityEngine;
using System.Collections;

/*
 * The blueprint for weapons implementation. 
 * Need to implement Start() and Fire() in children
 */

public abstract class Weapon: MonoBehaviour {
	// Weapon Traits
	// implement in children
	protected int damagePerProjectile;
	protected int maxClipSize;
	// reloadSpeed should be evenly divisible by maxClipSize
	protected int reloadSpeed;
	protected int curr_reload;
	// type corresponds to the weapon's index in the WeaponManager List
	// type also corresponds to the integer stored in the WeaponPickup
	protected int weapon_type;
	protected int fireRate;
	protected int curr_ROF;

	void Start(){

	}

	/* implement in children
	* Information provided by player:
	* 		Player Position, Reticule Position, Current Ammo
	* 		Any modifiers associated with using the weapon
	*
	* The Fire() function should be called from player on a key or mouse pressed
	* The Player should check if it has enough ammo to fire
	* Fire() should check if the gun is reloading or on cooldown from the Rate of Fire variable
	*/
	public abstract void Fire (Player owner);

	// included for later
	public abstract void altFire (Player owner);

	public void reload(){
		curr_reload = 0;
	}

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

	public int getReload(){
		return curr_reload;
	}

	public int getFireRate(){
		return fireRate;
	}

	public bool canFire(){
		return (fireRate == curr_ROF);
	}
}
