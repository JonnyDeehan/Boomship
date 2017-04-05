using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour {

	public float speed = -10f;
	private float minX = -10f;
	private float maxX = 20f;
	private GameMaster gameMaster;

	private Rigidbody2D body2d;

	void Awake () {
		body2d = GetComponent<Rigidbody2D>();
		gameMaster = GameMaster.gameMaster;
	}

	// Update is called once per frame
	void Update () {
		var vel = body2d.velocity;
		body2d.velocity = new Vector2(speed, vel.y);

		var asteroidPosition = transform.position;
		if (asteroidPosition.x < minX || asteroidPosition.x > maxX) {
			Destroy (gameObject);
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
			gameMaster.ExplodeAnimation(gameObject);
			gameMaster.SpawnGem (gameObject);
			Destroy (gameObject);
		}
	}
}
