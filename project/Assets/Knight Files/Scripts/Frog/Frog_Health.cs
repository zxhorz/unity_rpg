using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Frog_Health : MonoBehaviour
{
	public float health = 60f;
	// The player's health.

	public float repeatDamagePeriod = 1.5f;
	// How frequently the player can be damaged.

	public AudioClip[] ouchClips;
	// Array of clips to play when the player is damaged.

	public float hurtForce = 7000f;
	// The force with which the player is pushed when hurt.

	public float damageAmount = 20f;
	// The amount of damage to take when enemies touch the player

	private float lastHitTime;
	// The time at which the player was last hit.

	// Reference to the PlayerControl script.
	private Animator anim;
	// Reference to the Animator on the player

	public PlayerHealth playerhealth;
	public void Awake ()
	{
		anim = GetComponent<Animator> ();
		playerhealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth>();
	}
		
	//攻击伤害
	void OnTriggerStay2D (Collider2D col)
	{
		
		// 敌人碰撞
		if (col.gameObject.CompareTag ("AttackRange")) {
			// 两次受攻击间隔大于无敌时间
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// hp》0
				if (health > 0f) {
					// ... take damage and reset the lastHitTime.
					TakeDamage (20f, col.transform); 
					lastHitTime = Time.time; 
				}
				else {

					// 死亡动画
					Destroy (gameObject);

				}
			}
		} else if (col.gameObject.CompareTag ("DashRange")) {
			// 两次受攻击间隔大于无敌时间
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// hp》0
				if (health > 0f) {
					// ... take damage and reset the lastHitTime.
					playerhealth.health -= 10;
					playerhealth.UpdateHealthBar();
					TakeDamage (40f, col.transform); 
					lastHitTime = Time.time; 
				}
				// hp《0
				else {

					// 死亡动画
					Destroy (gameObject);

				}
			}
		}
	}
		

	//火球伤害
	void OnCollisionEnter2D (Collision2D col)
	{


		// 敌人碰撞
		if (col.gameObject.CompareTag ("Fireball")) {
			// 两次受攻击间隔大于无敌时间
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// hp》0
				if (health > 0f) {
					// ... take damage and reset the lastHitTime.
					TakeDamage(10f,col.transform);

					lastHitTime = Time.time; 
				}
				// hp《0
				else {

					// 死亡动画
					Destroy (gameObject);

				}
			}

		}
	}
		
	public void TakeDamage (float damage,Transform enemy)
	{
		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector2 hurtVector = new Vector2 ((Mathf.Abs (transform.position.x - enemy.position.x)) / (transform.position.x - enemy.position.x), 0);

		// 受到来自敌人的力
		GetComponent<Rigidbody2D> ().AddForce (hurtVector * hurtForce);
		// 减少血量
		health -= damage;


	}
}
