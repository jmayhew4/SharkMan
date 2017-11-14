using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseUI;
	private bool paused = false;
	
	
	// Use this for initialization
	void Start () {
		
		PauseUI.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButtonDown("Pause")){
			paused = !paused;
			AudioListener.pause = true;
		}
		
		if(paused){
			PauseUI.SetActive(true);
			Time.timeScale = 0;
			
		}
		
		if(!paused){
			PauseUI.SetActive(false);
			Time.timeScale = 1;
			AudioListener.pause = false;
			
		}
	}
	
	public void Resume () {
		paused = false;
	}
	
	public void Restart () {
		Application.LoadLevel(Application.loadedLevel);
		paused = false;
	}
	
	public void MainMenu () {
		Application.LoadLevel(0);
		paused = false;
		
	}
	
	public void Quit () {
		Application.Quit();
	}
}
