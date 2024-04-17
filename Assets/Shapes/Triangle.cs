using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#nullable enable
public class Triangle : Shape
{
	public Triangle(BulletShooterType shooterType, AudioClip? audioClip = null) : base(shooterType, audioClip)
	{
		this.projectileVelocity = 20f;
		this.damage = 1;
		this.attackSpeed = 0.2f;
	}
    public override float CalcDamage(float raw)
    {
        return raw * 1f;
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
    public override void ActionLeftMouseButton(Rigidbody2D player, Rigidbody2D bulletPattern, Rigidbody2D squeareBulletPattern)
    {
        //var bullet = Instantiate(Bullet, transform.position, transform.rotation);
        var bullet = UnityEngine.Object.Instantiate<Rigidbody2D>(bulletPattern, player.transform.position, player.transform.rotation);
        //bullet.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Cos(angle));
        float rot = bullet.rotation;
        if (rot < 0)
        {
            rot += 360;
        }
        rot = (float)(rot * Mathf.PI / 180f);
        bullet.velocity = new Vector2((float)Mathf.Cos(rot), (float)Mathf.Sin(rot)) * this.projectileVelocity;
        var bulletScript = bullet.GetComponent<BulletScript>();
		bulletScript.SetDamage(this.damage);
        bulletScript.DestroyAfter(5f);
        bulletScript.Activate(this.bulletShooterType);
    }
}
