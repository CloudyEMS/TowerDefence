using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int health = 20;
	public int money = 100;

	public bool isDead = false;

	public void TakeDamage(){
		if(isDead)
			return;
		
		health--;
		if(health <= 0){
			isDead = true;
			Debug.Log("DEAD!");
		}
	}

	public bool Buy(int m){
		if(money >= m){
			money -= m;
			return true;
		}
		return false;
	}

	public void AddMoney(int m){
		money += m;
	}

	// Might be possible to add special abilities in the future.
}
