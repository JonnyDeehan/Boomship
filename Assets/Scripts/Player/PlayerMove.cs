using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction {

    public float speed = 1f;
    private float flyDelay = 0.1f;
    private float lastFlyTime = 0;
	private GameMaster gameMaster;

	void Awake(){
		base.Awake ();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}


    void Update() {
		// Min and Max position of the player on the y-axis
		var maxY = 5f;
		var minY = -5f;

		// Temp position of the player to be clamped
		var playerPosition = transform.position;
		playerPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);
		transform.position = playerPosition;

		// Did press fly up/jump
        var canflyUp = inputState.GetButtonValue(inputButtons[0]);

        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (canflyUp && (holdTime < flyDelay) && (Time.time - lastFlyTime > flyDelay)) {
            FlyUp();
        }

		if(body2d.velocity.x != 0){
			gameMaster.KillPlayer ();
		}

    }

    private void FlyUp() {
        lastFlyTime = Time.time;
        var vel = body2d.velocity;
		body2d.velocity = new Vector2(vel.x, 9f);
    }

}
