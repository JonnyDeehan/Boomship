using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public float speed = -10f;

	private int enemyKillScore = 20;
	private Rigidbody2D body2d;
	private PlayerHealth playerHealth;
	private GameObject player;

	void Awake () {
		body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (player == null) {
			FindPlayer ();
		}

		var vel = body2d.velocity;
		body2d.velocity = new Vector2(speed, vel.y);

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		var enemyPosition = transform.position;
		if (enemyPosition.x < min.x) {
			if (player != null) {
				playerHealth.TakeDamage (10f);
			}
			Destroy (gameObject);
		}
	}

	void FindPlayer(){
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent<PlayerHealth> ();
		}
	}
}
