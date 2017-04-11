using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

	private GameMaster gameMaster;
	private AsteroidManager manager;
	private Asteroid asteroid;
	private float asteroidHP;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		manager = GameObject.FindGameObjectWithTag ("AsteroidManager").GetComponent<AsteroidManager> ();
		asteroid = manager.GetCurrentAsteroid ();
	}

	void Start(){
		asteroidHP = asteroid.asteroidHP;
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "PlayerProjectile") {
			// Explode Animation
			asteroidHP--;
			if (asteroidHP < 0) {
				gameMaster.ExplodeAnimation (gameObject);
				gameMaster.SpawnGem (gameObject);
				Destroy (gameObject);
			}
		}
	}
}
