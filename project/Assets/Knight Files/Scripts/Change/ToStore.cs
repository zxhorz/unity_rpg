using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStore : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    public void OnClick()
    {
        SceneManager.LoadScene("Store_Scene");
    }
}

