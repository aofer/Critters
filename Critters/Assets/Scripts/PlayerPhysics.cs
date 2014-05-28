using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	// some basic properties
	float acceleration = 0.5f;
	float maxSpeed = 2f;
	float gravity = 1f;
	float maxfall = 200f;
	float jump = 200f;

	int layerMask;

	Rect box;

	Vector2 velocity;

	bool isOnGround = false;
	bool falling = false;

	int horizontalRays = 6;
	int verticalRays = 4;
	float margin = 0.1f;

	// Use this for initialization
	void Start () {
		layerMask = 1 << LayerMask.NameToLayer("normalCollisions");
		print (LayerMask.NameToLayer ("normalCollisions"));
	}
	
	// Update is called once per frame
	void Update () {
		box = new Rect (
			collider.bounds.min.x,
			collider.bounds.min.y,
			collider.bounds.size.x,
			collider.bounds.size.y
		);
	}

	void FixedUpdate() {

		if (!isOnGround) {
			velocity = new Vector2(velocity.x,Mathf.Max(velocity.y - gravity, - maxfall));
		}
		if (velocity.y < 0) {
			falling = true;
		}
		if (isOnGround || falling) {
			Vector3 startPoint = new Vector3(box.xMin + margin, box.center.y,transform.position.z);
			Vector3 endPoint = new Vector3(box.xMax - margin, box.center.y, transform.position.z);

			RaycastHit hitInfo;

			float distance = box.height / 2 + (isOnGround? margin : Mathf.Abs(velocity.y * Time.deltaTime));

			bool isConnected = false;

			for (int i = 0 ; i < verticalRays ; i++){
				float lerpAmount = (float)i / (float) verticalRays - 1;
				Vector3 origin = Vector3.Lerp(startPoint, endPoint, lerpAmount);
				Ray ray = new Ray(origin, Vector3.down);

				isConnected = Physics.Raycast(ray, out hitInfo, distance, layerMask);
				if (isConnected){
					isOnGround = true;
					falling = false;
					transform.Translate(Vector3.down * (hitInfo.distance - box.height/2));
					velocity = new Vector2(velocity.x, 0);
					break;
				}
			}
			if (!isConnected){
				isOnGround = false;
			}
		}

		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float newVelocityX = velocity.x;
		if (horizontalAxis != 0) {
			newVelocityX += acceleration * horizontalAxis;
			newVelocityX = Mathf.Clamp (newVelocityX, -maxSpeed, maxSpeed);
		} else if (velocity.x != 0) {
			int modifier = velocity.x > 0 ? -1 : 1;
			newVelocityX += acceleration * modifier;
		}
		velocity = new Vector2 (newVelocityX, velocity.y);

		if (velocity.x != 0) {
			Vector3 startPoint = new Vector3(box.center.x, box.yMin + margin, transform.position.z);
			Vector3 endPoint = new Vector3(box.center.x, box.yMax - margin, transform.position.z);

			RaycastHit hitInfo;

			float sideRayLength = box.width / 2 + Mathf.Abs(newVelocityX * Time.deltaTime);
			Vector3 direction = newVelocityX > 0 ? Vector3.right : Vector3.left;
			bool connected = false;

			for(int i = 0; i < horizontalRays; i++){
				float lerpAmount = (float) i / (float) (horizontalRays -1);
				Vector3 origin = Vector3.Lerp(startPoint, endPoint, lerpAmount);
				Ray ray = new Ray(origin, direction);

				connected = Physics.Raycast(ray, out hitInfo, sideRayLength);

				if(connected){
					transform.Translate(direction * (hitInfo.distance - box.width / 2));
					velocity = new Vector2(0, velocity.y);
					break;
				}
			}
		}
	}

	void LateUpdate(){
		transform.Translate (velocity * Time.deltaTime);

	}
}
