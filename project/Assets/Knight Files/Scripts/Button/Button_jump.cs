using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_jump : MonoBehaviour {
    public GameObject player;
    public PlayerControl playerControl;
    // Use this for initialization
    void Start () {
        playerControl = player.GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Click()
    {
        Debug.Log("Button_Jump Clicked");
        if(playerControl.grounded)
            playerControl.jump = true;
    }
}
