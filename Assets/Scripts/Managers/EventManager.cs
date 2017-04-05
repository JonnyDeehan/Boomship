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

	private Stage stage;
	private float stageTime;
	private bool didStartCountDown;
	private GameMaster gameMaster;
	private AsteroidManager asteroidManager;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		asteroidManager = GameObject.FindGameObjectWithTag ("AsteroidManager").GetComponent<AsteroidManager> ();
	}

	// Use this for initialization
	void Start () {
		stage = Stage.First;
		stageTime = 30f;
		didStartCountDown = false;
		StartCoroutine(CountDownStage());
		asteroidManager.UpdateAsteroid (stage);
	}
	
	// Update is called once per frame
	void Update () {

		if (!didStartCountDown && !gameMaster.playerDead) {
			didStartCountDown = true;
			StartCoroutine(CountDownStage());
		}
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
			asteroidManager.UpdateAsteroid (stage);
		} else if (stage == Stage.Second) {
			stage = Stage.Third;
			asteroidManager.UpdateAsteroid (stage);
		} else if (stage == Stage.Third) {
			stage = Stage.Indefinite;
			asteroidManager.UpdateAsteroid (stage);
		}
		didStartCountDown = false;
	}

	public Stage GetStage(){
		return stage;
	}

	public void SetStage(Stage resetStage){
		this.stage = resetStage;
	}
}
