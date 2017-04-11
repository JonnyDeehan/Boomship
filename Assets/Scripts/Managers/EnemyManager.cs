using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;
	private float spawnDelay = 4f;
	private bool canSpawn = true;
	private EventManager eventManager;

	void Awake(){
		eventManager = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawn) {
			var spawnPosition = new Vector2(10f,Random.Range (min: -4f, max: 4f));
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