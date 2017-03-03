using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave{
	public Path[] paths;
	public float timeTillNextWave = 2f;
	public float timeBetweenEnemies = 2f;

	public Wave() {}

	public bool WaveFinished {
		get{
			foreach(Path p in paths){
				if(!p.SpawnedAllEnemies){
					return false;
				}
			}
			return true;
		}
	}

	public void SpawnEnemy(ref List<Enemy> enemies){
		foreach(Path p in paths){
			GameObject obj = p.SpawnEnemy();	
			if(obj != null){
				Enemy e = obj.GetComponent<Enemy>();
				if(e != null){
					enemies.Add(e);
				}
			}
		}
	}
}

[System.Serializable]
public class Path{
	public PathNode startNode;
	public GameObject[] enemies;

	private int enemyIndex = 0;

	public int EnemiesLength{
		get{
			if(enemies != null)
				return enemies.Length;
			else 
				return 0;
		}
	}

	public bool SpawnedAllEnemies {
		get {
			if(enemyIndex > EnemiesLength - 1){
				return true;
			} else {
				return false;
			}
		}
	}

	public Path() {}

	public GameObject SpawnEnemy(){
		if(enemyIndex > EnemiesLength - 1 || enemies[enemyIndex] == null ){
			return null;
		}

		PathNode nextNode = startNode.NextNode;
		Vector3 dir = (nextNode.transform.position - startNode.transform.position).normalized;
		Quaternion rot = Quaternion.LookRotation(dir);
		GameObject obj = GameObject.Instantiate(enemies[enemyIndex], startNode.transform.position, rot) as GameObject;
		if(obj){
			obj.GetComponent<Enemy>().CurrentNode = nextNode;
		}
		enemyIndex++;
		return obj;
	}
}
