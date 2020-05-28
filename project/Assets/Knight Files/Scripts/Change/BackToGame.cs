using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGame : MonoBehaviour {

    GameObject gb;
    // Use this for initialization
    void Start () {
        gb = GameObject.Find("PanelPause");
    }

    public void onClick()
    {
        Time.timeScale = 1;
        if (gb.activeSelf == true)
        {
            gb.SetActive(false);
        }
        else
        {
            gb.SetActive(true);
        }
    }
}
