using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

	public Sprite[] healthSprites;

	private GameMaster gameMaster;
	private AudioManager audioManager;
	private AsteroidManager manager;
	private Asteroid asteroid;
	private float asteroidHP;
	private SpriteRenderer healthSprite;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		manager = GameObject.FindGameObjectWithTag ("AsteroidManager").GetComponent<AsteroidManager> ();
		asteroid = manager.GetCurrentAsteroid ();
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager>();
		healthSprite = transform.FindChild ("HP").GetComponent<SpriteRenderer> ();
	}

	void Start(){
		asteroidHP = asteroid.asteroidHP;
	}

	void Update(){
		if (asteroidHP == 3) {
			healthSprite.sprite = healthSprites [3];
		} else if (asteroidHP == 2) {
			healthSprite.sprite = healthSprites [2];
		} else if (asteroidHP == 1) {
			healthSprite.sprite = healthSprites [1];
		} else if (asteroidHP == 0) {
			healthSprite.sprite = healthSprites [0];
		}
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
			audioManager.PlayAudio ("Hit");
			if (asteroidHP < 1f) {

				var randomItem = Random.Range (min: 0, max: 3);

				if (randomItem == 1) {
					gameMaster.SpawnGem (gameObject);
				} else if (randomItem == 2) {
					gameMaster.SpawnShootUpgrade (gameObject);
				} else if (randomItem == 3) {
					gameMaster.SpawnHP (gameObject);
				}

				gameMaster.ExplodeAnimation (gameObject);
				Destroy (gameObject);
			}
		}
	}
}