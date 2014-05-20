using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives = 3;
	
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
	
	public void setLives(int num){
		lives = num;
	}
	
	public int getLives(){
		return lives;
	}
	
	public void addLives(int num){
		lives += num;
	}
	
	public void loseLife(){
		lives -= 1;
	}
	
	public void die(){
		loseLife();
		GetComponent<character>().setIsDead(true);
		GameObject playerPool = GameObject.FindGameObjectWithTag("Pool " + playerNumber);
		transform.position = playerPool.transform.position;
		if(lives > 0){
			Invoke("respawn",5);
		}
	}
	
	public void respawn(){
		PlayerSpawner spwnr = GameObject.FindGameObjectWithTag("Script Holder").GetComponent<PlayerSpawner>();
		spwnr.respawn(gameObject);
		GetComponent<character>().setIsDead(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Player")){
			print ("hit");
			Player othPlayerScript = other.GetComponent<Player>();
			othPlayerScript.die();
		}
	}
}
