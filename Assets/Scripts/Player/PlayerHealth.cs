using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	private float health;
	public bool immunity = false;
	public float maxHealth = 100;

	void Awake(){
		health = maxHealth;
	}

	public float GetHealth(){
		return health;
	}

	public void SetHealth(float value){
		health = value;
	}

	public void TakeDamage(float damage){
		if (!immunity) {
			health -= damage;
			// Start Co-routine after taking damage to make invulnerable and change the colour of the player
			StartCoroutine(ImmuneForDuration());
		}
	}

	IEnumerator ImmuneForDuration(){
		immunity = true;
		yield return new WaitForSeconds (1.5f);
		immunity = false;
	}

	public void RestoreHealth(float restoreAmount){
		if (health + restoreAmount > maxHealth)
			health = maxHealth;
		else
			health += restoreAmount;
	}
}
