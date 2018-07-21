using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRController : MonoBehaviour {

    public GameObject GameHUD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.Get(OVRInput.Touch.SecondaryThumbRest))
        {
            GameHUD.SetActive(true);
        }
        else
        {
            GameHUD.SetActive(false);
        }
	}
}
