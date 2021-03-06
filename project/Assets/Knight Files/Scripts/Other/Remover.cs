using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			//
			Destroy (col.gameObject);

			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}
    void OnTriggerStay2D(Collider2D col)
    {
        // If the player hits the trigger...
        if (col.gameObject.tag == "Player")
        {
            // .. stop the camera tracking the player
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

            //
            Destroy(col.gameObject);

            // ... reload the level.
            StartCoroutine("ReloadGame");
        }
        else
        {
            // Destroy the enemy.
            Destroy(col.gameObject);
        }
    }

    IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
