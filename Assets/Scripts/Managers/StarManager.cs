using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour {

	public GameObject starPrefab;
	private int maxStars;

	// Use this for initialization
	void Start () {
		maxStars = 40;
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		for (int s = 0; s < maxStars; ++s) {
			var star = Instantiate (starPrefab);

			star.transform.position = new Vector2 (Random.Range (min.x, max.x), Random.Range (min.y, max.y));

			star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

			star.transform.parent = transform;
		}
	}
	
}
