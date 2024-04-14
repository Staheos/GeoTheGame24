using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriangleEnemyScript : MonoBehaviour
{
	public bool activated = false;
	public Rigidbody2D playerRigitbodyRef;
	public Rigidbody2D rigidbodyRef;
	public float moveVelocity;
	public float moveDistance;
	public const float STANDARD_ROTATION = -90f;
	public float MAX_HP;
	private float hp;
	public Rigidbody2D enemyBulletPatternRef;

	public float attackTime = 2f;
	private float attackCooldown = 0f;
	public float bulletVelocity = 10f;

	// Start is called before the first frame update
	void Start()
	{
		this.hp = this.MAX_HP;
	}

	// Update is called once per frame
	void Update()
	{
		if (!this.activated)
		{
			return;
		}
		if (this.hp <= 0)
		{
			Destroy(this.gameObject);
		}

		Transform playerTransform = this.playerRigitbodyRef.transform;

		Vector2 vel = playerTransform.localPosition - this.transform.position;

		float angle = Mathf.Atan2(vel.y, vel.x) * 180f / Mathf.PI;
		if (angle < 0)
		{
			angle += 360f;
		}
		angle += STANDARD_ROTATION;
		this.rigidbodyRef.rotation = angle;

		if (vel.magnitude > this.moveDistance)
		{
			vel.Normalize();
			vel *= this.moveVelocity;
			this.rigidbodyRef.velocity = vel;
		}
		else
		{
			this.rigidbodyRef.velocity = Vector2.zero;

			if (this.attackCooldown <= 0f)
			{
				vel.Normalize();
				this.attackCooldown = this.attackTime;
				var enemyBullet = UnityEngine.Object.Instantiate<Rigidbody2D>(this.enemyBulletPatternRef, this.transform.position, this.transform.rotation);
				float rot = enemyBullet.rotation + STANDARD_ROTATION;
				if (rot < 0)
				{
					rot += 360f;
				}
				Debug.Log(rot);
				rot = rot * Mathf.PI / 180f;
				//enemyBullet.velocity = new Vector2(Mathf.Cos(rot), Mathf.Sin(rot)) * this.bulletVelocity;
				enemyBullet.velocity = vel * this.bulletVelocity;
				enemyBullet.rotation = this.rigidbodyRef.rotation - STANDARD_ROTATION;
				var bulletScript = enemyBullet.GetComponent<EnemyBulletScript>();
				bulletScript.DestroyAfter(5f);
				bulletScript.Activate();
			}
			else
			{
				this.attackCooldown -= Time.deltaTime;
			}
		}
	}
	public void TakeDamage(float amount)
	{
		this.hp -= amount;
	}

	// aby pattern nie działał
	public void Activate()
	{
		this.activated = true;
	}
}
