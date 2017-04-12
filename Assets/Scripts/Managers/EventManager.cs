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
	public bool StageEntry {
		get {
			return stageEntry;
		}
	}

	private GameObject enemyManagerObj;
	private GameObject asteroidManagerObj;
	private GameObject itemManagerGameObj;
	private Stage stage;
	private float stageTime;
	private float stageEntryDuration;
	private bool didStartCountDown;
	private bool stageEntry = false;
	private GameMaster gameMaster;
	private AsteroidManager asteroidManager;
	private EnemyManager enemyManager;
	private ItemManager itemManager;
	private AudioManager audioManager;
	private GameMode gameMode;

	void Awake(){
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		gameMode = gameMaster.GetGameMode ();
		stage = gameMode.stage;
		StartManagers ();
	}

	// Use this for initialization
	void Start () {
		if (stage == Stage.First) {
			stageEntry = true;
		}
		stageTime = 30f;
		stageEntryDuration = 3f;
		didStartCountDown = false;
		enemyManager.UpdateSpawnDelay (gameMode.enemySpawnDelay);
	}
	
	// Update is called once per frame
	void Update () {

		var gameState = gameMaster.GetGameState ();

		if (!didStartCountDown && !gameMaster.playerDead
		    && (gameState == GameState.Gameplay) && (stage != Stage.Indefinite)) {
			if (stageEntry) {
				StartCoroutine (StageEntryDuration ());
			}

			if (!stageEntry){
				didStartCountDown = true;
				Debug.Log (stage);
				StartCoroutine (CountDownStage ());
			}
		}

		if (gameState == GameState.GameOver) {
			stage = gameMode.stage;
		}
	}

	void StartManagers(){
		enemyManagerObj = Instantiate (enemyManagerPrefab);
		asteroidManagerObj = Instantiate (asteroidManagerPrefab);
		asteroidManager = asteroidManagerObj.GetComponent<AsteroidManager> ();
		asteroidManager.UpdateAsteroid (stage);
		itemManagerGameObj = Instantiate (itemManagerPrefab);
		itemManager = itemManagerGameObj.GetComponent<ItemManager> ();
		itemManager.UpdateItems (stage);
		enemyManager = enemyManagerObj.GetComponent<EnemyManager> ();
		itemManager = itemManagerGameObj.GetComponent<ItemManager> ();
	}

	IEnumerator StageEntryDuration(){
		yield return new WaitForSeconds (stageEntryDuration);
		stageEntry = false;
	}

	IEnumerator CountDownStage(){
		yield return new WaitForSeconds (stageTime);
		// Change to next stage
		if (!gameMaster.playerDead) {
			UpdateStage ();
		}
	}

	void UpdateStage(){
		if (stage == Stage.First) {
			stage = Stage.Second;
			audioManager.PlayAudio ("Stage");
			stageEntry = true;
			// Update asteroid manager and enemy manager (and items)
			asteroidManager.UpdateAsteroid (stage);
			audioManager.PlayAudio ("Stage");
			enemyManager.UpdateSpawnDelay (4f);
		} else if (stage == Stage.Second) {
			// Update asteroid manager and enemy manager (and items)
			stage = Stage.Third;
			audioManager.PlayAudio ("Stage");
			stageEntry = true;
			asteroidManager.UpdateAsteroid (stage);
			enemyManager.UpdateSpawnDelay (3f);
		} else if (stage == Stage.Third) {
			// Update asteroid manager and enemy manager (and items)
			stage = Stage.Indefinite;
			audioManager.PlayAudio ("Stage");
			stageEntry = true;
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