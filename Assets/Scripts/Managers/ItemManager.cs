using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	// ======= Spawn Items =======

	private float spawnDelay = 4f;
	private bool canSpawn = true;
	private GameMaster gameMaster;
	public GameObject item_type1;

	void Awake(){
		gameMaster = GameMaster.gameMaster;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawn) {
			var spawnPosition = new Vector2(10f,Random.Range (min: -4f, max: 4f));
			StartCoroutine (SpawnItem (spawnPosition));
			canSpawn = false;
		}
	}

	IEnumerator SpawnItem(Vector2 position){
		// Spawn Enemy
		var itemClone = Instantiate (item_type1,position,Quaternion.identity);
		itemClone.transform.localScale = transform.localScale;

		yield return new WaitForSeconds (spawnDelay);
		canSpawn = true;
	}
}
