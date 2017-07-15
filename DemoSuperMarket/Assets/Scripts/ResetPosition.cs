using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ResetPosition : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 0, -5);
    }
}
