using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWarning : MonoBehaviour {

    GameObject gb;
    // Use this for initialization
    void Start () {
        gb = GameObject.Find("ImageWarning");
        gb.SetActive(false);
    }

    public void onClick()
    {
        Time.timeScale = 0;
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
