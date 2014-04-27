using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public float speed = 1.0f;
	public float maxSpeed = 10.0f;
	public float jumpHeight = 2.0f;
	public float currentMoveSpeed;
	
	private bool isJumping = false;
	private bool isWalking = false;
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody2D.velocity.x < maxSpeed && rigidbody2D.velocity.x > -maxSpeed){
			rigidbody2D.AddForce(new Vector2(speed * Time.deltaTime * Input.GetAxis("Horizontal") * 10,0));
		}
		
		print (Input.GetAxis("Horizontal"));
		if(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1){
			if(isWalking){
				anim.SetBool("isWalking", false);
				isWalking = false;  
			}
		} else {
			if(!isWalking){
				anim.SetBool("isWalking", true);
				isWalking = true;
			}
		}
		
		
//		if(Input.GetAxis("Horizontal") != 0){
//			rigidbody2D.velocity.x += speed * Time.deltaTime * Input.GetAxis("Horizontal");
//		}
		
		if(Input.GetButtonDown("Jump")){
			isJumping = true;
		}
		
		if(isJumping){
			Jump();
		}
	}
	
	void Jump(){
		rigidbody2D.AddForce(new Vector2(0,jumpHeight * Time.deltaTime * 1000));
		isJumping = false;
	}
}
