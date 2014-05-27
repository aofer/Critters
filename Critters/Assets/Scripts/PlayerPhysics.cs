using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	// some basic properties
	float acceleration = 4f;
	float maxSpeed = 150f;
	float gravity = 6f;
	float maxfall = 200f;
	float jump = 200f;

	int layerMask;

	Rect box;

	Vector2 velocity;

	bool isOnGround = false;
	bool falling = false;

	int horizontalRays = 6;
	int verticalRays = 4;
	int margin = 2;

	// Use this for initialization
	void Start () {
		layerMask = LayerMask.NameToLayer ("normalCollisions");
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


	}

	void LateUpdate(){
		transform.Translate (velocity * Time.deltaTime);
	}
}
