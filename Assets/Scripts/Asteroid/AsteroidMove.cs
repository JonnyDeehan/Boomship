using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour {

	private float speed = -6f;
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

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		var asteroidPosition = transform.position;
		if (asteroidPosition.x < min.x) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
