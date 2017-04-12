using UnityEngine;
using System.Collections;

public class PlayerFlash : MonoBehaviour {

	private SpriteRenderer spRndrer;

	void Awake(){
		spRndrer = GetComponent<SpriteRenderer> ();
	}

	public IEnumerator Flash(){

		for (int i = 0; i < 4; i++) {
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
}
