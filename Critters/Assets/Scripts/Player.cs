using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int playerNumber = 0;
	private string playerCharacter = "none";

	void OnAwake () {	
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void setPlayerNumber(int num){
		playerNumber = num;
	}
	
	public void setPlayerCharacter(string characterName){
		playerCharacter = characterName;
	}
	
	public int getPlayerNumber(){
		return playerNumber;
	}
	
	public string setPlayerCharacter(){
		return playerCharacter;
	}
}
