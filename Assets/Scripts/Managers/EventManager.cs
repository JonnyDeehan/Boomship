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
	private bool didStartEntry;
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

		Debug.Log ("Selected game mode is: " + gameMode);

		if (gameMode == GameMode.Standard) {
			stage = Stage.First;
		} else {
			stage = Stage.Indefinite;
		}
		StartManagers ();
	}

	// Use this for initialization
	void Start () {
		stageEntry = true;
		didStartEntry = false;
		stageTime = 30f;
		stageEntryDuration = 3f;
		didStartCountDown = false;
	}
	
	// Update is called once per frame
	void Update () {

		var gameState = gameMaster.GetGameState ();

		if (!didStartCountDown && !gameMaster.playerDead && (gameState == GameState.Gameplay)) {
			if (stageEntry && !didStartEntry) {
				didStartEntry = true;
				StartCoroutine (StageEntryDuration ());
			}
				
		}

//		if (gameState == GameState.GameOver) {
//			if (gameMode == GameMode.Standard) {
//				stage = Stage.First;
//			} else {
//				stage = Stage.Indefinite;
//			}
//		}
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
		Debug.Log (stage);
		yield return new WaitForSeconds (stageEntryDuration);
		didStartEntry = false;
		if (stage != Stage.Indefinite) {
			didStartCountDown = true;
			StartCoroutine (CountDownStage ());
		}
		stageEntry = false;
	}

	IEnumerator CountDownStage(){
		yield return new WaitForSeconds (stageTime);
		didStartCountDown = false;
		// Change to next stage
		if (!gameMaster.playerDead) {
			UpdateStage ();
		}
	}

	void StartStage(){
		audioManager.PlayAudio ("Stage");

		// Update asteroid manager and enemy manager (and items)
		asteroidManager.UpdateAsteroid (stage);
		enemyManager.UpdateEnemy (stage);
		itemManager.UpdateItems (stage);

		stageEntry = true;
	}

	void UpdateStage(){
		if (stage == Stage.First) {
			stage = Stage.Second;
		} else if (stage == Stage.Second) {
			stage = Stage.Third;
		} else if (stage == Stage.Third) {
			stage = Stage.Indefinite;
		}
		StartStage ();
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