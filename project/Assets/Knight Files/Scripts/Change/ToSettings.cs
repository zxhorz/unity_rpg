using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnClick()
    {
        SceneManager.LoadScene("Settings");
    }
}
