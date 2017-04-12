using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public string targetTag = "Player";
	protected GameMaster gameMaster;
	protected Rigidbody2D body2d;
	public float speed = -10f;

	protected virtual void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		gameMaster = GameMaster.gameMaster;
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == targetTag) {
			OnCollect (target.gameObject);
			OnDestroy ();
		}
	}

	protected virtual void OnCollect(GameObject target){

	}

	protected virtual void OnDestroy(){
		Destroy (gameObject);
	}

	protected virtual void Update(){

		var vel = body2d.velocity;
		body2d.velocity = new Vector2(speed, vel.y);

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		if (transform.position.x < min.x) {
			Destroy (gameObject);
		}

	}
}
