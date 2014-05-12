using UnityEngine;
using System.Collections;

public class CharAnimImpl : MonoBehaviour {

	private Animator anim;
	
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	public void setAnimation(string name){
		switch(name){
			case "walk_left":
				anim.SetBool("isWalking", false);
				anim.SetBool("isWalkingLeft", true);
				anim.SetBool("isIdleLeft", false);
				anim.SetBool("isIdle", false);
				break;
			case "walk_right":
				anim.SetBool("isWalking", true);
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("isIdleLeft", false);
				anim.SetBool("isIdle", false);
				break;
			case "idle_left":
				anim.SetBool("isWalking", false);
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("isIdleLeft", true);
				anim.SetBool("isIdle", false);
				break;
			case "idle_right":
				anim.SetBool("isWalking", false);
				anim.SetBool("isWalkingLeft", false);
				anim.SetBool("isIdleLeft", false);
				anim.SetBool("isIdle", true);
				break;
		}
	}
}
