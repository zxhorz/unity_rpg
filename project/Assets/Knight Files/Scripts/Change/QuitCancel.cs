using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCancel : MonoBehaviour {

    GameObject gb;
    // Use this for initialization
    void Start () {
        gb = GameObject.Find("ImageWarning");
    }

    // Update is called once per frame
    public void onClick()
    {
        gb.SetActive(false);
    }
}
