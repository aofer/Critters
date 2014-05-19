using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class character : MonoBehaviour {


	public float tempForPlay = 0.0f;

	public float accelaration = 1.0f;
	public float speed = 0.5f;
	public float maxSpeed = 3.0f;
	public float gravity = 0.011f;
	public float jumpHeight = 0.28f;
	
	private float scale_x;
	private float scale_y;
	private float velocity_x;
	private float velocity_y;
	private float restartSpeed;
	private bool isJumping = false;
	private bool isInAir = true;
	
	private Dictionary<int,Dictionary<string,string>> playerInputByNumber = new Dictionary<int,Dictionary<string,string>>();
	private string tempHor = "";
	private string tempVer = "";
	private string tempJump = "";
	
	private CharAnimImpl charAnimImplScript;
	private Player playerScript;
	
	// Use this for initialization
	void Start () {
	
		Dictionary<string,string> player1 = new Dictionary<string,string>();
		player1.Add("Hor","Horizontal");
		player1.Add("Ver","Vertical");
		player1.Add ("Jump","Jump");
		playerInputByNumber.Add(1,player1);
		
		Dictionary<string,string> player2 = new Dictionary<string,string>();
		player2.Add("Hor","Horizontal2");
		player2.Add("Ver","Vertical2");
		player2.Add ("Jump","Jump2");
		playerInputByNumber.Add(2,player2);
		
		restartSpeed = speed;
		charAnimImplScript = GetComponent<CharAnimImpl>();
		playerScript = GetComponent<Player>();
		
		Dictionary<string,string> tempDict;;
		playerInputByNumber.TryGetValue(playerScript.getPlayerNumber(), out tempDict);
		tempDict.TryGetValue("Hor",out tempHor);
		tempDict.TryGetValue("Ver",out tempVer);
		tempDict.TryGetValue("Jump",out tempJump);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		velocity_x = (speed * scale_x * Time.deltaTime);
		
		if(Input.GetAxis(tempHor) < 0){
			if(speed <= maxSpeed)
				speed += accelaration;
			scale_x = -1;
			transform.Translate(new Vector2(velocity_x, 0));
			
			charAnimImplScript.setAnimation("walk_left");
		}

		if(Input.GetAxis(tempHor) > 0){
			if(speed <= maxSpeed)
				speed += accelaration;
			scale_x = 1;
			transform.Translate(new Vector2(velocity_x,0));
			
			charAnimImplScript.setAnimation("walk_right");
		}
		
		if(isJumping){
				transform.Translate(new Vector2(velocity_x,velocity_y));
				velocity_y = velocity_y - gravity;
		}
		
		if(isInAir){
			velocity_y -= gravity;
   			transform.Translate(new Vector2(velocity_x,velocity_y));
		} else {
			if(velocity_y != jumpHeight)
				velocity_y = jumpHeight;
		}
	}
	
	void Update(){
		
		if(Input.GetAxis(tempHor) == 0){
			speed = restartSpeed;
			
			if(scale_x == -1){
				charAnimImplScript.setAnimation("idle_left");
			}
			if(scale_x == 1){
				charAnimImplScript.setAnimation("idle_right");
			}
			
			scale_x = 0;
		}
		
		if(Input.GetButtonDown(tempJump) && !isJumping){
			isJumping = true;
			isInAir = true;
		}
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Floor")){
			isInAir = false;
			isJumping = false;
		}
		if(other.gameObject.tag.Equals("Ceiling")){
			isJumping = false;
			if(velocity_y > 0){
				velocity_y = 0;
			}
		}
	}
	
	//TO-DO - There is no actual formula to the borders... it is kind of hard coded at the moment
	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag.Equals("Wall")){
			if(other.transform.localPosition.x < 0){
				transform.position = new Vector2(other.transform.position.x + other.transform.lossyScale.x
				                                 + transform.lossyScale.x/2,transform.position.y);
			}
			if(other.transform.localPosition.x > 0){
				transform.position = new Vector2(other.transform.position.x - other.transform.lossyScale.x*1.5f
				                                 + transform.lossyScale.x/2,transform.position.y);
			}
			
		}
		if(other.gameObject.tag.Equals("Floor")){
			transform.position = new Vector2(transform.position.x,other.transform.position.y + 
			                                 other.transform.lossyScale.y/2 + transform.lossyScale.y/2  + 0.1f);
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag.Equals("Floor")){
			if(!isJumping){
				velocity_y = 0;
			}
			isInAir = true;
		}
	}
	
	public string getVelocityX(){
			return velocity_x.ToString();
	}
	
	public string getVelocityY(){
			return velocity_y.ToString();
	}
	

}
