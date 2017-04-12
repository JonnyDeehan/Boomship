using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable {
	
	private float destroyDelay = .3f;
	private int gemScore = 2;
	private int gemCount = 1;

	override protected void OnCollect(GameObject target){
		// Do something with the coin for the player
		gameMaster.IncrementGemCount(gemCount);
		gameMaster.IncrementScore (gemScore);

		// Destroy the coin
		Destroy(gameObject);
	}
		
}
