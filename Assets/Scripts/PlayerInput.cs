using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public float speed;
	private float minX, minY, maxX, maxY;
	private GameObject gameMasterObject;
	private GameMaster gameMaster;

	void Awake (){
		gameMasterObject = GameObject.FindGameObjectWithTag ("GameMaster");
		gameMaster = gameMasterObject.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector2 direction = new Vector2 (x, y).normalized;

		Move (direction);
	}

	void Move(Vector2 direction){

		minX = gameMaster.minX;
		minY = gameMaster.minY;
		maxX = gameMaster.maxX;
		maxY = gameMaster.maxY;

		Vector2 pos = transform.position;

		pos += direction * speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, minX, maxX);
		pos.y = Mathf.Clamp (pos.y, minY, maxY);

		transform.position = pos;
	}
}
