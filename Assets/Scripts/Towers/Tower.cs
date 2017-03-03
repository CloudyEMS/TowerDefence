using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public Transform rotatingPart;
	public float radius = 3f;
	public float rateOfFire = 0.5f;
	public int price = 20;

	private Enemy currentEnemy;
	private float shootTimer = 0f;
	// Use this for initialization
	void Start () {
		shootTimer = rateOfFire;
	}
	
	// Update is called once per frame
	void Update () {
		if(!FindEnemy()){
				return;
		}

		LookAtEnemy();
		Shoot();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	private bool FindEnemy(){
		if(currentEnemy == null){
			return SearchForEnemy();
		} else {
			// make sure the enemy is still inside our range.
			float dist = Vector3.Distance(currentEnemy.transform.position, transform.position);
			if(dist > radius){
				// currentEnemy is now out of my range... need to find the next one.
				currentEnemy = null;
				return SearchForEnemy();
			}
			// currentEnemy is still inside my range, so be happy! 
			return true;
		}
	}

	private bool SearchForEnemy(){
		List<Enemy> enemies = LevelManager.Current.enemies;
		foreach(Enemy e in enemies){
			if(e == null)
				continue;

			// calculate distance
			float dist = Vector3.Distance(e.transform.position, transform.position);
			if(dist <= radius){
				// Found the first enemt that entered my radius! 
				currentEnemy = e;
				return true;
			}
		}
		return false;
	}

	public virtual void LookAtEnemy(){
		// TODO: Only change on Y axis.
		rotatingPart.LookAt(currentEnemy.transform.position);
	}

	// Can change this to Coroutine.
	public void Shoot(){
		shootTimer -= Time.deltaTime;
		if(shootTimer <= 0){
			shootTimer = rateOfFire;
			ShootEnemy();
		}
	}

	public virtual void ShootEnemy(){
		currentEnemy.TakeDamage(1);
	}
}
