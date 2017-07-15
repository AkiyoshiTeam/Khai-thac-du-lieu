using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    private Camera cam;

    void Star()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            transform.Find("Camera").GetComponent<Camera>().enabled = false;
            return;
        }
        else {
            if (!transform.Find("Camera").GetComponent<Camera>().enabled)
                transform.Find("Camera").GetComponent<Camera>().enabled = true;
        }
        if (Input.GetAxis("Vertical") >= 0)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }
        else
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * -150.0f;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }
    }
}
