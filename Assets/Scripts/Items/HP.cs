using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : Collectable {

	private float destroyDelay = .3f;
	private float hpAmount = 20f;

	private PlayerHealth playerHP;

	void Awake(){
		base.Awake ();
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerHP = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		}
	}

	override protected void OnCollect(GameObject target){
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerHP.RestoreHealth (hpAmount);
		}

		Destroy(gameObject);
	}
}
