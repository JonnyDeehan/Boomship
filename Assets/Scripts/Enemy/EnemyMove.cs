using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public float speed = -10f;

	private int enemyLayer = 12;
	private int asteroidLayer = 13;
	private int enemyKillScore = 20;
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

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		var enemyPosition = transform.position;
		if (enemyPosition.x < min.x) {
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
		gameMaster.SpawnGem (gameObject);
		Destroy (gameObject);
	}
}
