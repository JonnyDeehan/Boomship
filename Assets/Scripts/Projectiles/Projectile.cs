using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	public Vector2 initialVelocity;
	protected Rigidbody2D body2d;
	private static float MINX = -10f;
	private static float MAXX = 20f;
	protected GameMaster gameMaster;
	protected string targetTag;
	private int killEnemyScore = 20;
	private PlayerHealth health;
	private float projectileDamage = 20f;

	protected virtual void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}

	// Use this for initialization
	protected virtual void Start () {

		var startVelX = initialVelocity.x * transform.localScale.x; 
		var startVelY = initialVelocity.y * transform.localScale.y;

		body2d.velocity = new Vector2 (startVelX, startVelY);
	}

	protected virtual void Update(){
		var minX = MINX;
		var maxX = MAXX;

		if (transform.position.x > maxX || transform.position.x < minX) {
			Destroy (gameObject);
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D target){
		
		if (target.gameObject.tag == targetTag) {
			if (targetTag == "Player") {
				health = target.gameObject.GetComponent<PlayerHealth>();
				health.TakeDamage (projectileDamage);
			} else if(targetTag == "Enemy") {
				gameMaster.IncrementScore (killEnemyScore);
//				Destroy (target.gameObject);
			}
		}
		Destroy (gameObject);
	}
}
