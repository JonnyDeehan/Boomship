using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

	void Awake () {
		targetTag = "Enemy";
		base.Awake ();
	}
}
