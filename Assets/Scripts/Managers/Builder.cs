using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	private static Builder _current;
	public static Builder Current{
		get {
			if(_current == null){
				_current = GameObject.FindObjectOfType<Builder>();
				// TODO: MAYBE DO THIS STEP LATER.
				//				if(_current != this){
				//					
				//				}
			}
			return _current;
		}
	}
	public GameObject buildButton;
	public GameObject[] towers;

	public TowerSelector CurrentTowerSelector{
		get; set;
	}

//	public void ShowUI(Vector3 pos){
//		
//	}
//
//	public void ShowUI(Vector3 pos, Vector3 offset){
//		
//	}
//
//	public void ShowTowerUI(Tower t, Vector3 pos){
//		
//	}

	public void ShowBuildUI(Vector3 pos){
		buildButton.transform.position = pos + Vector3.up * 3f;
		buildButton.SetActive(true);
	}

	public void DisablePanel(GameObject p){
		p.SetActive(false);
	}

	public void BuildTower(int i){
		if(CurrentTowerSelector == null)
			return;
		if(CurrentTowerSelector.currentTower == null){
			// FOR NOW, FIXME later
			// Must find a better way to decouple the pricing from the script, probably a ScriptableObject would solve this!
			Tower t = towers[i].GetComponent<Tower>();
			if(t){
				if(LevelManager.Current.Buy(t.price))
					BuildTowerAt(towers[i],CurrentTowerSelector.transform.position);
			}
		}
	}

	private void BuildTowerAt(GameObject tPrefab, Vector3 pos){
		GameObject tObj = Instantiate<GameObject>(tPrefab, pos, Quaternion.identity);
		if(tObj){
			Tower t = tObj.GetComponent<Tower>();
			if(t){
				CurrentTowerSelector.currentTower = t;
			}
		}
	}
}
