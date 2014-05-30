using UnityEngine;
using System.Collections;

public class testFeet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Player Head") && !transform.IsChildOf(other.transform)){
			Player othPlayerScript = other.transform.parent.GetComponent<Player>();
			othPlayerScript.die();
			character thisCharScript = transform.parent.GetComponent<character>();
			thisCharScript.setBounceOffKillTrue();
		}
	}
}
