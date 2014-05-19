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
			Vector2 startPoint = new Vector2(box.xMin + margin, box.center.y);
			Vector2 endPoint = new Vector2(box.xMax - margin, box.center.y);

			RaycastHit2D hitInfo;

			float distance = box.height / 2 + (isOnGround? margin : Mathf.Abs(velocity.y * Time.deltaTime));

			bool isConnected = false;

			for (int i = 0 ; i < verticalRays ; i++){
				float lerpAmount = (float)i / (float) verticalRays - 1;
				Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);
				Ray2D ray = new Ray2D(origin, -Vector2.up);

				hitInfo = Physics2D.Raycast(origin, -Vector2.up, distance, layerMask);

				if (hitInfo != null){
					isConnected = true;
					isOnGround = true;
					falling = false;
					float distanceToHit = Mathf.Abs(hitInfo.point.y - box.center.y);
					transform.Translate(-Vector2.up * distanceToHit);
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
