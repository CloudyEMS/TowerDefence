using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {

	[SerializeField]
	PathNode nextNode;

	public PathNode NextNode {
		get {
			return nextNode;
		}
	}

	// Just visualize the path in the scene view.
	void OnDrawGizmos(){
		if(nextNode){
			Vector3 dir = (transform.position - nextNode.transform.position).normalized;
			Vector3 nextNodePos = nextNode.transform.position + dir * 0.2f;
			Gizmos.DrawLine(transform.position, nextNodePos);
			Gizmos.DrawCube(nextNodePos, new Vector3(0.05f, 0.05f, 0.05f));
		}
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position,0.2f);
	}
}
