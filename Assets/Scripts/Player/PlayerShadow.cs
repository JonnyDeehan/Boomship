using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : PlayerAction {

	public bool canShadow;
	private PlayerManager playerManager;
	private AudioManager audioManager;
	private Animator playerAnimator;
	private int playerCollisionLayer = 8;
	public int[] collisionLayers;
	private int shadowMeter;

	void Awake(){
		base.Awake ();
		playerManager = GameObject.FindGameObjectWithTag ("PlayerManager").GetComponent<PlayerManager> ();
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
		playerAnimator = GetComponent<Animator> ();
		shadowMeter = 0;
		canShadow = false;
	}

	void Update () {

		if (shadowMeter >= 120) {
			canShadow = true;
		}

		var shadow = inputState.GetButtonValue (inputButtons [0]);
		var state = playerManager.GetPlayerState ();

		if (shadow && (state == PlayerState.Normal) && canShadow) {
			shadowMeter = 0;
			canShadow = false;
			playerManager.SetPlayerState (PlayerState.Shadow);

			StartCoroutine (ShadowImmunity ());
		}
	}

	IEnumerator ShadowImmunity(){
		ChangeAnimationState (1);
		audioManager.PlayAudio ("Shadow");
		foreach (int layer in collisionLayers) {
			Physics2D.IgnoreLayerCollision (playerCollisionLayer, layer, true);
		}
		yield return new WaitForSeconds (5f);

		foreach (int layer in collisionLayers) {
			Physics2D.IgnoreLayerCollision (playerCollisionLayer, layer, false);
		}
		ChangeAnimationState (0);
	}

	void ChangeAnimationState(int value){
		playerAnimator.SetInteger("AnimState", value);
	}

	public float GetShadowMeter(){
		return shadowMeter;
	}

	public void IncrementShadowMeter(int val){
		shadowMeter += val;
	}

}
