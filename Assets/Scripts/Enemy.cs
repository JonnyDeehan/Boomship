using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public Vector2 initialVelocity = new Vector2 (0, -10);
	private Rigidbody2D body2d;
	private GameMaster gameMaster;
	private GameObject player;
	public GameObject shootPrefab;
	public Transform shootPoint;
	private Vector3 shootPosition;
	private bool canShoot = true;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}

	// Use this for initialization
	void Start () {
		var startVelY = initialVelocity.y * transform.localScale.y;

		body2d.velocity = new Vector2 (initialVelocity.x, startVelY);
		body2d.constraints = RigidbodyConstraints2D.FreezePositionX;
	}

	// Instantiate the projectile prefab at the calculated fire position
	IEnumerator CreateProjectile(){
		canShoot = false;
		shootPosition = shootPoint.position;
		var clone = Instantiate (shootPrefab, shootPosition, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
		yield return new WaitForSeconds (2f);
		canShoot = true;
	}

	void Update(){

		if (canShoot && player != null) {
			StartCoroutine (CreateProjectile ());
		}

		var pos = transform.position;
		if (pos.x > gameMaster.maxX || pos.x < gameMaster.minX || pos.y < gameMaster.minY) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "Player"){
			Destroy (collision.gameObject);
			Destroy (gameObject);
		}
	}
		
}
