using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathMovement : MonoBehaviour, IMoveBehaviour {

	public float speed = 3f, angularSpeed = 10f;

	public float Speed{
		get{
			return speed;
		}
	}

	public void Move(Vector3 to){
		// Look at the next node and move forward.
		Vector3 dir = (to - transform.position);
		Quaternion rot = Quaternion.LookRotation(dir.normalized);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot, angularSpeed * Time.deltaTime);
		transform.Translate(0,0,speed * Time.deltaTime);
	}
}
