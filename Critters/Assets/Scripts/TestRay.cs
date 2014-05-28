using UnityEngine;
using System.Collections;

public class TestRay : MonoBehaviour {

	int layerMask;

	// Use this for initialization
	void Start () {
		layerMask = 9;
	}
	
	// Update is called once per frame
	void Update () {
		print ("layerMask = " + layerMask);
		Ray ray = new Ray (Vector3.zero, Vector3.down);
		RaycastHit hitInfo;
		Debug.DrawRay(Vector3.zero,Vector3.down,Color.green,1000f,true);
		if (Physics.Raycast (ray, out hitInfo, 1f, layerMask)) {
			print ("HIT HIT HIT");
		}
	}
}
