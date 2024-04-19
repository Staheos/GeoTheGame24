using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public float MAX_HP = 5;
	public float moveDistance = 10;
	public float moveSpeed = 5;

	private Shape shape;
	private float hp;
	private bool active;
	public bool Active
	{ get { return this.active; } }

	public Rigidbody2D rigidbodyRef;
	public SpriteRenderer spriteRendererRef;
	public Rigidbody2D squareBulletPattern;
	public Rigidbody2D bulletPattern;
	public Rigidbody2D playerRigitbodyRef;

	// Start is called before the first frame update
	void Start()
	{
		this.hp = MAX_HP;
	}

	// Update is called once per frame
	void Update()
	{
		if (!this.Active)
		{
			return;
		}
		if (this.hp <= 0)
		{
			Destroy(this.gameObject);
		}
		this.shape.OnTick(Time.deltaTime, this.rigidbodyRef);

		Transform playerTransform = this.playerRigitbodyRef.transform;
		Vector2 vel = playerTransform.localPosition - this.transform.position;

		float angle = Mathf.Atan2(vel.y, vel.x) * 180f / Mathf.PI;
		if (angle < 0)
		{
			angle += 360f;
		}
		this.rigidbodyRef.rotation = angle;

		if (vel.magnitude > this.moveDistance)
		{
			vel.Normalize();
			vel *= this.moveSpeed;
			this.rigidbodyRef.velocity = vel;
		}
		else
		{
			this.rigidbodyRef.velocity = Vector2.zero;
			this.shape.OnLeftMouseButton(Time.deltaTime, this.rigidbodyRef, this.bulletPattern, this.squareBulletPattern);
		}
	}
	public void TakeDamage(float amount)
	{
		this.hp -= amount;
	}
	public void Activate(Shape shape)
	{
		this.active = true;
        //this.shape = new Triangle(BulletShooterType.ENEMY);
		this.shape = shape;
        this.shape.OnChange(this.spriteRendererRef);
    }
}

