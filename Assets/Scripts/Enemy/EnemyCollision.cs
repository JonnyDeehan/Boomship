using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	private int enemyLayer = 12;
	private int asteroidLayer = 13;
	private GameMaster gameMaster;
	private float enemyHP;
	private AudioManager audioManager;

	void Awake(){
		Physics2D.IgnoreLayerCollision (enemyLayer, asteroidLayer);
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		enemyHP = 2f;
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager>();
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "PlayerProjectile") {
			enemyHP--;
			audioManager.PlayAudio ("Hit");
			if (enemyHP < 1f) {
				OnCollision ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Player") {
			OnCollision();
		}
	}

	void OnCollision(){
		// Explode Animation
		gameMaster.ExplodeAnimation(gameObject);
		gameMaster.SpawnGem (gameObject);
		Destroy (gameObject);
	}
}
