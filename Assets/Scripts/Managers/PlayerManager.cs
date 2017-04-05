using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

//	public Animation anim;
//	public AudioSource[] audio;

	private PlayerHealth playerHealth;

	public enum PlayerState {
		Normal,
		Shadow
	}

	private GameObject player;
	private PlayerState playerState;

	void Awake () {
		FindPlayer ();
		playerState = PlayerState.Normal;
		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player = null)
			FindPlayer ();

		if (playerState.Equals(PlayerState.Shadow)) {
			// Change animation for shadow
		} else {
			// have normal animation
		}
	}

	// Shadow state for 5 seconds, revert back to normal state
	IEnumerator ShadowDuration(){
		yield return new WaitForSeconds (5f);
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
	}
}
