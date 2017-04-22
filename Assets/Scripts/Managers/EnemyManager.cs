using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy {
	public GameObject enemy;
	public float spawnDelay;
	public float enemyHP;
}

public class EnemyManager : MonoBehaviour {

	public Enemy[] enemies;
	private Enemy currentEnemy;
	public float[] spawnPositions;
	private float spawnDelay = 4f;
	private bool canSpawn = true;
	private EventManager eventManager;

	void Awake(){
		eventManager = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager> ();
	}

	void Start(){
		if (eventManager.GetStage() == Stage.First) {
			currentEnemy = enemies [0];
		} else if (eventManager.GetStage() == Stage.Indefinite) {
			currentEnemy = enemies [1];
		}
	}

	public void UpdateEnemy(Stage stage){
		if (stage == Stage.First) {
			currentEnemy = enemies [0];
		} else if (stage == Stage.Second) {
			currentEnemy = enemies [1];
		} else if (stage == Stage.Third) {
			currentEnemy = enemies [2];
		} else if (stage == Stage.Indefinite) {
			currentEnemy = enemies [2];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawn && !eventManager.StageEntry) {
			var spawnPosition = new Vector2(10f,spawnPositions[Random.Range(min:0, max:5)]);
			StartCoroutine (SpawnEnemy (spawnPosition));
			canSpawn = false;
		}
	}

	IEnumerator SpawnEnemy(Vector2 position){
		// Spawn Enemy
		var enemyClone = Instantiate (currentEnemy.enemy,position,Quaternion.identity);
		enemyClone.transform.localScale = transform.localScale;
		yield return new WaitForSeconds (currentEnemy.spawnDelay);
		canSpawn = true;
	}

	public Enemy GetCurrentEnemy(){
		return currentEnemy;
	}
}