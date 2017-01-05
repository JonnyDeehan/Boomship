using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public bool isPlayerDead = true;
	public GameObject playerPrefab;
	private Vector2 playerPosition;

	// Player movement clamping in relation to camera
	public float minX, minY, maxX, maxY;
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectWithTag("Player") == null && isPlayerDead) {
			
			StartCoroutine (RespawnPlayer ());
			isPlayerDead = false;
		}

		// Clamping ranges
		minX = Camera.main.transform.position.x - 8.07f;
		maxX = Camera.main.transform.position.x + 8.08f;
		minY = Camera.main.transform.position.y - 4.25f;
		maxY = Camera.main.transform.position.y + 4.26f;
	}

	IEnumerator RespawnPlayer(){
		yield return new WaitForSeconds (1f);
		playerPosition = new Vector2 (0.08f, -4.02f);
		var clone = Instantiate (playerPrefab, playerPosition, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
		isPlayerDead = true;
	}
}
