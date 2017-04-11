using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

	public float speed = 1000.0f;
	private Vector2 directionToPlayer;
	public Vector2 initialVelocity;
	protected Rigidbody2D body2d;
	protected GameMaster gameMaster;
	protected string targetTag;
	private PlayerHealth health;
	private float projectileDamage = 20f;
	private GameObject player;
	private int projectileLayer = 11;
	private int thisLayer = 14;

	void Awake () {
		body2d = GetComponent<Rigidbody2D> ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		targetTag = "Player";

		player = GameObject.FindGameObjectWithTag (targetTag);

		Physics2D.IgnoreLayerCollision (projectileLayer, thisLayer);
	}

	public void SetDirectionToPlayer(Vector2 direction){
		directionToPlayer = direction.normalized;
	}

	void Start(){
		var startVelX = initialVelocity.x * transform.localScale.x; 
		var startVelY = initialVelocity.y * transform.localScale.y;

		body2d.velocity = new Vector2 (player.transform.position.x - transform.position.x,
			player.transform.position.y - transform.position.y);
	}

	void Update(){

		var pos = transform.position;

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		if (pos.x < min.x || pos.y < min.y || pos.y > max.y) {
			Destroy (gameObject);
		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D target){

		if (target.gameObject.tag == targetTag) {
			health = target.gameObject.GetComponent<PlayerHealth>();
			health.TakeDamage (projectileDamage);
		}
		Destroy (gameObject);
	}
}
