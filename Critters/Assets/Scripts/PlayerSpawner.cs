using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public Transform Bunny;
	public Transform Raccoon;
	
	
	// Use this for initialization
	void Start () {
		//Instantiate 2 game objects - both have Player class in Player.cs component.
		instanBunny();
		instanRaccoon();
		
		//Go through both game objects one by one and activate a setter.
		int i = 1;
		GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
		Player playerScript1 = playerList[0].GetComponent<Player>();
		foreach(GameObject player in playerList){
			Player playerScript = player.GetComponent<Player>();
			if(playerScript.getPlayerNumber() == 0){
				playerScript.setPlayerNumber(i);
				i++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void instanBunny(){
		Instantiate(Bunny,Vector2.zero,Quaternion.identity);
	}
	
	void instanRaccoon(){
		Instantiate(Raccoon,Vector2.zero,Quaternion.identity);
	}
}
