using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : PlayerAction {

	private PlayerManager playerManager;

	void Awake(){
		base.Awake ();
		playerManager = GameObject.FindGameObjectWithTag ("PlayerManager").GetComponent<PlayerManager> ();
	}

	void Update () {
		var shadow = inputState.GetButtonValue (inputButtons [0]);
		var state = playerManager.GetPlayerState ();

		if (shadow && (state == PlayerManager.PlayerState.Normal)) {
			playerManager.SetPlayerState (PlayerManager.PlayerState.Shadow);
			StartCoroutine (ShadowImmunity ());
		}

	}

	IEnumerator ShadowImmunity(){
		GetComponent<Collider2D> ().enabled = false;
		yield return new WaitForSeconds (5f);
		GetComponent<Collider2D> ().enabled = true;
	}
}
