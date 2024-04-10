using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
	public virtual void OnChange(SpriteRenderer spriteRenderer)
	{
		Debug.Log(Resources.Load<Sprite>("TriangleSceneSprite"));
		
		//Texture2D tex = Resources.Load<Texture2D>("TrangleShapeSprite");
        //spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 128, 128), spriteRenderer.sprite.pivot);

        spriteRenderer.sprite = Resources.Load<Sprite>("TriangleSceneSprite");
        //spriteRenderer.sprite = Sprite.Create()
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
