using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_dash : MonoBehaviour {
    public GameObject player;
    public PlayerControl playerControl;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }

    public void Click()
    {
        Debug.Log("Button_attack Clicked");
        if (playerControl.grounded && !playerControl.dash)
            playerControl.dash = true;
    }
}
