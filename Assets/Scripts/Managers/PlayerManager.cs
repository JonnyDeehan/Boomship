using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

//	public Animation anim;
	public AudioSource[] audio;

	private PlayerHealth playerHealth;

	public enum PlayerState {
		Normal,
		Shadow
	}

	private GameObject player;
	private PlayerState playerState;
	private GameState gameState;
	private GameMaster gameMaster;
	private Animator playerAnimator;

	void Awake () {
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		playerState = PlayerState.Normal;
	}
	
	// Update is called once per frame
	void Update () {
		if ((player = null) && (gameMaster.GetGameState() == GameState.Gameplay)) {
			FindPlayer ();
		}

		if (player != null) {

			if (playerState == PlayerState.Normal) {
				playerAnimator.SetBool ("Shadow", false);
			}

			if (playerState == PlayerState.Shadow) {
				playerAnimator.SetBool ("Shadow", true);
			}

		}
	}

	// Shadow state for 5 seconds, revert back to normal state
	IEnumerator ShadowDuration(){
		audio [0].Play ();
		Debug.Log ("ShadowForm");
		yield return new WaitForSeconds (5f);
		Debug.Log ("NormalForm");
		playerState = PlayerState.Normal;
	}

	public void SetPlayerState(PlayerState state){
		playerState = state;
		StartCoroutine (ShadowDuration());
	}

	public PlayerState GetPlayerState(){
		return playerState;
	}

	private void FindPlayer(){
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			playerHealth = player.GetComponent<PlayerHealth> ();
			playerAnimator = player.GetComponent<Animator> ();
		}
	}
}
