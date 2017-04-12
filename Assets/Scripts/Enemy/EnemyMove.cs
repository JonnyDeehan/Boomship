using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public float speed = -10f;

	private int enemyKillScore = 20;
	private Rigidbody2D body2d;

	void Awake () {
		body2d = GetComponent<Rigidbody2D>();
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
}
