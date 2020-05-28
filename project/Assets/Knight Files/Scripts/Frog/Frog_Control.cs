using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Control : MonoBehaviour
{

	//是否朝右
	[HideInInspector]
	public bool facingRight = true;

	// Use this for initialization
	//定义怪物的四种状态：站立、行走、无所事事
	public const int STATE_STAND = 0;
	public const int STATE_JUMP = 1;

	//速度
	public float maxSpeed = 80f;
	public float moveForce = 200f;

	//怪物当前状态
	private int NowState;
	//游戏角色
	public GameObject Player;
	//怪物思考时间
	public const int AI_THINK_TIME = 2;
	//触发怪物攻击的临界距离
	public const int AI_ATTACT_DISTANCE = 5;

	//上一次思考的时间
	private float LastThinkTime;

	//初始位置


	//人物跟踪点
	public Transform groundcheck;

	void Awake(){
		Player = GameObject.Find ("Player");
		groundcheck = GameObject.Find ("groundcheck").GetComponent<Transform> ();
	}

	void Start ()
	{

	}

	void Update ()
	{

		//当敌人与怪物间的距离小于攻击范围半径的时候  
		if (Vector2.Distance (transform.position, groundcheck.transform.position) < AI_ATTACT_DISTANCE) {
			//敌人开始奔跑  
			this.GetComponent<Animator> ().Play ("Frog_jump");
			//敌人进入奔跑状态  
			NowState = STATE_JUMP;
			//使敌人面向角色
			if (transform.position.x > groundcheck.position.x && facingRight) {
				Flip ();
			} else if (transform.position.x < groundcheck.position.x && !facingRight) {
				Flip ();
			}
			

			if (facingRight) {
				if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x)  < maxSpeed)
					GetComponent<Rigidbody2D> ().AddForce (Vector2.right * moveForce);

				else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
					GetComponent<Rigidbody2D> ().AddForce (Vector2.left * moveForce);

				//GetComponent<Rigidbody2D> ().velocity = new Vector2 (Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) < maxSpeed)
					GetComponent<Rigidbody2D> ().AddForce (Vector2.left * moveForce);

				else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
					GetComponent<Rigidbody2D> ().AddForce (Vector2.right * moveForce);

				//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
			}

		} else {
			//当当前时间与上一次思考时间的差值大于怪物的思考时间时怪物开始思考  

			if (Time.time - LastThinkTime > AI_THINK_TIME) {
				//开始思考 

				LastThinkTime = Time.time;
				//获取0-2之间的随机数字  
				int Rnd = Random.Range (0, 2);
				//根据随机数值为怪物赋予不同的状态行为  
				switch (Rnd) {
				case 0:
                        //站立状态 
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					this.GetComponent<Animator> ().Play ("Frog_idle");
					NowState = STATE_STAND;
					break;

				case 1:
                        //行走状态  
                        //使怪物旋转以完成行走动作  
					if (Random.Range (0, 2) == 0)
						Flip ();
					this.GetComponent<Animator> ().Play ("Frog_jump");
                        //改变位置  
						//GetComponent<Rigidbody2D>().velocity = 3 * Vector2.right;

					if (facingRight) {
						if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x)  < maxSpeed)
							GetComponent<Rigidbody2D> ().AddForce (Vector2.right * moveForce);

						else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
							GetComponent<Rigidbody2D> ().AddForce (Vector2.left * moveForce);

						//GetComponent<Rigidbody2D> ().velocity = new Vector2 (Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
					} else {
						if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) < maxSpeed)
							GetComponent<Rigidbody2D> ().AddForce (Vector2.left * moveForce);

						else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
							GetComponent<Rigidbody2D> ().AddForce (Vector2.right * moveForce);

						//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
					}
					NowState = STATE_JUMP;
					break;
				}

			}
			switch (NowState) {
			case STATE_JUMP: 
				this.GetComponent<Animator> ().Play ("Frog_jump");
				if (facingRight) {
					if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x)  < maxSpeed)
						GetComponent<Rigidbody2D> ().AddForce (Vector2.right * moveForce);

					else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

					//GetComponent<Rigidbody2D> ().velocity = new Vector2 (Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
				} else {
					if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) < maxSpeed)
						GetComponent<Rigidbody2D> ().AddForce (Vector2.left * moveForce);

					else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

					//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
				}
				break;
			case STATE_STAND:
				this.GetComponent<Animator> ().Play ("Frog_idle");
				if (facingRight) {
					if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
						GetComponent<Rigidbody2D> ().AddForce (Vector2.left * moveForce);

					//GetComponent<Rigidbody2D> ().velocity = new Vector2 (Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
				} else {
						
					if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed)
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

					//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Time.deltaTime * SPEED, GetComponent<Rigidbody2D> ().velocity.y);
				}
				break;
			}

		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

	}
}
