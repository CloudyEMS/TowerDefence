using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private static MenuManager _current;
	public static MenuManager Current{
		get {
			if(_current == null){
				_current = GameObject.FindObjectOfType<MenuManager>();
			}
			return _current;
		}
	}

	public Text healthText;
	public Text moneyText;
	public Text waveText;

	void Start(){
		waveText.gameObject.SetActive(false);
	}

	public void StartSpawning(){
		LevelManager.Current.StartSpawning();
	}

	public void DisableObject(GameObject o){
		o.SetActive(false);
	}

	public void MakeUninteractable(Button b){
		b.interactable = false;
	}

	public void UpdateMoney(int m){
		moneyText.text = m.ToString() + "$";
	}

	public void UpdateHealth(int h){
		healthText.text = h.ToString() + " <3";
	}

	public void ShowLevelWon(){
		waveText.text = "Level Won! Exit with alt+f4";
		waveText.gameObject.SetActive(true);
	}

	public void ShowNextWave(){
		waveText.text = "Next Wave Incoming!";
		waveText.gameObject.SetActive(true);
		StartCoroutine(ToggleAfter(waveText.gameObject, 2f, false));
	}

	IEnumerator ToggleAfter(GameObject o, float seconds, bool active){
		yield return new WaitForSeconds(seconds);
		o.SetActive(active);
	}
}
