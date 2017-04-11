using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game Stage
public enum Stage {
	First,
	Second,
	Third,
	Indefinite
}

public class EventManager : MonoBehaviour {

	public GameObject enemyManagerPrefab;
	public GameObject asteroidManagerPrefab;
	public GameObject itemManagerPrefab;

	private GameObject enemyManagerObj;
	private GameObject asteroidManagerObj;
	private GameObject itemManagerGameObj;
	private Stage stage;
	private float stageTime;
	private bool didStartCountDown;
	private GameMaster gameMaster;
	private AsteroidManager asteroidManager;
	private EnemyManager enemyManager;
	private ItemManager itemManager;
	private GameMode gameMode;

	void Awake(){
		
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		gameMode = gameMaster.GetGameMode ();
			
		enemyManagerObj = Instantiate (enemyManagerPrefab);
		StartCoroutine(InstantiateWithDelay (asteroidManagerObj));
		itemManagerGameObj = Instantiate (itemManagerPrefab);
	
		enemyManager = enemyManagerObj.GetComponent<EnemyManager> ();
		itemManager = itemManagerGameObj.GetComponent<ItemManager> ();

	}

	// Use this for initialization
	void Start () {
		stage = gameMode.stage;
		stageTime = 30f;
		didStartCountDown = false;
		enemyManager.UpdateSpawnDelay (gameMode.enemySpawnDelay);
	}
	
	// Update is called once per frame
	void Update () {

		var gameState = gameMaster.GetGameState ();

		if (!didStartCountDown && !gameMaster.playerDead 
			&& (gameState == GameState.Gameplay) && (stage != Stage.Indefinite)) {
			didStartCountDown = true;
			StartCoroutine(CountDownStage());
		}

		if (gameState == GameState.GameOver) {
			stage = gameMode.stage;
		}
	}

	IEnumerator CountDownStage(){
		yield return new WaitForSeconds (stageTime);
		// Change to next stage
		if (!gameMaster.playerDead) {
			UpdateStage ();
		}
	}

	IEnumerator InstantiateWithDelay(GameObject manager){
		yield return new WaitForSeconds (gameMode.asteroidSpawnDelay / 2);

		asteroidManagerObj = Instantiate (asteroidManagerPrefab);
		asteroidManager = asteroidManagerObj.GetComponent<AsteroidManager> ();
		asteroidManager.UpdateAsteroid (stage);
	}

	void UpdateStage(){
		if (stage == Stage.First) {
			stage = Stage.Second;
			// Update asteroid manager and enemy manager (and items)
			asteroidManager.UpdateAsteroid (stage);
			enemyManager.UpdateSpawnDelay (4f);
		} else if (stage == Stage.Second) {
			// Update asteroid manager and enemy manager (and items)
			stage = Stage.Third;
			asteroidManager.UpdateAsteroid (stage);
			enemyManager.UpdateSpawnDelay (3f);
		} else if (stage == Stage.Third) {
			// Update asteroid manager and enemy manager (and items)
			stage = Stage.Indefinite;
			asteroidManager.UpdateAsteroid (stage);
			enemyManager.UpdateSpawnDelay (2f);
		}
		didStartCountDown = false;
	}

	public Stage GetStage(){
		return stage;
	}

	public void SetStage(Stage resetStage){
		this.stage = resetStage;
	}

	public void DestroyManagers(){
		Destroy (enemyManagerObj);
		Destroy (asteroidManagerObj);
		Destroy (itemManagerGameObj);
		Destroy (gameObject);
	}
}
