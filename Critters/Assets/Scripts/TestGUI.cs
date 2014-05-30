using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestGUI : MonoBehaviour {

//	GameObject character;
//	character charScript;
	
	private Dictionary<int,Player> playerScripts = new Dictionary<int,Player>();
	GameObject[] guiTexts;
	
	public GUISkin osdPlayerNames;
	public GUISkin osdPlayerLives;
	
	void Start(){
//		character = GameObject.FindGameObjectWithTag("Player");
//		charScript = character.GetComponent<character>();
		int i = 0;
		
		guiTexts = GameObject.FindGameObjectsWithTag("Score GUI");
		GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in playerList){
			Player playerScript = player.GetComponent<Player>();
			if(i <= 1){
				playerScripts.Add(playerScript.getPlayerNumber(),playerScript);
			}
		}

		
	}
	void OnGUI(){
//		GUI.Label(new Rect(Screen.width-200,0,150,150),"Velocity Y = " + charScript.getVelocityY());
//		GUI.Label(new Rect(Screen.width-350,0,150,150),"Velocity X = " + charScript.getVelocityX());
		Player player1script;
		Player player2script;
		playerScripts.TryGetValue(1, out player1script);
		playerScripts.TryGetValue(2, out player2script);

		GUI.skin = osdPlayerNames;
		GUI.Box(new Rect(Screen.width/12, Screen.height/12, 200, 100), "Bunny");
		GUI.Box(new Rect(Screen.width/2 + 350, Screen.height/12, 200, 100), "Raccoon");
		GUI.skin = osdPlayerLives;
		GUI.Box(new Rect(Screen.width/12 + 200, Screen.height/12 - 10, 100, 100), player1script.getLives().ToString());
		GUI.Box(new Rect(Screen.width/2 + 570, Screen.height/12 - 10, 100, 100), player2script.getLives().ToString());
	}
}
