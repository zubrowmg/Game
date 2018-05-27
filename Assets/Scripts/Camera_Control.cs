using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour {

    private Vector3 offset;
    public Vector3 forward;
    public Vector3 backward;
    public Vector3 left;
    public Vector3 right;

    // Use this for initialization
    void Start () {
        offset =  Camera.main.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = this.transform.position + offset;




        forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);

        backward = forward * -1;


        
	}
}
