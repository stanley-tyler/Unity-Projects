using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float speed;
    private Rigidbody rb;
    private OVRCameraRig camera;
    private TextMesh textMesh;



    void Start ()
    {
       rb  = GetComponent<Rigidbody>();
       camera = GetComponent<OVRCameraRig>();
       textMesh = GetComponent<TextMesh>();
    }
	
	
	void Update () {

        //OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)
        //Input.GetKey(KeyCode.UpArrow)
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            //textMesh.text = "Button Pressed";
            rb.position = transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }
	}
}
