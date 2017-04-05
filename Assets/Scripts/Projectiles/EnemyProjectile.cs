using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

	public float speed = 1.0f;

	void Awake () {
		targetTag = "Player";
		base.Awake ();
		GameObject player = GameObject.FindGameObjectWithTag (targetTag);

		if (player != null) {
			Vector2 directionToPlayer = new Vector2 (speed * (player.transform.position.x - transform.position.x),
				                            speed * (player.transform.position.y - transform.position.y));
			initialVelocity = directionToPlayer;
		} else {
			Vector2 vel = new Vector2 (-speed*10, 0);
			initialVelocity = vel;
		}
	}
}
