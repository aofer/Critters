﻿using UnityEngine;
using System.Collections;



public class character : MonoBehaviour {


	public float tempForPlay = 0.0f;

	public float accelaration = 2.0f;
	public float speed = 20.0f;
	public float maxSpeed = 100.0f;
	public float gravity = 300.0f;
	public float jumpHeight = 1000.0f;
	
	private float angleInDegrees = 0.0f;
	private float scale_x;
	private float scale_y;
	private float velocity_x;
	private float velocity_y;
	private float restartSpeed;
	private float tempTime = 0.0f;
	private float wallTempPositionX;
	private float floorTempPositionY;
	private bool isTouchingWall = false;
	private bool isJumping = false;
	private bool isInAir = true;
	
	// Use this for initialization
	void Start () {
		restartSpeed = speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		velocity_x = (speed * scale_x * Time.deltaTime);
	
		if(Input.GetKey("left")){
			if(speed <= maxSpeed)
				speed += accelaration;
			scale_x = -1;
			transform.Translate(new Vector2(velocity_x, 0));
		}

		if(Input.GetKey("right")){
			if(speed <= maxSpeed)
				speed += accelaration;
			scale_x = 1;
			transform.Translate(new Vector2(velocity_x,0));
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
		
		if(Input.GetKeyUp("left") || Input.GetKeyUp("right")){
			speed = restartSpeed;
			scale_x = 0;
		}
		
		if(Input.GetKeyDown("space") && !isJumping){
			isJumping = true;
			isInAir = true;
		}
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Floor")){
			floorTempPositionY = transform.position.y;
			isInAir = false;
			isJumping = false;
		}
		if(other.gameObject.tag.Equals("Wall")){
			wallTempPositionX = transform.position.x;
		}
	}
	
	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag.Equals("Wall")){
			print (other.transform.localPosition.x + "," + other.transform.lossyScale.x);
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
			                                 other.transform.lossyScale.y/2 + transform.lossyScale.y/2  + tempForPlay);
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
		if(velocity_x != null)
			return velocity_x.ToString();
		return "Currently Null";
	}
	
	public string getVelocityY(){
		if(velocity_y != null)
			return velocity_y.ToString();
		return "Currently Null";
	}
	

}
