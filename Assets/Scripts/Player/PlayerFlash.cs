using UnityEngine;
using System.Collections;

public class PlayerFlash : MonoBehaviour {

	private SpriteRenderer spRndrer;

	void Awake(){
		spRndrer = GetComponent<SpriteRenderer> ();
	}

	public IEnumerator FlashSprites(SpriteRenderer[] sprites, int numTimes, float delay, bool disable = false) {
		// number of times to loop
		for (int loop = 0; loop < numTimes; loop++) {            
			// cycle through all sprites
			for (int i = 0; i < sprites.Length; i++) {                
				if (disable) {
					// for disabling
					sprites[i].enabled = false;
				} else {
					// for changing the alpha
					sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.5f);
				}
			}

			// delay specified amount
			yield return new WaitForSeconds(delay);

			// cycle through all sprites
			for (int i = 0; i < sprites.Length; i++) {
				if (disable) {
					// for disabling
					sprites[i].enabled = true;
				} else {
					// for changing the alpha
					sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 1);
				}
			}

			// delay specified amount
			yield return new WaitForSeconds(delay);
		}
	}

	public IEnumerator Flash(){

		spRndrer.enabled = true;
		yield return new WaitForSeconds (.05f);
		spRndrer.enabled = false;
		yield return new WaitForSeconds (.05f);
		spRndrer.enabled = true;
		yield return new WaitForSeconds (.05f);
		spRndrer.enabled = false;
		yield return new WaitForSeconds (.05f);
		spRndrer.enabled = true;
	}
}
