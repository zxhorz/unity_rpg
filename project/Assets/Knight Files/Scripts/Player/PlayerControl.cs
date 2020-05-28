using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //行动方向
    [HideInInspector]
    public float h = 0;
	//是否朝右
	[HideInInspector]
	public bool facingRight = true;

	//检查是否处于能跳跃状态
	[HideInInspector]
	public bool jump = false;

	//dash
	[HideInInspector]
	public bool dash = false;

	//fireballattack
	[HideInInspector]
	public bool fireballAttack = false;

	//attack
	[HideInInspector]
	public bool attack = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 30000f;
	//private bool grounded = false;//检查player是否落地
	private Animator anim;
    

	//落地判断
	public Transform groundcheck;
	public bool grounded = true;
	public float checkradius = 0.2f;

	//判断半径
	public LayerMask ground;

	//火球模型
	public GameObject FireballPrefab;

	//攻击范围模型
	public GameObject AttackRange;

	//下踢范围模型
	public GameObject DashRange;

	//火球发射中心
	public Transform Firecenter;
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		AttackRange = GameObject.Find ("AttackRange");
		DashRange = GameObject.Find ("DashRange");

	}

	void Start ()
	{
        Count.setTime(0);
        AttackRange.SetActive (false);
		DashRange.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.J))
			attack = true;
		if (Input.GetKeyDown (KeyCode.K) && grounded && !dash)
			jump = true;
		if (Input.GetKeyDown (KeyCode.I))
			fireballAttack = true;
		if (Input.GetKeyDown (KeyCode.Space) && grounded)
			dash = true;
	}

	private void FixedUpdate ()
	{

		Move ();
		Attack ();
		Dash ();
		Jump ();
		FireballAttack ();

	}
	void Move ()
	{
		grounded = Physics2D.OverlapCircle (groundcheck.position, checkradius, ground);
		anim.SetBool ("grounded", grounded);
		anim.SetBool ("jump", jump);
		//获取水平值
        
		//h = Input.GetAxis ("Horizontal");

		//判断人物模型是否翻转
		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}

		anim.SetFloat ("Speed", Mathf.Abs (h));
		if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);

		if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
	}

	void Dash ()
	{
		grounded = Physics2D.OverlapCircle (groundcheck.position, checkradius, ground);
		anim.SetBool ("grounded", grounded);
		anim.SetBool ("jump", jump);
		if (dash) {
			DashRange.SetActive (true);
			StartCoroutine ("DisableDashRange");
			anim.SetBool ("dash", dash);
			StartCoroutine (Wait ((float)0.8));
		} else {
			anim.SetBool ("dash", dash);
		}
	}

	void Jump ()
	{

		grounded = Physics2D.OverlapCircle (groundcheck.position, checkradius, ground);
		anim.SetBool ("grounded", grounded);
		anim.SetBool ("jump", jump);

		//跳跃
		if (jump) {

			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
            
			jump = false;

		}

	}

	void Attack ()
	{
		grounded = Physics2D.OverlapCircle (groundcheck.position, checkradius, ground);
		anim.SetBool ("grounded", grounded);
		anim.SetBool ("jump", jump);

		if (attack) {
			AttackRange.SetActive (true);
			StartCoroutine ("DisableAttackRange");
			if (grounded)
				anim.SetTrigger ("attack");//平地攻击
            else
				anim.SetTrigger ("jumpattack");//跳跃攻击

			attack = false;
		}
	}

	void FireballAttack ()
	{
		grounded = Physics2D.OverlapCircle (groundcheck.position, checkradius, ground);
		anim.SetBool ("grounded", grounded);
		anim.SetBool ("jump", jump);

		if (fireballAttack) {
			
			if (grounded)
				anim.SetTrigger ("attack");//平地攻击
			else
				anim.SetTrigger ("jumpattack");//跳跃攻击
			GameObject FireballInstance;
			FireballInstance = Instantiate (FireballPrefab, Firecenter.position, Firecenter.rotation) as GameObject;
		}
		fireballAttack = false;
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	IEnumerator Wait (float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		dash = false;
	}

	IEnumerator DisableAttackRange ()
	{
		yield return new WaitForSeconds (0.5f);
		AttackRange.SetActive (false);
	}

	IEnumerator DisableDashRange ()
	{
		yield return new WaitForSeconds (0.8f);
		AttackRange.SetActive (false);
	}
}
