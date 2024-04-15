using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    public Square(AudioClip audioClip) : base(audioClip)
    {
        this.projectileVelocity = 32f;
        this.damage = 5;
        this.attackSpeed = 1f;
    }
    public override float CalcDamage(float raw)
    {
        return raw * 1f;
    }
    public override void OnChange(SpriteRenderer spriteRenderer)
    {
        Debug.Log(Resources.Load<Sprite>("SquareShapeSprite"));
        spriteRenderer.sprite = Resources.Load<Sprite>("SquareShapeSprite");
    }
    public override void AnimationLeftMouseButton(float dt, Rigidbody2D rigidbody2D)
    {
        base.AnimationLeftMouseButton(dt, rigidbody2D);
        rigidbody2D.transform.localScale = new Vector3(this.startScale.x * this.CalculateAnimationLMB(), this.startScale.y, this.startScale.z);
    }
    public override float CalculateAnimationLMB()
    {
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
        var bullet = UnityEngine.Object.Instantiate<Rigidbody2D>(squeareBulletPattern, player.transform.position, player.transform.rotation);
        float rot = bullet.rotation;
        if (rot < 0)
        {
            rot += 360;
        }
        rot = (float)(rot * Mathf.PI / 180f);
        bullet.velocity = new Vector2((float)Mathf.Cos(rot), (float)Mathf.Sin(rot)) * this.projectileVelocity;
        var bulletScript = bullet.GetComponent<SquareBulletScript>();
        bulletScript.SetDamage(this.damage);
        bulletScript.DestroyAfter(5f);
        bulletScript.Activate();
    }
}
