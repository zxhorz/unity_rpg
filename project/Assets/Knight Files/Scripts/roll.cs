using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll : MonoBehaviour {

    private float mSpeed = 3.0F;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Translate form right to left  
        transform.Translate(Vector3.right * Time.deltaTime * mSpeed);
        // If first background is out of camera view,then show sencond background  
        if (transform.position.x <= -11.8F)
        {
            //We can chenge this value to reduce the wdith between 2 background  
            transform.position = new Vector3(11.8F, transform.position.y, transform.position.z);
        }
    }
}
