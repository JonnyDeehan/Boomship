using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
	Normal,
	Shadow
}

public class PlayerManager : MonoBehaviour {

	private PlayerHealth playerHealth;
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
	}

	// Shadow state for 5 seconds, revert back to normal state
	IEnumerator ShadowDuration(){
		playerState = PlayerState.Shadow;
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
		}
	}

}
