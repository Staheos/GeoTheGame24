using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public Shape shape;
	// v w coś / sec
	public float speed = 11;

	public Rigidbody2D rigidBodyRef;
	public SpriteRenderer spriteRendererRef;
	public Rigidbody2D Bullet;
	public Rigidbody2D ememyTrianglePatternRef;

	public float MAX_HP;
	private float hp;

	private bool pressedW;
	private bool pressedS;
	private bool pressedA;
	private bool pressedD;
	private bool pressedLMB;

	private float trg_cd = 0;

	// Start is called before the first frame update
	void Start()
	{
		this.pressedW = false;
		this.pressedS = false;
		this.pressedA = false;
		this.pressedD = false;
		this.pressedLMB = false;

        this.hp = MAX_HP;

        this.shape = new Shape();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.F) && this.trg_cd <= 0) 
		{
			this.trg_cd = 0.1f;
            var newEnemy = UnityEngine.Object.Instantiate<Rigidbody2D>(this.ememyTrianglePatternRef, this.transform.position, this.transform.rotation);
            newEnemy.GetComponent<TriangleEnemyScript>().Activate();
        }
		this.trg_cd -= Time.deltaTime;
		if (this.trg_cd <= 0)
		{
			this.trg_cd = 0;
		}

		// obrót w stronę kursora
		Vector3 mouse = Input.mousePosition;
		mouse -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
		mouse.Normalize();
		double angle = Math.Atan(mouse.y / mouse.x) * 180 / Math.PI;

		this.shape.OnTick(Time.deltaTime, this.rigidBodyRef);

		//if (Input.GetKeyDown(KeyCode.Mouse0))
		//{
		//	this.shape.OnLeftMouseButton(Time.deltaTime, this.rigidBodyRef, this.Bullet);
		//}
		if (Input.GetKey(KeyCode.Mouse0)) 
		{
			this.shape.OnLeftMouseButton(Time.deltaTime, this.rigidBodyRef, this.Bullet);
		}
		if (Input.GetKeyDown(KeyCode.Mouse1)) 
		{
			this.shape.OnRightMouseButton(Time.deltaTime);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			this.shape.OnRButton(Time.deltaTime);
		}

		// zmiana kształtu
		if (Input.GetKeyDown(KeyCode.Alpha1) && !this.shape.IsPlayingAnimations())
		{
			this.shape = new Triangle();
			this.shape.OnChange(this.spriteRendererRef);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && !this.shape.IsPlayingAnimations())
		{
			this.shape = new Circle();
			this.shape.OnChange(this.spriteRendererRef);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && !this.shape.IsPlayingAnimations())
		{
			this.shape = new Square();
			this.shape.OnChange(this.spriteRendererRef);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) && !this.shape.IsPlayingAnimations())
		{
			this.shape = new Pentagon();
			this.shape.OnChange(this.spriteRendererRef);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5) && !this.shape.IsPlayingAnimations())
		{
			this.shape = new Deltoid();
			this.shape.OnChange(this.spriteRendererRef);
		}

		if (mouse.x < 0 && mouse.y < 0)
		{
			angle = 90 + 90 + angle;
		}
		else if (mouse.x < 0)
		{
			// na dodatni
			angle = -angle;
			// dopełnienie od 0
			angle = 90 + 90 - angle;
		}
		else if (mouse.y < 0)
		{
			angle = -angle;
			angle = 90 + 90 + 90 + 90 - angle;
		}
		else
		{
			
		}
		this.rigidBodyRef.rotation = (float)angle;

		// śledzenie kamery
		Camera.main.transform.SetPositionAndRotation(new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, Camera.main.transform.position.z), Camera.main.transform.rotation);

		// Ruch
		if (Input.GetKeyDown(KeyCode.W))
		{
			this.pressedW = true;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			this.pressedS = true;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			this.pressedA = true;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			this.pressedD = true;
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			this.pressedW = false;
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			this.pressedS = false;
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			this.pressedA = false;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			this.pressedD = false;
		}

		Vector2 vector2 = Vector2.zero;
		if (this.pressedW)
		{
			vector2.y = 1;
		}
		if (this.pressedS)
		{
			vector2.y = -1;
		}
		if (this.pressedA)
		{
			vector2.x = -1;
		}
		if (this.pressedD)
		{
			vector2.x = 1;
		}
		vector2.Normalize();
		vector2 = vector2 * this.speed;
		this.rigidBodyRef.velocity = vector2;
		// Ruch koniec

	}
	public void TakeDamage(float amount)
	{
		this.hp -= amount;
		Debug.Log($"Gracz otrzymał obrażenia: {amount} pozostało: {this.hp}");
	}
}
