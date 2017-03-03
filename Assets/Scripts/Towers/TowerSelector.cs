using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelector : MonoBehaviour {

	private Builder builder;
	public Tower currentTower;

	void OnMouseDown(){
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		Builder.Current.CurrentTowerSelector = this;

		if(currentTower == true){
			// Tell the builder that I have a tower on me, so that it shows my info. yay :p
//			Builder.Current.ShowTowerUI(currentTower, transform.position);
			return;
		}

		Builder.Current.ShowBuildUI(transform.position);
	}

}
