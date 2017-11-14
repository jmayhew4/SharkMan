using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectables : MonoBehaviour {

	private AudioSource audioSource;
	public SharkmanController SharkmanControllerScript;
	public SoundController AudioPlayer;
	public GameObject boneCounterUI;
	
	// Use this for initialization
	void Start () {
		
		AudioPlayer = GameObject.Find("SoundController").GetComponent<SoundController>();
		SharkmanControllerScript = GameObject.Find("Sharkman").GetComponent<SharkmanController>();
		boneCounterUI = GameObject.Find("BoneCounter");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.tag == "Player"){
			SharkmanControllerScript.dogBonesCollected += 1;
			boneCounterUI.GetComponentInChildren<Text>().text = SharkmanControllerScript.dogBonesCollected.ToString();
			AudioPlayer.PlayDogBarkSound();
			Destroy(gameObject);
		}
		
	}
}
