using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

	public float shootDelay; // Shooting delay
	public GameObject projectilePrefab; // Reference to projectile prefab
	public Color debugColor = Color.yellow;
	public float debugRadius = 3f; 
//	public AudioSource source; // Fire projectile sound effect

	private float timeElapsed = 0f; // Elapsed time since shot
	public Transform ProjectileFirePoint;
	private Vector2 projectilePosition;

	// Update is called once per frame
	void Update () {
		if (projectilePrefab != null) {
			var canFire = Input.GetAxisRaw ("Fire1");


			if ((canFire > 0) && (timeElapsed > shootDelay)) {
				CreateProjectile();
//				source.Play ();
				timeElapsed = 0;
			}

			timeElapsed += Time.deltaTime;
		}
	}

	// Instantiate the projectile prefab at the calculated fire position
	public void CreateProjectile(){
		projectilePosition = ProjectileFirePoint.position;
		var clone = Instantiate (projectilePrefab, projectilePosition, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
	}

	// Gizmos for visual debugging 
	void OnDrawGizmos(){

		Gizmos.color = debugColor;

		var pos = projectilePosition;

		Gizmos.DrawWireSphere (pos, debugRadius);
	}
}
