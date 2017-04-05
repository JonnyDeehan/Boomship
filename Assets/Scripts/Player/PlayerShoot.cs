using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot: PlayerAction {

	public GameObject projectilePrefab;
	public Vector2 firePosition;
	private float shootDelay = 0.1f;
	private float lastShootTime = 0;


	void Update(){
		var shoot = inputState.GetButtonValue (inputButtons [0]);
		var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (shoot && (holdTime < shootDelay) && (Time.time - lastShootTime > shootDelay)) {
			Shoot ();
		}
	}

	private void Shoot(){
		
		lastShootTime = Time.time;
		var pos = firePosition;

		// Set the firing position relative to the player transform position
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		var projectileClone = Instantiate (projectilePrefab,pos,Quaternion.identity);
		Physics2D.IgnoreCollision(projectileClone.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		projectileClone.transform.localScale = transform.localScale;

	}
}
