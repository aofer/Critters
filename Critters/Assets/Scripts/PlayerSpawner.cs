using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public Transform Bunny;
	public Transform Raccoon;
	
	private GameObject[] spawnPoints;
	
	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
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
	
	public void respawn(GameObject player){
		int spawnLoc = (int)Random.Range(0,spawnPoints.Length-1);
		player.transform.position = spawnPoints[spawnLoc].transform.position;
	}
	
	private void instanBunny(){
		Instantiate(Bunny,spawnPoints[0].transform.position,Quaternion.identity);
	}
	
	private void instanRaccoon(){
		Instantiate(Raccoon,spawnPoints[1].transform.position,Quaternion.identity);
	}
}
