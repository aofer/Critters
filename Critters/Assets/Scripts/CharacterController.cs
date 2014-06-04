
using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// some basic properties
	float acceleration = 0.5f;
	float maxSpeed = 2f;
	float gravity = 1f;
	float maxfall = 5f;
	float jump = 200f;

	private RayCastingCollisionDetection _rayCastingController;
	private Vector3 _velocity;

	void Start () {
		print (" inside start");
		_rayCastingController = new RayCastingCollisionDetection ();
		_rayCastingController.Init (gameObject);

	} 

	void update(){

	}

	void FixedUpdate() {

		//_velocity = new Vector3(_velocity.x,Mathf.Max(_velocity.y - gravity, - maxfall),0);
		float newVelocityY = 0.0f;
		if (!_rayCastingController.OnGround) {
			newVelocityY = Mathf.Max (_velocity.y - gravity, - maxfall);
		}


		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float newVelocityX = _velocity.x;
		if (horizontalAxis != 0) {
			newVelocityX += acceleration * horizontalAxis;
			newVelocityX = Mathf.Clamp (newVelocityX, -maxSpeed, maxSpeed);
		} else if (_velocity.x != 0) {
			int modifier = _velocity.x > 0 ? -1 : 1;
			newVelocityX += acceleration * modifier;
		}
		_velocity = new Vector3 (newVelocityX, newVelocityY, 0);
		print ("move before raycastCollisionDetection:" +  _velocity);
		_velocity = _rayCastingController.Move (_velocity, gameObject);
		print ("move after raycastCollisionDetection:" +  _velocity);
		transform.Translate (_velocity * Time.deltaTime);
	}
	
	void LateUpdate(){
				
	}
}
