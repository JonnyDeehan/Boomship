using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction {

    public float speed = 50f;
    private float flyDelay = 0.1f;
    private float lastFlyTime = 0;

    void Update() {
        var canflyUp = inputState.GetButtonValue(inputButtons[0]);

        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (canflyUp && (holdTime < flyDelay) && (Time.time - lastFlyTime > flyDelay)) {
            FlyUp();
        }
    }

    private void FlyUp() {
        lastFlyTime = Time.time;
        var vel = body2d.velocity;
        body2d.velocity = new Vector2(vel.x, speed);
    }

}
