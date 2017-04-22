using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	private float health;
	private bool immunity = false;
	private PlayerFlash flash;
	public float maxHealth = 100;
	private int playerCollisionLayer = 8;
	public int[] collisionLayers;

	void Awake(){
		health = maxHealth;
		flash = GetComponent<PlayerFlash> ();
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
		}
	}

	public bool IsPlayerImmune(){
		return immunity;
	}

	public void Immunity(){
		StartCoroutine(ImmuneForDuration());
	}

	IEnumerator ImmuneForDuration(){
		immunity = true;
		foreach (int layer in collisionLayers) {
			Physics2D.IgnoreLayerCollision (playerCollisionLayer, layer, true);
		}
		StartCoroutine (GetComponent<PlayerFlash> ().Flash ());
		yield return new WaitForSeconds (1.5f);
		immunity = false;
		foreach (int layer in collisionLayers) {
			Physics2D.IgnoreLayerCollision (playerCollisionLayer, layer, false);
		}
	}

	public void RestoreHealth(float restoreAmount){
		if (health + restoreAmount > maxHealth)
			health = maxHealth;
		else
			health += restoreAmount;
	}
}