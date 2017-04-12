using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asteroid {
	public GameObject asteroid;
	public float spawnDelay;
	public float asteroidHP;
}

public class AsteroidManager : MonoBehaviour {

	public Asteroid[] asteroids;
	public float[] spawnPositions;
	private Asteroid currentAsteroid;
	private bool canSpawn = true;
	private GameMaster gameMaster;
	private EventManager eventManager;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		eventManager = GameObject.FindGameObjectWithTag ("EventManager").GetComponent<EventManager> ();
	}

	void Start(){
		if (gameMaster.CurrentStage() == Stage.First) {
			currentAsteroid = asteroids [0];
		} else if (gameMaster.CurrentStage() == Stage.Indefinite) {
			currentAsteroid = asteroids [2];
		}
	}

	public void UpdateAsteroid(Stage stage){
		if (stage == Stage.First) {
			currentAsteroid = asteroids [0];
		} else if (stage == Stage.Second) {
			currentAsteroid = asteroids [1];
		} else if (stage == Stage.Third) {
			currentAsteroid = asteroids [2];
		} else if (stage == Stage.Indefinite) {
			currentAsteroid = asteroids [3];
		}
	}

	// Update is called once per frame
	void Update () {

		if (canSpawn && !eventManager.StageEntry) {
			var spawnPosition = new Vector2(10f,spawnPositions[Random.Range(min:0, max:4)]);
			StartCoroutine (SpawnAsteroid (spawnPosition));
			canSpawn = false;
		}
	}

	IEnumerator SpawnAsteroid(Vector2 position){
		yield return new WaitForSeconds (2f);
		// Spawn Enemy
		var asteroidClone = Instantiate (currentAsteroid.asteroid,position,Quaternion.identity);
//		asteroidClone.transform.localScale = transform.localScale;

		yield return new WaitForSeconds (currentAsteroid.spawnDelay);
		canSpawn = true;
	}

	public Asteroid GetCurrentAsteroid(){
		return currentAsteroid;
	}
		
}