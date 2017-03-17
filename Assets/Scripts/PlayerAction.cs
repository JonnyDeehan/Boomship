using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction : MonoBehaviour {

    public Buttons[] inputButtons;
    public MonoBehaviour[] disableScripts;

    protected PlayerInput inputState;
    protected Rigidbody2D body2d;
    protected PlayerCollision collisionState;

    protected virtual void Awake() {
        inputState = GetComponent<PlayerInput>();
        body2d = GetComponent<Rigidbody2D>();
        collisionState = GetComponent<PlayerCollision>();
    }

    protected virtual void ToggleScripts(bool value) {
        foreach (var script in disableScripts) {
            script.enabled = value;
        }
    }
}
