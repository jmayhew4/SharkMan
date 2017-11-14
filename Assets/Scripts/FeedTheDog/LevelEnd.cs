using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	public SharkmanController SharkmanControllerScript;
	
	// Use this for initialization
	void Start () {
		SharkmanControllerScript = GameObject.Find("Sharkman").GetComponent<SharkmanController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.tag == "Player"){
			if(SharkmanControllerScript.dogBonesCollected == 10){
				Application.LoadLevel(2);
			}
			
		}
		
	}
}
