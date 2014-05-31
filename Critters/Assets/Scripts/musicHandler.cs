using UnityEngine;
using System.Collections;

public class musicHandler : MonoBehaviour {

	AudioSource audio;
	void Start () {
		audio = GetComponent<AudioSource>();
		float beginTime = Random.Range(0,300f);
		audio.time = beginTime;
		audio.loop = true;
		audio.Play();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
