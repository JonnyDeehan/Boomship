using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	public Sprite[] healthSprites;

	private int enemyLayer = 12;
	private int asteroidLayer = 13;
	private GameMaster gameMaster;
	private float enemyHP;
	private AudioManager audioManager;
	private EnemyManager manager;
	private Enemy enemy;
	private SpriteRenderer healthSprite;

	void Awake(){
		Physics2D.IgnoreLayerCollision (enemyLayer, asteroidLayer);
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager>();
		manager = GameObject.FindGameObjectWithTag ("EnemyManager").GetComponent<EnemyManager> ();
		enemy = manager.GetCurrentEnemy ();
		healthSprite = transform.FindChild ("HP").GetComponent<SpriteRenderer> ();
	}

	void Start(){
		enemyHP = enemy.enemyHP;
	}

	void Update(){
		if (enemyHP == 3) {
			healthSprite.sprite = healthSprites [3];
		} else if (enemyHP == 2) {
			healthSprite.sprite = healthSprites [2];
		} else if (enemyHP == 1) {
			healthSprite.sprite = healthSprites [1];
		} else if (enemyHP == 0) {
			healthSprite.sprite = healthSprites [0];
		}
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
