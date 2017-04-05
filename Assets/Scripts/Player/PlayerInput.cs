using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState {
    public bool value;
    public float holdTime = 0;
}

public enum Buttons {
    TouchJump,
    TouchShoot,
    TouchShadow
}

public enum Condition {
    GreaterThan,
    LessThan
}

public class PlayerInput : MonoBehaviour {

    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();
    private Rigidbody2D body2D = new Rigidbody2D();
    private GameObject player;

    public float absVelY = 0f;
    public InputAxisState[] inputs;


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        body2D = player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var input in inputs) {
            SetButtonValue(input.button, input.value);
        }
    }

    void FixedUpdate() {
        absVelY = Mathf.Abs(body2D.velocity.y);
    }

    public void SetButtonValue(Buttons key, bool value) {
        if (!buttonStates.ContainsKey(key))
            buttonStates.Add(key, new ButtonState());

        // Set the button state value associated with the given button state key
        var state = buttonStates[key];

        if (state.value && !value) {
            // if the button state is true but the button value coming in is false, the button must be released
            state.holdTime = 0;
        }
        else if (state.value && value) {
            // if the button state is true and the button value coming in is also true, the button is still pressed
            state.holdTime += Time.deltaTime;
        }

        state.value = value;
    }

    public bool GetButtonValue(Buttons key) {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].value;
        else
            return false;
    }

    public float GetButtonHoldTime(Buttons key) {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].holdTime;
        else
            return 0;
    }
}

// The System.Serializable allows unity ide to present the properties of the class
// for a given game object using this class
[System.Serializable]
public class InputAxisState {
    public string axisName;
    public float offValue;
    public Buttons button;
    public Condition condition;

    public bool value {
        get {
            var val = Input.GetAxis(axisName);

            switch (condition) {
                case Condition.GreaterThan:
                    return val > offValue;
                case Condition.LessThan:
                    return val < offValue;
            }
            return false;
        }
    }
}
