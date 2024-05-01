using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public bool menuMode = true;

	public Shape shape;
	// v w coś / sec
	public float speed = 11;

	public Rigidbody2D rigidBodyRef;
	public SpriteRenderer spriteRendererRef;
	public Rigidbody2D Bullet;
	public Rigidbody2D squeareBulletPattern;
	public Rigidbody2D ememyTrianglePatternRef;
	public Rigidbody2D enemyPatternRef;
	public GameObject HPbarRef;
	public GameObject HPframeRef;

	public AudioSource audioSource;
	public AudioClip audioDamage;
	public AudioClip audioCircle;
	public AudioClip audioSquare;
	public AudioClip audioTriangle;
	public AudioClip audioMusic;

	public float MAX_HP;
	public float hp;
	public float regen = 0f;

	private bool pressedW;
	private bool pressedS;
	private bool pressedA;
	private bool pressedD;

	private double totalTime = 0;

	private float trg_cd = 0;

	private float musicCooldown = 0;

	private float enemyTriangleSpawnCooldown = 0;
	private int spawnPointIndex = 0;
	private Vector2[] spawnPoints = { 
		// rogi mapy
		new Vector2(-80, 46),
		new Vector2(-80, -46),
		new Vector2(80, 46),
		new Vector2(80, -46),
		// środki boków
		new Vector2(40, 0),
		new Vector2(-40, 0),
		new Vector2(0, 23),
		new Vector2(0, -23),
	};

	private PacketHandler packetHandler;
	// Start is called before the first frame update
	void Start()
	{
		this.pressedW = false;
		this.pressedS = false;
		this.pressedA = false;
		this.pressedD = false;

		this.spriteRendererRef.sprite = Resources.Load<Sprite>("MenuSprite");
		//this.spriteRendererRef.color = new Color(this.spriteRendererRef.color.r, this.spriteRendererRef.color.g, this.spriteRendererRef.color.b, 1);
		this.gameObject.transform.position = new Vector3(0, 0, 0);
		this.rigidBodyRef.rotation = 0;
		this.HPframeRef.SetActive(false);
		this.HPbarRef.SetActive(false);
		this.totalTime = 0;
		this.musicCooldown = 0;
		this.gameObject.transform.localScale = Vector3.one;

		this.packetHandler = new PacketHandler();
		this.packetHandler.Connect();
	}

	// Update is called once per frame
	void Update()
	{
		// DLA WSZYSTKIEGO (MENU I GRY)
		SpawnEnemyIfCooldown(Time.deltaTime);

		this.musicCooldown -= Time.deltaTime;
		if (this.musicCooldown <= 0)
		{
			this.audioSource.PlayOneShot(this.audioMusic);
			this.musicCooldown = 193f;
		}

		// DLA MENU
		if (this.menuMode)
		{
			this.gameObject.transform.localScale = Vector3.one / 2;
			if (Input.GetKey(KeyCode.Return)) 
			{
				// destroy old objects
				var objs = UnityEngine.Object.FindObjectsByType<EnemyScript>(FindObjectsSortMode.None);
				for (int i = 0; i < objs.Length; i++)
				{
					if (!objs[i].Active)
					{
						continue;
					}
					Destroy(objs[i].gameObject);
				}
				foreach (var obj in UnityEngine.Object.FindObjectsByType<BulletScript>(FindObjectsSortMode.None))
				{
					if (!obj.Active)
					{
						continue;
					}
					Destroy(obj.gameObject);
				}

				this.hp = MAX_HP;
				this.HPframeRef.SetActive(true);
				this.HPbarRef.SetActive(true);
				this.shape = new Triangle(BulletShooterType.PLAYER, this.audioTriangle);
				this.shape.OnChange(this.spriteRendererRef);
				this.gameObject.transform.localScale = Vector3.one;
				this.menuMode = false;
			}
		}
		else
		{
			//this.packetHandler.Send(Encoding.ASCII.GetBytes(gameObject.transform.position.ToString() + '\0'));
			this.packetHandler.SendPosition(gameObject.transform.position);



			// dla gry (nie menu)
			if (this.hp <= 0)
			{
				this.menuMode = true;
				this.enemyTriangleSpawnCooldown = 0f;
				this.Start();
				this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
			}
			this.hp = Mathf.Min(this.MAX_HP, this.hp + regen * Time.deltaTime);

			this.totalTime += Time.deltaTime;

			//         if (Input.GetKey(KeyCode.F) && this.trg_cd <= 0)
			//{
			//	this.trg_cd = 0.1f;
			//	var newEnemy = UnityEngine.Object.Instantiate<Rigidbody2D>(this.ememyTrianglePatternRef, this.transform.position, this.transform.rotation);
			//	newEnemy.GetComponent<TriangleEnemyScript>().Activate();
			//}
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
				this.shape.OnLeftMouseButton(Time.deltaTime, this.rigidBodyRef, this.Bullet, this.squeareBulletPattern, this.audioSource);
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
				this.shape = new Triangle(BulletShooterType.PLAYER, this.audioTriangle);
				this.shape.OnChange(this.spriteRendererRef);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2) && !this.shape.IsPlayingAnimations())
			{
				this.shape = new Circle(BulletShooterType.PLAYER, this.audioCircle);
				this.shape.OnChange(this.spriteRendererRef);
			}
			if (Input.GetKeyDown(KeyCode.Alpha3) && !this.shape.IsPlayingAnimations())
			{
				this.shape = new Square(BulletShooterType.PLAYER, this.audioSquare);
				this.shape.OnChange(this.spriteRendererRef);
			}
			//if (Input.GetKeyDown(KeyCode.Alpha4) && !this.shape.IsPlayingAnimations())
			//{
			//	this.shape = new Pentagon();
			//	this.shape.OnChange(this.spriteRendererRef);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha5) && !this.shape.IsPlayingAnimations())
			//{
			//	this.shape = new Deltoid();
			//	this.shape.OnChange(this.spriteRendererRef);
			//}

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
	}
	public void TakeDamage(float amount)
	{
		this.hp = Mathf.Max(0, this.hp - this.shape.CalcDamage(amount));
		this.audioSource.PlayOneShot(this.audioDamage);
	}

	public void SpawnEnemyIfCooldown(float dt)
	{
		this.enemyTriangleSpawnCooldown -= dt;
		if (this.enemyTriangleSpawnCooldown <= 0)
		{
			if (this.spawnPointIndex >= this.spawnPoints.Length) 
			{
				this.spawnPointIndex = 0;
			}
			var position = this.spawnPoints[this.spawnPointIndex];

			// randomizer
			float randomizer = dt * 1000;
			randomizer = randomizer % 20;
			randomizer = randomizer / 10;
			position.x = position.x + randomizer;
			position.y = position.y + randomizer;

			this.spawnPointIndex += 1;
			var newEnemy = UnityEngine.Object.Instantiate<Rigidbody2D>(this.enemyPatternRef, position, this.transform.rotation);
			//newEnemy.GetComponent<TriangleEnemyScript>().Activate();
			newEnemy.GetComponent<EnemyScript>().Activate(new Triangle(BulletShooterType.ENEMY));
			this.enemyTriangleSpawnCooldown = this.CalculateSpawnEnemyCooldown(dt);
		}
	}
	public float CalculateSpawnEnemyCooldown(float dt)
	{
		float val;

		val = (float)(1 / this.totalTime * 60);
		val = Mathf.Min(2.5f, val);
		return val;
	}
}
