using UnityEngine;
using System.Collections;

public class BoomBall : MonoBehaviour {

	public Vector2 initialVelocity;
	private Rigidbody2D body2d;
	private GameObject player;
	private PlayerInput playerInput;
	private GameMaster gameMaster;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInput = player.GetComponent<PlayerInput> ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}

	// Use this for initialization
	void Start () {
		var startVelY = initialVelocity.y * transform.localScale.y;
		var startVelX = initialVelocity.x * transform.localScale.x;

		body2d.velocity = new Vector2 (player.transform.position.x - transform.position.x, 
			player.transform.position.y - transform.position.y);
	}

	void Update(){
		var pos = transform.position;
		if (pos.x > gameMaster.maxX || pos.x < gameMaster.minX || pos.y < gameMaster.minY
			|| pos.y > gameMaster.maxY) {
			Destroy (gameObject);
		}
	}
		
	void OnTriggerEnter2D(Collider2D collision){
		if(collision.gameObject.tag == "Player"){
			Destroy (collision.gameObject);
			Destroy (gameObject);
		}
	}
}
