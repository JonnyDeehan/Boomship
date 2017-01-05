using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public float speed;
	private GameMaster gameMaster;

	void Awake(){
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position;

		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		transform.position = position;

		if (transform.position.y < gameMaster.minY) {
			transform.position = new Vector2 (Random.Range (gameMaster.minX, gameMaster.maxX), gameMaster.maxY);
		}
	
	}
}
