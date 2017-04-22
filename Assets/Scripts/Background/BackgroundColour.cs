using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColour : MonoBehaviour {

	private Camera camera;
	private EventManager eventManager;
	private Color currentBackgroundColor;
	private GameMaster gameMaster;
	private const string COLOR1 = "#08162D00";
	private const string COLOR2 = "#2E3D5600";
	private const string COLOR3 = "#5B5F88";
	private const string COLOR4 = "#9197CA";

	// #08162D00 -> stage 1
	// #2E3D5600 -> stage 2
	// #5B5F88 -> stage 3
	// #9197CA -> stage 4

	void Awake(){
		camera = GetComponent<Camera> ();
		currentBackgroundColor = new Color();
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameMaster.mainMenu) {
			ColorUtility.TryParseHtmlString(COLOR1, out currentBackgroundColor);
			camera.backgroundColor = currentBackgroundColor;
		}
		if (eventManager != null && (!gameMaster.mainMenu)) {
			// Convert hex form to Colour object -> currently set to presets for testing
			if (eventManager.GetStage () == Stage.First) {
				ColorUtility.TryParseHtmlString(COLOR1, out currentBackgroundColor);
				camera.backgroundColor = currentBackgroundColor;
			} else if (eventManager.GetStage () == Stage.Second) {
				ColorUtility.TryParseHtmlString(COLOR2, out currentBackgroundColor);
				camera.backgroundColor = currentBackgroundColor;
			} else if (eventManager.GetStage () == Stage.Third) {
				ColorUtility.TryParseHtmlString(COLOR3, out currentBackgroundColor);
				camera.backgroundColor = currentBackgroundColor;
			} else if (eventManager.GetStage () == Stage.Indefinite) {
				ColorUtility.TryParseHtmlString(COLOR4, out currentBackgroundColor);
				camera.backgroundColor = currentBackgroundColor;
			}
		} else {
			FindEventManager ();
		}
	}

	void FindEventManager(){
		if (GameObject.FindGameObjectWithTag ("EventManager") != null) {
			eventManager = GameObject.FindGameObjectWithTag ("EventManager").GetComponent<EventManager> ();
		}
	}
}
