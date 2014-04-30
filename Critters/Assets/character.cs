using UnityEngine;
using System.Collections;



public class character : MonoBehaviour {

	public float accelaration = 2.0f;
	public float speed = 20.0f;
	public float maxSpeed = 100.0f;
	public float gravity = 300.0f;
	public float jumpHeight = 1000.0f;
	public float jumpSpeed = 100.0f;
	public float hangTime = 1;
	
	private float angleInDegrees = 0.0f;
	private float scale_x;
	private float scale_y;
	private float velocity_x;
	private float velocity_y;
	private float angleInRadians;
	private float restartSpeed;
	private float tempYLocation;
	private float tempTime = 0.0f;
	private bool isJumping = false;
	private bool isInAir = true;
	private static double RADIANS_IN_DEGREE = 0.0174532925;
	
	// Use this for initialization
	void Start () {
		restartSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		angleInRadians = angleInDegrees * (float)RADIANS_IN_DEGREE;
		scale_x = Mathf.Cos(angleInRadians);
		scale_y = Mathf.Sin(angleInRadians);
		velocity_x = (speed * scale_x * Time.deltaTime);
		velocity_y = (speed * scale_y * Time.deltaTime);
		
//		print(scale_x + ", " + scale_y + ",  " + velocity_x + ", " + velocity_y);
		
		if(Input.GetKey("left")){
			if(speed <= maxSpeed)
				speed += accelaration;
			if(angleInDegrees != 180){
				angleInDegrees = 180;
			}
			transform.Translate(new Vector2(velocity_x, velocity_y));
		}

		if(Input.GetKey("right")){
			if(speed <= maxSpeed)
				speed += accelaration;
			if(angleInDegrees != 0){
				angleInDegrees = 0;
			}
			transform.Translate(new Vector2(velocity_x,velocity_y));
		}
		
		if(Input.GetKeyDown("space") && !isJumping){
			isJumping = true;
			tempYLocation = transform.position.y;
			tempTime = Time.realtimeSinceStartup;
		}
		
		if(isJumping){
			if(transform.position.y - tempYLocation <= jumpHeight){
				velocity_y += jumpSpeed;
				transform.Translate(new Vector2(velocity_x,velocity_y));
			} else if(Time.realtimeSinceStartup - temptTime <= hangTime) { 
				if(velocity_y != 0)
					velocity_y = 0;
			} else {
				isJumping = false;
				isInAir = true;
			}
		}
		
		if(Input.GetKeyUp("left") || Input.GetKeyUp("right")){
			speed = restartSpeed;
		}
		
		if(isInAir){
			velocity_y -= gravity;
			transform.Translate(new Vector2(velocity_x,velocity_y));
		} else {
			velocity_y = 0;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other){
		print ("collision");
		if(other.gameObject.tag.Equals("Floor")){
			isInAir = false;
			velocity_y = 0;
		}
	}
}
