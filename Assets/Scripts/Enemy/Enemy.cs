using UnityEngine;

//TODO: Change to abstract class for later. Also, create a new interfact called IAttacker.
public class Enemy : MonoBehaviour {

	public int health = 5;
	public int money = 5;

	private PathNode currentNode;
	private bool isDead = false;

	private IMoveBehaviour moveBehaviour;
	private IDeathBehaviour deathBehaviour;

	public PathNode CurrentNode {
		get{
			return currentNode;
		}
		set{
			currentNode = value;
		}
	}

	// Use this for initialization
	void Start () {
		if(currentNode == null){
			Debug.LogError(name + " no refernce to NodePath found");
		}
		deathBehaviour = GetComponent<IDeathBehaviour>();
		if(deathBehaviour == null){
			Debug.LogError(name + " no IDeathBehaviour attached to gameobject");
		}
		moveBehaviour = GetComponent<IMoveBehaviour>();
		if(deathBehaviour == null){
			Debug.LogError(name + " no IMoveBehaviour attached to gameobject");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(currentNode == null || isDead)
			return;

		// Check if reached node.
		float distance = Vector3.Distance(transform.position, currentNode.transform.position);
		if(distance < moveBehaviour.Speed * Time.deltaTime){
			currentNode = currentNode.NextNode;
			if(currentNode == null){
				// We reached the end of the path, so go away ^^
				Debug.Log("Reached end");
				DamagePlayer();
				return;
			}
		}

		moveBehaviour.Move(currentNode.transform.position);
	}

	protected void Disappear(){
		isDead = true;
		//TODO: Add object pooler to increase performance.
		Destroy(gameObject);
	}

	private void DamagePlayer(){
		// Enemy had reached the end node. So damage the player.
		LevelManager.Current.LoseHealth();
		// And then die
		Disappear();
	}

	public void TakeDamage(int i){
		if(isDead)
			return;

		health--;
		if(health <= 0){
			LevelManager.Current.AddMoney(money);// player.AddMoney(money);
			deathBehaviour.Die();
			isDead = true;
		}
	}
}
