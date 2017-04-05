using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private GameMaster gameMaster;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}

	void OnCollisionEnter2D(Collision2D target){
		// When player collides with an enemy, projectile or asteroid, destroy the player
		if (target.gameObject.tag == "Enemy" || target.gameObject.tag == "Asteroid") {
			// Destroy player
			gameMaster.KillPlayer();
			Destroy (target.gameObject);
		} 
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "EnemyProjectile") {
			gameMaster.KillPlayer ();
		}
	}

}
