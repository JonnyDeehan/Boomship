using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public static GameMaster gameMaster;
	public GameObject playerPrefab;
	public GameObject explosionPrefab;
	public GameObject gemPrefab;
	public bool playerDead;

	private Image Healthbar; 
	private int gemCount;
	private int score;
	private int lives;
	private bool commencingRespawn;
	private bool gameOver;
	private EventManager eventManager;
	private GameObject player;
	private PlayerHealth playerHealth;
	private Vector2 spawnPoint;


	void Awake(){

//		Healthbar = GameObject.FindGameObjectWithTag("UI").transform.FindChild ("Canvas").FindChild ("Healthbar").GetComponent<Image> ();


		if (gameMaster == null) {
			gameMaster = GameObject.FindGameObjectWithTag ("GameMaster")
				.GetComponent<GameMaster> ();
		}
		if (player == null) {
			FindPlayer ();
		}

		eventManager = GameObject.FindGameObjectWithTag ("EventManager")
			.GetComponent<EventManager> ();
	}

	// Use this for initialization
	void Start () {
		gemCount = 0;
		score = 0;
		playerDead = false;
		commencingRespawn = false;
		lives = 3;
		gameOver = false;
		spawnPoint = new Vector2 (-6.48f, 1.01f);
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null && playerDead && !commencingRespawn) {
			commencingRespawn = true;
			StartCoroutine(RespawnPlayer ());
		}

		if (player != null) {
			if (playerHealth.GetHealth() <= 0f) {
				KillPlayer ();
			}
		}
	}

	void FindPlayer(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	public void KillPlayer(){
		ExplodeAnimation (player.gameObject);
		Destroy (player);
		playerDead = true;
		lives--;
		if (lives < 1) {
			gameOver = true;
			GameOverScreen ();
		}
	}

	IEnumerator RespawnPlayer(){

		yield return new WaitForSeconds (1.5f);

		var playerNew = Instantiate (playerPrefab,spawnPoint,Quaternion.identity);
		commencingRespawn = false;
		eventManager.SetStage (Stage.First);
		playerDead = false;
		FindPlayer ();
	}

	public void IncrementGemCount(int value){
		this.gemCount += value;
	}

	public void IncrementScore(int value){
		this.score += score;
	}

	public int GetGemCount(){
		return this.gemCount;
	}

	public int GetScore(){
		return this.score;
	}

	private void GameOverScreen(){

	}
		
	public Stage CurrentStage(){
		var stage = eventManager.GetStage ();

		return stage;
	}

	public void ExplodeAnimation(GameObject target){
		var explosion = Instantiate (explosionPrefab,target.transform.position,Quaternion.identity);
	}

	public void SpawnGem(GameObject target){
		var gem = Instantiate (gemPrefab, target.transform.position, Quaternion.identity);
	}

}
