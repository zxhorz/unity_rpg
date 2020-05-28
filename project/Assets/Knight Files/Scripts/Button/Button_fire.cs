using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_fire : MonoBehaviour {
    public GameObject player;
    public PlayerControl playerControl;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Click()
    {
        Debug.Log("Button_fire Clicked");
        playerControl.fireballAttack = true;
 
    }

}
