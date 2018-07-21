using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

	OVRGrabbable grab;
	Rigidbody rb;
	bool wasGrabbing;

	// Use this for initialization
	void Start () {
		grab =  this.gameObject.GetComponent<OVRGrabbable>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (grab.isGrabbed){
			if(!wasGrabbing){
				wasGrabbing = true;
				if(!GameManager2.IsGameStarted()) GameManager2.StartGame();
			}
			Transform hand = grab.grabbedBy.transform;
			Vector3 angle = hand.rotation.eulerAngles;
			angle.x -= 315f;
			transform.rotation = Quaternion.Euler(angle);
			transform.position = hand.position;
		}else{
			rb.velocity = Vector3.zero;
			wasGrabbing = false;
		}
	}
}
