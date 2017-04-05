using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public float speed = -10f;

	private float minX = -10f;
	private float maxX = 20f;
	private int enemyLayer = 12;
	private int asteroidLayer = 13;
	private int enemyKillScore = 2;
	private Rigidbody2D body2d;
	private GameMaster gameMaster;

	void Awake () {
		body2d = GetComponent<Rigidbody2D>();
		Physics2D.IgnoreLayerCollision (enemyLayer, asteroidLayer);
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		var vel = body2d.velocity;
		body2d.velocity = new Vector2(speed, vel.y);

		var enemyPosition = transform.position;
		if (enemyPosition.x < minX || enemyPosition.x > maxX) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "PlayerProjectile") {
			OnCollision();
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
		gameMaster.IncrementScore (enemyKillScore);
		gameMaster.SpawnGem (gameObject);
		Destroy (gameObject);
	}
}
