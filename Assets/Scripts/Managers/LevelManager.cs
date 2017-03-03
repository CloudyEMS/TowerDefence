using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private static LevelManager _current;
	public static LevelManager Current{
		get {
			if(_current == null){
				_current = GameObject.FindObjectOfType<LevelManager>();
				// TODO: MAYBE DO THIS STEP LATER.
//				if(_current != this){
//					
//				}
			}
			return _current;
		}
	}

	public float timeBetweenEnemies = 2f;
	public Wave[] waves;

	[HideInInspector] public Player player;
	[HideInInspector] public List<Enemy> enemies;
	private int waveIndex = 0;
	private bool startSpawning = true;
	private float enemiesTimer = 0f;
	private float wavesTimer = 5f;
	private bool waitForNextWave = false;

	// Use this for initialization
	void Start () {
		enemiesTimer = timeBetweenEnemies;
		enemies = new List<Enemy>();
		startSpawning = false;
		// Find player.
		player = FindObjectOfType<Player>();
		if(player == null){
			Debug.LogError("No player available!!");
			return;
		}
		MenuManager.Current.UpdateHealth(player.health);
		MenuManager.Current.UpdateMoney(player.money);
	}
	
	// Update is called once per frame
	void Update () {
		if(!startSpawning)
			return;

		if(waves == null || waves.Length < 1)
			return;
		
		if(waveIndex > waves.Length - 1 || waves[waveIndex] == null)
			return;

		Spawn();
	}

	// Must probably seperate the spawning on a different script. This would allow for better extensibility.
	private void Spawn(){
		if(waitForNextWave){
			wavesTimer -= Time.deltaTime;
			if(wavesTimer <= 0f){
				waitForNextWave = false;
				waveIndex++;
				if(waveIndex < waves.Length){
					wavesTimer = waves[waveIndex].timeTillNextWave;
					MenuManager.Current.ShowNextWave();
				} else {
					// We finished the waves! So Show that game is done!
					MenuManager.Current.ShowLevelWon();
				}
			}

			return;
		}

		if(waves[waveIndex].WaveFinished){
			waitForNextWave = true;
		} else {
			enemiesTimer -= Time.deltaTime;
			if(enemiesTimer <= 0f){
				enemiesTimer = waves[waveIndex].timeBetweenEnemies;
				SpawnEnemies();
			}
		}
	}

	private void SpawnEnemies(){
		
		waves[waveIndex].SpawnEnemy(ref enemies);
	}

	public void AddEnemy(Enemy e){
		if(e != null)
			enemies.Add(e);
	}

	public void StartSpawning(){
		startSpawning = true;
	}

	public void LoseHealth(){
		if(player.isDead)
			return;
		
		player.TakeDamage();
		MenuManager.Current.UpdateHealth(player.health);
	}

	public bool Buy(int m){
		if(player.isDead)
			return false;
		
		if(player.Buy(m)){
			MenuManager.Current.UpdateMoney(player.money);
			return true;
		}
		return false;
	}

	public void AddMoney(int m){
		if(player.isDead)
			return;

		player.AddMoney(m);
		MenuManager.Current.UpdateMoney(player.money);
	}
}
