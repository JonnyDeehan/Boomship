using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot: PlayerAction {

	public GameObject projectilePrefab;
	public Vector2 firePosition;
	private float shootDelay = 0.1f;
	private float lastShootTime = 0;
	private float reloadTime = 1f;
	private bool didReload;
	private float ammo;
	private AudioManager audioManager;
	public float Ammo {
		get{
			return ammo;
		}
	}

	void Start(){
		ammo = 9f;
		didReload = false;
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
	}

	void Update(){
		var shoot = inputState.GetButtonValue (inputButtons [0]);
		var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (shoot && (holdTime < shootDelay) && (Time.time - lastShootTime > shootDelay) && (ammo>0f)) {
			Shoot ();
		}
		if (ammo < 9f && !didReload) {
			didReload = true;
			StartCoroutine (Reload (reloadTime));
		}
	}

	IEnumerator Reload(float time){
		yield return new WaitForSeconds(time);
		ammo++;
		didReload = false;
	}

	private void Shoot(){
		audioManager.PlayAudio ("Shoot");
		ammo--;
		
		lastShootTime = Time.time;
		var pos = firePosition;

		// Set the firing position relative to the player transform position
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		var projectileClone = Instantiate (projectilePrefab,pos,Quaternion.identity);
		Physics2D.IgnoreCollision(projectileClone.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		projectileClone.transform.localScale = transform.localScale;

	}
}
