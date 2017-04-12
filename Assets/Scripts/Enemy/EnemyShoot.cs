using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public GameObject projectilePrefab;
	public Vector2 firePosition;

	private bool canShoot = true;
	public float shootDelay = 0.7f;
	
	// Update is called once per frame
	void Update () {
		if (canShoot) {
			StartCoroutine (Shoot ());
			canShoot = false;
		}
	}

	IEnumerator Shoot(){

		var pos = firePosition;

		// Set the firing position relative to the player transform position
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		var player = GameObject.FindGameObjectWithTag ("Player");

		if(player != null){
			var distanceToPlayer = (player.transform.position - transform.position).magnitude;
			if (distanceToPlayer > 6) {
				var projectileClone = Instantiate (projectilePrefab, transform.position, Quaternion.identity);
				Physics2D.IgnoreCollision (projectileClone.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
				projectileClone.transform.localScale = transform.localScale;
				var direction = player.transform.position - projectileClone.transform.position;

				projectileClone.GetComponent<EnemyProjectile> ().SetDirectionToPlayer (direction);
			}
		}


		yield return new WaitForSeconds (shootDelay);

		canShoot = true;

	}
}
