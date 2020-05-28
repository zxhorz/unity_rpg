using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    [HideInInspector]
	public bool facingRight = true;

	[HideInInspector]
	public Rigidbody2D FireballPrefab;

    public Transform Firecenter;

	public Animator anim;

     public PlayerControl player;
    int direction;
    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		FireballPrefab = GetComponent<Rigidbody2D>();
        facingRight = player.facingRight;
		if (!facingRight) 
			Flip ();
        if (facingRight)
            direction = 1;
        else
            direction = -1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//        Rigidbody2D FireballInstance;
//		FireballInstance = Instantiate(FireballPrefab, Firecenter.position, Firecenter.rotation) as Rigidbody2D;
//		FireballPrefab.AddForce( new Vector2(direction, 0) * 5);
        FireballPrefab.velocity = (new Vector2(direction, 0) * 10);
    }

	void OnCollisionEnter2D (Collision2D col)
	{
			anim.Play ("Firebomb");
			StartCoroutine ("Wait");
	}

    void Flip()
    {

		Vector3 theScale = FireballPrefab.transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

	IEnumerator Wait(){
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
}

