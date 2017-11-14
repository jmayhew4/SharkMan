using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip dogBarkSound;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayDogBarkSound () {
        audioSource.PlayOneShot(dogBarkSound, 0.55f);
    }
}
