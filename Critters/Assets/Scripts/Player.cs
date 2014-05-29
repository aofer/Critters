using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives = 3;
	
	private int playerNumber = 0;
	private string playerCharacter = "none";
	private bool outOfGame = false;
	private worldAnimImpl worldScript;

	void Start () {	
		GameObject worldAnim = GameObject.FindGameObjectWithTag("World Anim");
		worldScript = worldAnim.GetComponent<worldAnimImpl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(lives <= 0)
			outOfGame = true;
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
	
	public bool isOutOfGame(){
		return outOfGame;
	}
	
	public void die(){
		loseLife();
		GetComponent<character>().setIsDead(true);
		GameObject playerPool = GameObject.FindGameObjectWithTag("Pool " + playerNumber);
		worldScript.setAnimation("blood_splatter", transform.position);
		transform.position = playerPool.transform.position;
		if(!outOfGame){
			Invoke("respawn",5);
		}
	}
	
	public void respawn(){
		PlayerSpawner spwnr = GameObject.FindGameObjectWithTag("Script Holder").GetComponent<PlayerSpawner>();
		spwnr.respawn(gameObject);
		GetComponent<character>().setIsDead(false);
	}

	void OnTriggerEnter2D(Collider2D other){
//		if(other.gameObject.tag.Equals("Player")){
//			Player othPlayerScript = other.GetComponent<Player>();
//			othPlayerScript.die();
//		}
	}
}
