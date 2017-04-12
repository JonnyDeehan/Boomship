using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;
	public float[] spawnPositions;
	private float spawnDelay = 4f;
	private bool canSpawn = true;
	private EventManager eventManager;

	void Awake(){
		eventManager = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager> ();
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
		var enemyClone = Instantiate (enemy,position,Quaternion.identity);
		enemyClone.transform.localScale = transform.localScale;

		yield return new WaitForSeconds (spawnDelay);
		canSpawn = true;
	}

	public void UpdateSpawnDelay(float delay){
		this.spawnDelay = delay;
	}
}