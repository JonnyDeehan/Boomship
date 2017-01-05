using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public float spawnDelay; // spawn delay
	public GameObject enemyPrefab; // Reference to enemy prefab
	public Color debugColor = Color.yellow;
	public float debugRadius = 3f; 

	private float timeElapsed = 0f; // Elapsed time since shot
	private Vector2 enemyPosition;
	private PlayerInput playerInput;
	private GameMaster gameMaster;

	void Awake(){
		playerInput = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerInput> ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}

	// Update is called once per frame
	void Update () {
		if (enemyPrefab != null) {

			if (timeElapsed > spawnDelay) {
				SpawnNewEnemy();
				//				source.Play ();
				timeElapsed = 0;
			}

			timeElapsed += Time.deltaTime;
		}
	}

	// Instantiate the projectile prefab at the calculated fire position
	public void SpawnNewEnemy(){
		enemyPosition = new Vector2 (Random.Range (gameMaster.minX, gameMaster.maxX), gameMaster.maxY);
		var clone = Instantiate (enemyPrefab, enemyPosition, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
	}
}
