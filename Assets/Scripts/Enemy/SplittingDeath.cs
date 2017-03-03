using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SplittingDeath : MonoBehaviour, IDeathBehaviour  {
	public GameObject childEnemy;
	public int splits = 2;
	private Enemy enemy;

	void Start(){
		enemy = GetComponent<Enemy>();
	}

	public void Die(){
		Vector3 dir = enemy.CurrentNode.transform.position - transform.position;
		Quaternion rot = Quaternion.LookRotation(dir.normalized);
		for(int i = 0; i < splits; i++){
			GameObject eObj = Instantiate(childEnemy, transform.position + transform.right * ((i & 1) == 0 ? 1f : -1f), rot);
			Enemy e = eObj.GetComponent<Enemy>();
			e.CurrentNode = enemy.CurrentNode;
			LevelManager.Current.AddEnemy(e);
		}
		Destroy(gameObject);
	}
}