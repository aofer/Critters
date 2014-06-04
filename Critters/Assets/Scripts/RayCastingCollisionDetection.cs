
using UnityEngine;
using System.Collections;

public class RayCastingCollisionDetection : MonoBehaviour {

	private BoxCollider _collider;
	private Rect _collisionRect;
	private LayerMask _collisionMask;
	private LayerMask _playerMask;
	private GameObject _entityGo;
	
	public bool OnGround { get; set; }
	public bool SideCollision { get; set; }
	public bool PlayerCollision { get; set; }
	
	public void Init(GameObject entityGo) {
		print ("rayCasting init");
		_collisionMask = LayerMask.NameToLayer("normalCollisions");
		_playerMask = LayerMask.NameToLayer("Player");
		_collider = entityGo.GetComponent<BoxCollider>();
		_entityGo = entityGo;
	}

	public Vector3 Move(Vector3 moveAmount, GameObject entityGo) {
		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		Vector3 entityPosition = entityGo.transform.position;
		// Resolve any possible collisions below and above the entity.
		deltaY = YAxisCollisions(deltaY, Mathf.Sign(deltaX), entityPosition);
		// Resolve any possible collisions left and right of the entity.
		// Check if our deltaX value is 0 to avoid unnecessary collision detection.
		if (deltaX != 0) {
			deltaX = XAxisCollisions(deltaX, entityPosition);
		}
		Vector3 finalTransform = new Vector2(deltaX, deltaY);
		return finalTransform;
	}

	
	private float XAxisCollisions(float deltaX, Vector3 entityPosition) {
		SideCollision = false;
		PlayerCollision = false;
		// It's VERY important that the entity's collider doesn't change
		// shape during the game. This will cause irregular raycast hits
		// and most likely cause things to go through layers.
		// Ensure sprites use a fixed collider size for all frames.
		_collisionRect = GetNewCollisionRect();
		// Increase this value if you want the rays to start and end
		// outside of the entity's collider bounds.
		float margin = 0.1f;
		int numOfRays = 4;
		Vector3 rayStartPoint = new Vector3(_collisionRect.center.x,
		                                    _collisionRect.yMin + margin, entityPosition.z);
		Vector3 rayEndPoint = new Vector3(_collisionRect.center.x,
		                                  _collisionRect.yMax - margin, entityPosition.z);
		float distance = (_collisionRect.width / 2) + Mathf.Abs(deltaX);
		
		for (int i = 0; i < numOfRays; ++i) {
			float lerpAmount = (float) i / ((float) numOfRays - 1);
			Vector3 origin = Vector3.Lerp(rayStartPoint, rayEndPoint, lerpAmount);
			Ray ray = new Ray(origin, new Vector2(Mathf.Sign(deltaX), 0));
			//Debug.DrawRay(ray.origin, ray.direction, Color.white);
			RaycastHit hit;
			// Bit shift the layers to tell Unity to NOT ignore them.
			if (Physics.Raycast(ray, out hit, distance, 1 << _collisionMask) ||
			    Physics.Raycast(ray, out hit, distance, 1 << _playerMask)) {
				Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
				float x = Mathf.Sign(deltaX) == -1
					? _collisionRect.xMin
						: _collisionRect.xMax;
				// Give a small amount of skin space to prevent snagging.
				float skinSpace = 0.005f;
				deltaX = (_collisionRect.center.x + hit.distance * ray.direction.x - x) + skinSpace;
				if (hit.transform.gameObject.layer == _playerMask) {
					PlayerCollision = true;
				}
				SideCollision = true;
				break;
			}
		}
		
		return deltaX;
	}
	
	private float YAxisCollisions(float deltaY, float dirX, Vector3 entityPosition) {
		OnGround = false;
		// It's VERY important that the entity's collider doesn't change
		// shape during the game. This will cause irregular raycast hits
		// and most likely cause things to go through layers.
		// Ensure sprites use a fixed collider size for all frames.
		_collisionRect = GetNewCollisionRect();
		// Increase this value if you want the rays to start and end
		// outside of the entity's collider bounds.
		float margin = 0.0f;
		int numOfRays = 4;
		Vector3 rayStartPoint = new Vector3(_collisionRect.xMin + margin,
		                                    _collisionRect.center.y, entityPosition.z);
		Vector3 rayEndPoint = new Vector3(_collisionRect.xMax - margin,
		                                  _collisionRect.center.y, entityPosition.z);
		float distance = (_collisionRect.height / 2) + Mathf.Abs(deltaY * Time.deltaTime);
		
		for (int i = 0; i < numOfRays; ++i) {
			float lerpAmount = (float) i / ((float) numOfRays - 1);
			// If we are facing left, start the rays on the left side,
			// else start the ray rays on the right side.
			// This will help ensure precise castings on the corners.
			Vector3 origin = dirX == -1
				? Vector3.Lerp(rayStartPoint, rayEndPoint, lerpAmount)
					: Vector3.Lerp(rayEndPoint, rayStartPoint, lerpAmount);
			Ray ray = new Ray(origin, new Vector2(0, Mathf.Sign(deltaY)));
			Debug.DrawRay(ray.origin, ray.direction, Color.blue);
			RaycastHit hit;
			// Bit shift the layers to tell Unity to NOT ignore them.
			if (Physics.Raycast(ray, out hit, distance, 1 << _collisionMask) /*||
			    Physics.Raycast(ray, out hit, distance, 1 << _playerMask)*/) {
				Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
				print ("COLLISION OCCURRED");
				print (" Colliding on y with " + hit.collider.gameObject);
				float y = Mathf.Sign(deltaY) == -1
					? _collisionRect.yMin
						: _collisionRect.yMax;
				// Give a small amount of skin space to prevent snagging.
				float skinSpace = 0.0005f;
				deltaY = (_collisionRect.center.y + hit.distance * ray.direction.y - y) + skinSpace;
				OnGround = true;
				break;
			}
		}
		
		return deltaY;
	}
	
	private Rect GetNewCollisionRect() {
		return new Rect(
			_collider.bounds.min.x,
			_collider.bounds.min.y,
			_collider.bounds.size.x,
			_collider.bounds.size.y);
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		
	}

	void LateUpdate () {
		
	}


}