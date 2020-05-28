using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{

    // Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

            Count.setTime(Time.timeSinceLevelLoad);

            // ... reload the level.	
            //StartCoroutine("Win");
            SceneManager.LoadScene("Win");

		}

	}



}
