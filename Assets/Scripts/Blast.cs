using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {

	public Vector2 initialVelocity = new Vector2 (0, 10);
	private Rigidbody2D body2d;
	private GameMaster gameMaster;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();
	}

	// Use this for initialization
	void Start () {
		var startVelY = initialVelocity.y * transform.localScale.y;

		body2d.velocity = new Vector2 (initialVelocity.x, startVelY);
		body2d.constraints = RigidbodyConstraints2D.FreezePositionX;
	}

	void Update(){
		var pos = transform.position;
		if (pos.x > gameMaster.maxX || pos.x < gameMaster.minX || pos.y < gameMaster.minY
			|| pos.y > gameMaster.maxY) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if(collision.gameObject.tag == "Enemy"){
			Destroy (collision.gameObject);
			Destroy (gameObject);
		}
	}
}
