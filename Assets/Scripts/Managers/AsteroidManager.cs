using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asteroid {

	public GameObject asteroid;
	public float spawnDelay;

}

public class AsteroidManager : MonoBehaviour {

	public Asteroid[] asteroids;

	private Asteroid currentAsteroid;
	private bool canSpawn = true;
	private GameMaster gameMaster;

	void Awake(){
		gameMaster = GameMaster.gameMaster;
	}

	void Start(){
		currentAsteroid = asteroids [0];
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

		if (canSpawn) {
			var spawnPosition = new Vector2(10f,Random.Range (min: -4f, max: 4f));
			StartCoroutine (SpawnAsteroid (spawnPosition));
			canSpawn = false;
		}
	}

	IEnumerator SpawnAsteroid(Vector2 position){
		// Spawn Enemy
		var asteroidClone = Instantiate (currentAsteroid.asteroid,position,Quaternion.identity);
		asteroidClone.transform.localScale = transform.localScale;

		yield return new WaitForSeconds (currentAsteroid.spawnDelay);
		canSpawn = true;
	}
}