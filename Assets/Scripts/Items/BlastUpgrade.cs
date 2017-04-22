using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastUpgrade : Collectable {

	private PlayerShoot playerShoot;

	void Awake(){
		base.Awake ();
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerShoot = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerShoot> ();
		}
	}

	override protected void OnCollect(GameObject target){
		// Upgrade player blaster
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerShoot.InitiateShootUpgrade ();
		}

		// Destroy the blastupgrade
		Destroy(gameObject);
	}
}
