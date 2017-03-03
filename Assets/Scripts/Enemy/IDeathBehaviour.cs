using UnityEngine;


public interface IDeathBehaviour  {
	void Die();
}

public interface IMoveBehaviour {
	float Speed {
		get;
	}
	void Move(Vector3 to);
}