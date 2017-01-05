using UnityEngine;
using System.Collections;

public class SideDash : MonoBehaviour {

	private Rigidbody2D body2d;
	private bool didDash = false;
	private Vector2 dashVelocity; 
	private float dashDelay = 0.5f; // Dash delay
	private float lastDashTime = 0; // Last dash time
	private float dashDuration = 0.75f; // Duration of a dash
	public MonoBehaviour[] disableScripts;

	void Awake(){
		body2d = gameObject.GetComponent<Rigidbody2D> ();
		dashVelocity = new Vector2 (15f, 0);
	}

	void Update(){
		var pressedDash = Input.GetAxisRaw ("Fire3");

		if (pressedDash > 0 && (Time.time - lastDashTime > dashDelay)) {
			didDash = true;
			OnDash();
		}

		if (didDash) {
			var direction = Input.GetAxisRaw ("Horizontal");

			if (direction > 0)
				body2d.velocity = new Vector2 (dashVelocity.x, 0);
			else
				body2d.velocity = new Vector2 (-1 * dashVelocity.x, 0);
		}

		if (Time.time - lastDashTime > dashDuration){
			OnDashEnd ();
		}
	}

	void OnDash(){
		var direction = Input.GetAxisRaw ("Horizontal");

		if (direction > 0)
			body2d.velocity = new Vector2 (dashVelocity.x, 0);
		else
			body2d.velocity = new Vector2 (-1 * dashVelocity.x, 0);

		lastDashTime = Time.time;
		ToggleScripts (false);
	}

	void OnDashEnd(){
		didDash = false;
		ToggleScripts (true);
	}

	void ToggleScripts(bool value){
		foreach (var script in disableScripts) {
			script.enabled = value;
		}
	}


}
