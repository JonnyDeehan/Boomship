using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable {
	
	float originalY;
	private float destroyDelay = .3f;
	public float floatStrength = 4f; 
	public float speed = -10f;
	private float minX = -10f;
	private float maxX = 20f;
	private GameMaster gameMaster;
	private Rigidbody2D body2d;
	private int gemScore = 2;
	private int gemCount = 1;
//	public GameObject audioManagerObject;
//	private AudioManager audioManager;

	void Awake(){
//		audioManager = audioManagerObject.GetComponent<AudioManager> ();
		body2d = GetComponent<Rigidbody2D>();
		gameMaster = GameMaster.gameMaster;
	}

	void Start(){
		this.originalY = this.transform.position.y;
	}

	override protected void OnCollect(GameObject target){
		// Do something with the coin for the player
		gameMaster.IncrementGemCount(gemCount);
		gameMaster.IncrementScore (gemScore);
//		audioManager.PlayAudio(gameObject);
		// Destroy the coin
		Destroy(gameObject);
	}

	void Update(){
		var vel = body2d.velocity;
		body2d.velocity = new Vector2(speed, vel.y);

		var gemPosition = transform.position;
		if (gemPosition.x < minX || gemPosition.x > maxX) {
			Destroy (gameObject);
		}

//		// Floating the coin up and down
//		transform.position = new Vector3(transform.position.x,
//			originalY + ((float)Mathf.Sin(Time.time) * floatStrength),
//			transform.position.z);

	}
}
