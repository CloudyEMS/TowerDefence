using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDeath : MonoBehaviour, IDeathBehaviour  {
	public void Die(){
		Destroy(gameObject);
	}
}