using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Shape
{

	public Triangle() : base()
	{

	}
	public override void OnChange(SpriteRenderer spriteRenderer)
	{
		Debug.Log(Resources.Load<Sprite>("TriangleSceneSprite"));

		//Texture2D tex = Resources.Load<Texture2D>("TrangleShapeSprite");
		//spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 128, 128), spriteRenderer.sprite.pivot);

		spriteRenderer.sprite = Resources.Load<Sprite>("TriangleSceneSprite");
		//spriteRenderer.sprite = Sprite.Create()
		// doda� tekstury odpowiednich kszta�t�w
		//spriteRenderer.sprite = Sprite.Create()
	}
	public override void AnimationLeftMouseButton(float dt, Rigidbody2D rigidbody2D)
	{
		base.AnimationLeftMouseButton(dt, rigidbody2D);
		rigidbody2D.transform.localScale = new Vector3(this.startScale.x * this.CalculateAnimationLMB(), this.startScale.y, this.startScale.z);
	}
	public override float CalculateAnimationLMB()
	{
		//float strength = 3;
		//float val = this.animationTimeLMB * this.animationTimeLMB * -strength;
		//val += this.animationTimeLMB * strength;
		//val += 1;
		//return val
		float val;
		if (this.animationTimeLMB <= 0.5f)
		{
            float a = 83f;
            val = Mathf.Pow(a, this.animationTimeLMB - 0.5f) + 0.9f;
        }	
		else
		{
			val = -1.7f * this.animationTimeLMB + 2.7f;
		}
		return val;
	}
}
