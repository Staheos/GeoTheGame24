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

    private bool pressedW;
	private bool pressedS;
	private bool pressedA;
	private bool pressedD;

	// Start is called before the first frame update
	void Start()
	{
		this.pressedW = false;
		this.pressedS = false;
		this.pressedA = false;
		this.pressedD = false;

		this.shape = new Shape();
		this.shape.OnChange(spriteRendererRef);
	}

	// Update is called once per frame
	void Update()
	{
		// obrót w stronę kursora
		Vector3 mouse = Input.mousePosition;
		mouse -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
		mouse.Normalize();
		Debug.Log(mouse.ToString());
		this.rigidBodyRef.rotation = (float)(Math.Atan(mouse.y / mouse.x) * 180 / Math.PI);

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
