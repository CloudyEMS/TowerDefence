using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTower : Tower {

	public override void LookAtEnemy(){
		return;
	}

	public override void ShootEnemy(){
		// Get a list of all enemies that are close to you, and hit them.
		// Can also use a simpler trick by using Physics.OverlapSphere()
		// But since I am not assigning colliders to my enemies it won't work.
		List<Enemy> nearbyEnemies = new List<Enemy>(1);
		foreach(Enemy e in LevelManager.Current.enemies){
			if(e == null)
				continue;

			// calculate distance
			float dist = Vector3.Distance(e.transform.position, transform.position);
			if(dist <= radius){
				// Found the first enemt that entered my radius! 
				nearbyEnemies.Add(e);
			}
		}

		foreach(Enemy e in nearbyEnemies){
			e.TakeDamage(1);
		}
	}

}
