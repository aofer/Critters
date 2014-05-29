using UnityEngine;
using System.Collections;

public class worldAnimImpl : MonoBehaviour {

	private Animator bldAnim;
	private Transform bldsplt;
	
	void Start () {
		bldsplt = transform.GetChild(0);
		bldAnim = bldsplt.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void setAnimation(string name, Vector2 location){
		switch(name){
			case "blood_splatter":
				bldsplt.transform.position = location;
				bldAnim.SetTrigger("Splat");
				break;
		}
	}
}
