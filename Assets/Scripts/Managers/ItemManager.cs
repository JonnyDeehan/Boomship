using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	// ======= Spawn Items =======

	public GameObject[] items;

	private float spawnDelay = 8f;
	private bool canSpawn = true;
	private GameMaster gameMaster;
	private EventManager eventManager;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		eventManager = GameObject.FindGameObjectWithTag ("EventManager").GetComponent<EventManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawn && !eventManager.StageEntry) {
			canSpawn = false;
			var spawnPosition = new Vector2(10f,Random.Range (min: -4f, max: 4f));
			StartCoroutine (SpawnItem (spawnPosition, items[Random.Range (min: 0, max: 3)]));
		}
	}

	public void UpdateItems(Stage stage){
		if (stage == Stage.First) {
			spawnDelay = 10f;
		} else if (stage == Stage.Second) {
			spawnDelay = 8f;
		} else if (stage == Stage.Third) {
			spawnDelay = 7f;
		} else if (stage == Stage.Indefinite) {
			spawnDelay = 6f;
		}
	}

	IEnumerator SpawnItem(Vector2 position, GameObject item){
		yield return new WaitForSeconds (5f);
		// Spawn item
		var itemClone = Instantiate (item,position,Quaternion.identity);

		yield return new WaitForSeconds (spawnDelay);
		canSpawn = true;
	}
}
