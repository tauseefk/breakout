using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(LayerMask.NameToLayer("Boundary") == other.gameObject.layer) {
			Debug.Log("boing!");
		}
	}
}
