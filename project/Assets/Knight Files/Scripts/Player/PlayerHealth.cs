using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;
	//hp
	public float repeatDamagePeriod = 1f;
	//无敌时间
	public AudioClip[] ouchClips;
	// Array of clips to play when the player is damaged.
	public float hurtForce = 7000f;
	// 受伤时受力
	public float damageAmount = 20f;
	// 受伤数值

	private SpriteRenderer healthBar;
	// 血条
	private float lastHitTime;
	// 上次受攻击时间                  
	private Vector3 healthScale;
	// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;
	// 控制脚本
	private Animator anim;
	// 人物动画


	void Awake ()
	{
		// 
		playerControl = GetComponent<PlayerControl> ();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}


	void OnCollisionEnter2D (Collision2D col)
	{
		// 敌人碰撞
		if (col.gameObject.CompareTag ( "Monster" )) {
			// 两次受攻击间隔大于无敌时间
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// hp》0
				if (health > 0f) {
					// ... take damage and reset the lastHitTime.
					TakeDamage (col.transform); 
					lastHitTime = Time.time; 
				}
				// hp《0
				else {

					//  
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> ();
					foreach (SpriteRenderer s in spr) {
						s.sortingLayerName = "UI";
					}


					// 控制脚本不可用
					GetComponent<PlayerControl> ().enabled = false;

					// 死亡动画
					anim.SetTrigger ("die");

					// 游戏结束
					StartCoroutine ("GameOver");
				}
			}
		}
	}		

	void OnCollisionStay2D (Collision2D col)
	{
		// 敌人碰撞
		if (col.gameObject.CompareTag ( "Monster" )) {
			// 两次受攻击间隔大于无敌时间
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// hp》0
				if (health > 0f) {
					// ... take damage and reset the lastHitTime.
					TakeDamage (col.transform); 
					lastHitTime = Time.time; 
				}
				// hp《0
				else {

					//  
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> ();
					foreach (SpriteRenderer s in spr) {
						s.sortingLayerName = "UI";
					}


					// 控制脚本不可用
					GetComponent<PlayerControl> ().enabled = false;

					// 死亡动画
					anim.SetTrigger ("die");

					// 游戏结束
					StartCoroutine ("GameOver");
				}
			}
		}
	}		

	void TakeDamageDash (Transform enemy)
	{
		// 减少10点血量
		health -= 10;

		// Update what the health bar looks like.
		UpdateHealthBar();
	}

	void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;
		playerControl.dash = false;
		playerControl.attack = false;
		playerControl.fireballAttack = false;


		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector2 hurtVector = new Vector2 ((Mathf.Abs(transform.position.x - enemy.position.x)) / (transform.position.x - enemy.position.x), 0);

		// 受到来自敌人的力
		GetComponent<Rigidbody2D> ().AddForce (hurtVector * hurtForce);

		// 减少10点血量
		health -= damageAmount;

	
		// 受伤动画
		anim.SetTrigger ("hurt");
		// Update what the health bar looks like.
		 UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		//		int i = Random.Range (0, ouchClips.Length);
		//		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3 (healthScale.x * health * 0.01f, 1, 1);
	}

	IEnumerator GameOver ()
	{
		yield return new WaitForSeconds (1.5f);
		// ... and then reload the level.
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex, LoadSceneMode.Single);
	}
}
