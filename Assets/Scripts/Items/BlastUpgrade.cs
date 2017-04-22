using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastUpgrade : Collectable {

	private PlayerShoot playerShoot;
	private AudioManager audioManager;

	void Awake(){
		base.Awake ();
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerShoot = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerShoot> ();
		}
	}

	override protected void OnCollect(GameObject target){
		// Upgrade player blaster
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			audioManager.PlayAudio ("BlastUpgrade");
			playerShoot.InitiateShootUpgrade ();
		}

		// Destroy the blastupgrade
		Destroy(gameObject);
	}
}
