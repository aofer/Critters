using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {

	GameObject character;
	character charScript;
	
	void Start(){
		character = GameObject.FindGameObjectWithTag("Player");
		charScript = character.GetComponent<character>();
	}
	void OnGUI(){
		GUI.Label(new Rect(Screen.width-200,0,150,150),"Velocity Y = " + charScript.getVelocityY());
	}
}
