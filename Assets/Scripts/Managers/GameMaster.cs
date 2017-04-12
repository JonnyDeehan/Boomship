using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public static GameMaster gameMaster;
	// Prefabs
	public GameObject playerPrefab;
	public GameObject explosionPrefab;
	public GameObject gemPrefab;
	public GameObject eventManagerPrefab;

	//	Managers
	private GameObject eventManagerObj;
	private EventManager eventManager;
	private AudioManager audioManager;

	// UI
	public Sprite[] livesImages;
	public Sprite[] healthBarImages;
	public Sprite[] pausePlayImages;
	public Sprite[] gameModeImages;
	public Sprite[] blastMeterImages;
	public Sprite[] stageImages;
	private Image Healthbar;
	private Image LivesImage;
	private Image pausePlayImage;
	private Image blastMeterImage;
	private Image stageTransitionImage;
	private Image titleImage;
	private Transform HealthBarObj;
	private Transform LivesObj;
	private Transform PlayButton;
	private Transform GameOver;
	private Transform ScoreObj;
	private Transform GameModeObj;
	private Transform pausePlayObj;
	private Transform MainMenuObj;
	private Transform BlastMeterObj;
	private Transform StageObj;
	private Transform TitleObj;
	private Transform ScoreTitleObj;
	private Text scoreTextUI;
	private Dropdown gameModeSelection;

	// Game attributes
	private int gemCount;
	private int score;
	private int lives;
	private Vector2 spawnPoint;
	private GameState gameState;
	private GameMode gameMode;

	// Conditions/ States
	public bool pauseGame;
	private bool commencingRespawn;
	private bool gameOver;
	public bool playerDead;
	private bool mainMenu;

	// Player related
	private GameObject player;
	private PlayerHealth playerHealth;
	private PlayerShoot playerShoot;


	void Awake(){

		// Find UI game objects and initialize
		HealthBarObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("HealthBar");
		Healthbar = HealthBarObj.GetComponent<Image> ();
		LivesObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("Lives");
		LivesImage = LivesObj.GetComponent<Image> ();
		PlayButton = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("PlayButton");
		GameOver = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("GameOver");
		ScoreObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("Score");
		scoreTextUI = ScoreObj.GetComponent<Text> ();
		GameModeObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("GameMode");
		gameModeSelection = GameModeObj.GetComponent<Dropdown> ();
		pausePlayObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("PausePlay");
		pausePlayImage = pausePlayObj.GetComponent<Image> ();
		MainMenuObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("MainMenu");
		BlastMeterObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("BlastMeter");
		blastMeterImage = BlastMeterObj.GetComponent<Image> ();
		StageObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("Stage");
		stageTransitionImage = StageObj.GetComponent<Image> ();
		TitleObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("BoomshipTitle");
		titleImage = TitleObj.GetComponent<Image> ();
		ScoreTitleObj = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("ScoreTitle");

		if (gameMaster == null) {
			gameMaster = GameObject.FindGameObjectWithTag ("GameMaster")
				.GetComponent<GameMaster> ();
		}
		if (player == null) {
			FindPlayer ();
		}

		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
	}

	// Use this for initialization
	void Start () {
		mainMenu = true;
		gemCount = 0;
		score = 0;
		playerDead = false;
		commencingRespawn = false;
		lives = 3;
		gameOver = false;
		spawnPoint = new Vector2 (-5.53f, 1.01f);
		gameState = GameState.Startup;
		pauseGame = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (GameModeObj != null) {
			var selectedMode = gameModeSelection.captionText.text;
			// Default selection is standard
			if (selectedMode == "Standard") {
				GameModeObj.GetComponent<Image> ().sprite = gameModeImages [0];
			} else if (selectedMode == "Infinite") {
				GameModeObj.GetComponent<Image> ().sprite = gameModeImages [1];
			}
		}

		if (lives == 3) {
			LivesImage.sprite = livesImages [0];
		} else if (lives == 2) {
			LivesImage.sprite = livesImages [1];
		} else if (lives == 1) {
			LivesImage.sprite = livesImages [2];
		} else if (lives == 0) {
			LivesImage.sprite = livesImages [3];
		}

		if (eventManager != null && !mainMenu) {
			if (eventManager.StageEntry) {
				StageObj.gameObject.SetActive (true);
				if (CurrentStage () == Stage.First) {
					stageTransitionImage.sprite = stageImages [0];
				} else if (CurrentStage () == Stage.Second) {
					stageTransitionImage.sprite = stageImages [1];
				} else if (CurrentStage () == Stage.Third) {
					stageTransitionImage.sprite = stageImages [2];
				} else if (CurrentStage () == Stage.Indefinite) {
					// IMPLEMENT INFINITE STAGE ICON
				}
			} else {
				StageObj.gameObject.SetActive (false);
			}
		}

		if (BlastMeterObj != null && player != null) {
			if (playerShoot.Ammo == 9f) {
				blastMeterImage.sprite = blastMeterImages [0];
			} else if (playerShoot.Ammo == 8f) {
				blastMeterImage.sprite = blastMeterImages [1];
			} else if (playerShoot.Ammo == 7f) {
				blastMeterImage.sprite = blastMeterImages [2];
			} else if (playerShoot.Ammo == 6f) {
				blastMeterImage.sprite = blastMeterImages [3];
			} else if (playerShoot.Ammo == 5f) {
				blastMeterImage.sprite = blastMeterImages [4];
			} else if (playerShoot.Ammo == 4f) {
				blastMeterImage.sprite = blastMeterImages [5];
			} else if (playerShoot.Ammo == 3f) {
				blastMeterImage.sprite = blastMeterImages [6];
			} else if (playerShoot.Ammo == 2f) {
				blastMeterImage.sprite = blastMeterImages [7];
			} else if (playerShoot.Ammo == 1f) {
				blastMeterImage.sprite = blastMeterImages [8];
			} else if (playerShoot.Ammo == 0f) {
				blastMeterImage.sprite = blastMeterImages [9];
			}
		}

		if (player != null) {
			var health = player.GetComponent<PlayerHealth> ().GetHealth ();
			if (health == 100) {
				Healthbar.sprite = healthBarImages [0];
			} else if (health == 80) {
				Healthbar.sprite = healthBarImages [1];
			} else if (health == 60) {
				Healthbar.sprite = healthBarImages [2];
			} else if (health == 40) {
				Healthbar.sprite = healthBarImages [3];
			} else if (health == 20) {
				Healthbar.sprite = healthBarImages [4];
			} else if (health <= 0) {
				Healthbar.sprite = healthBarImages [5];
			}
		}

		if (player == null && playerDead && !commencingRespawn && !gameOver) {
			commencingRespawn = true;
			StartCoroutine(RespawnPlayer ());
		}

		if (player != null) {
			if (playerHealth.GetHealth() <= 0f) {
				KillPlayer ();
			}
		}
	}

	// ======= Window Related Methods =======

	public void GameOverScreen(){
		gameState = GameState.GameOver;
		eventManager.DestroyManagers ();
		if (GameObject.FindGameObjectWithTag ("Enemy") != null) {
			Destroy (GameObject.FindGameObjectWithTag ("Enemy"));
		}
		if (GameObject.FindGameObjectWithTag ("Asteroid") != null) {
			Destroy (GameObject.FindGameObjectWithTag ("Asteroid"));
		}
		GameOver.gameObject.SetActive (true);
		PlayButton.gameObject.SetActive (true);
		pausePlayObj.gameObject.SetActive (false);
		GameModeObj.gameObject.SetActive (true);
		BlastMeterObj.gameObject.SetActive (false);
	}

	public void StartGame(){
		audioManager.PlayAudio ("Select");
		mainMenu = false;
		var selectedMode = gameModeSelection.captionText.text;
		gameMode = new GameMode();
		// Default selection is standard
		if (selectedMode == "Standard") {
			// set game mode to standard
			gameMode.init (Stage.First, 4f, 4f, 8f);
		} else if (selectedMode == "Infinite") {
			// set mode to infinite 
			GameModeObj.GetComponent<Image>().sprite = gameModeImages[1];
			gameMode.init(Stage.Indefinite, 2f, 2f, 6f);
		}
		
		// Activate/deactive necessary UI components
		TitleObj.gameObject.SetActive(false);
		PlayButton.gameObject.SetActive(false);
		HealthBarObj.gameObject.SetActive (true);
		LivesObj.gameObject.SetActive (true);
		ScoreObj.gameObject.SetActive (true);
		ScoreTitleObj.gameObject.SetActive (true);
		GameModeObj.gameObject.SetActive (false);
		pausePlayObj.gameObject.SetActive (true);
		BlastMeterObj.gameObject.SetActive (true);

		// Instantiate EventManager
		eventManagerObj = Instantiate(eventManagerPrefab);

		// Spawn Player and set game state to gameplay
		if (eventManagerObj != null) {
			SpawnPlayer ();
			gameState = GameState.Gameplay;
		}
	}

	public void PauseGame(){
		audioManager.PlayAudio ("Select");
		if (!pauseGame) {
			MainMenuObj.gameObject.SetActive (true);
			pauseGame = true;
			pausePlayImage.sprite = pausePlayImages [1];
			Time.timeScale = 0;
		} else if (pauseGame) {
			MainMenuObj.gameObject.SetActive (false);
			pauseGame = false;
			pausePlayImage.sprite = pausePlayImages [0];
			Time.timeScale = 1;
		}
	}

	public void MainMenu(){
		mainMenu = true;
		TitleObj.gameObject.SetActive(true);
		StageObj.gameObject.SetActive (false);
		LivesObj.gameObject.SetActive (false);
		HealthBarObj.gameObject.SetActive (false);
		ScoreTitleObj.gameObject.SetActive (false);
		ScoreObj.gameObject.SetActive (false);
		gameState = GameState.GameOver;
		eventManager.DestroyManagers ();
		if (GameObject.FindGameObjectWithTag ("Enemy") != null) {
			Destroy (GameObject.FindGameObjectWithTag ("Enemy"));
		}
		if (GameObject.FindGameObjectWithTag ("Asteroid") != null) {
			Destroy (GameObject.FindGameObjectWithTag ("Asteroid"));
		}
		Destroy(player);
		Time.timeScale = 1;
		pauseGame = false;
		pausePlayImage.sprite = pausePlayImages [0];
		MainMenuObj.gameObject.SetActive (false);
		PlayButton.gameObject.SetActive (true);
		pausePlayObj.gameObject.SetActive (false);
		ScoreObj.gameObject.SetActive (false);
		GameModeObj.gameObject.SetActive (true);
		BlastMeterObj.gameObject.SetActive (false);
	}

	// ======= Player Related Methods ======

	void FindPlayer(){
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			playerHealth = player.GetComponent<PlayerHealth> ();
			playerShoot = player.GetComponent<PlayerShoot> ();
		}
	}

	public void KillPlayer(){
		if (!playerHealth.IsPlayerImmune ()) {
			ExplodeAnimation (player.gameObject);
			Destroy (player);
			playerDead = true;
			lives--;
			if (lives < 1) {
				gameOver = true;
				GameOverScreen ();
			}
		}
	}

	IEnumerator RespawnPlayer(){

		yield return new WaitForSeconds (1.5f);

		var playerNew = Instantiate (playerPrefab,spawnPoint,Quaternion.identity);
		commencingRespawn = false;
		eventManager.SetStage (Stage.First);
		playerDead = false;
		// Player immune for duration after spawning
		playerNew.GetComponent<PlayerHealth> ().Immunity ();
		FindPlayer ();
	}

	private void SpawnPlayer(){
		GameOver.gameObject.SetActive (false);
		PlayButton.gameObject.SetActive (false);
		gemCount = 0;
		score = 0;
		lives = 3;
		gameOver = false;
		UpdateScoreText ();
		var playerNew = Instantiate (playerPrefab,spawnPoint,Quaternion.identity);
		eventManager = eventManagerObj.GetComponent<EventManager> ();
		eventManager.SetStage (Stage.First);
		playerDead = false;
		FindPlayer ();
	}

	// ====== Score and Gem Methods ======

	public void IncrementGemCount(int value){
		this.gemCount += value;
		audioManager.PlayAudio ("Gem");
	}

	public void IncrementScore(int value){
		this.score += value;
		UpdateScoreText ();
	}

	public int GetGemCount(){
		return this.gemCount;
	}

	public int GetScore(){
		return this.score;
	}

	public void SpawnGem(GameObject target){
		var gem = Instantiate (gemPrefab, target.transform.position, Quaternion.identity);
	}

	void UpdateScoreText(){
		var scoreString = string.Format("{0:000000}", score);
		scoreTextUI.text = scoreString;
	}
		
		
	public Stage CurrentStage(){
		var stage = eventManager.GetStage ();
		return stage;
	}

	public void ExplodeAnimation(GameObject target){
		audioManager.PlayAudio ("Explosion");
		var explosion = Instantiate (explosionPrefab,target.transform.position,Quaternion.identity);
	}
		
	public GameState GetGameState(){
		return gameState;
	}

	public GameMode GetGameMode(){
		return this.gameMode;
	}

}

public enum GameState {
	Startup,
	Gameplay,
	GameOver
}

public class GameMode {
	// Initialize with Event manager settings, enemy manager settings,
	// asteroid manager settings and item manager settings

	// ======= Stage, enemy spawn delay, asteroid spawn delay =======
	// ======= Standard -> Stage: First, SpawnDelay: 4f (initially) =======
	// ======= Infiinite -> Stage: Inifinite, SpawnDelay: 2f (for all of game) =======

	public Stage stage;
	public float enemySpawnDelay;
	public float asteroidSpawnDelay;
	public float itemSpawnDelay;

	// Init() method to set these conditions
	public void init(Stage stage, float enemySD, float asteroidSD, float itemSD){
		this.stage = stage;
		this.enemySpawnDelay = enemySD;
		this.asteroidSpawnDelay = asteroidSD;
		this.itemSpawnDelay = itemSD;
	}
}
