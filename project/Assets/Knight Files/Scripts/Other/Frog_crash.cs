using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_crash : MonoBehaviour
{

	public GameObject FrogPrefab;
	public int xdelt = 3;
	public float ydelt = 3f;
	public float m_timer = 0;
	public int k = 0;
	Vector3 trans;
	// Use this for initialization
	void Awake ()
	{
		trans = gameObject.transform.position;
	}

	void Update ()
	{
		


		if (Time.time - m_timer >= 1 || m_timer == 0) {
			m_timer = Time.time;
			GameObject FrogInstance;
			trans.x = gameObject.transform.position.x + k * xdelt;
			trans.y = gameObject.transform.position.y + ydelt;
			FrogInstance = Instantiate (FrogPrefab, trans, gameObject.transform.rotation) as GameObject;
			k++;
		}
		if (k == 3)
			Destroy (gameObject);
		

	}
}


