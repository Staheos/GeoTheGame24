using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
	// Przy zmianie na ten kształt
	public virtual void OnChange(SpriteRenderer spriteRenderer)
	{
		// dodać tekstury odpowiednich kształtów
		//spriteRenderer.sprite = Sprite.Create()
	}
	public virtual void Attack()
	{

	}
	public virtual void TakeDamage()
	{

	}
}
