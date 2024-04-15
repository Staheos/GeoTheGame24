using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public Circle(AudioClip audioClip) : base(audioClip)
    {
        this.attackDistance = 1.6f;
        this.damage = 3f;
        this.attackSpeed = 0.3f;
    }
    public override float CalcDamage(float raw)
    {
        return raw * 0.5f;
    }
    public override void OnChange(SpriteRenderer spriteRenderer)
    {
        Debug.Log(Resources.Load<Sprite>("CircleShapeSprite"));
        spriteRenderer.sprite = Resources.Load<Sprite>("CircleShapeSprite");
    }
    public override void AnimationLeftMouseButton(float dt, Rigidbody2D rigidbody2D)
    {
        base.AnimationLeftMouseButton(dt, rigidbody2D);
        rigidbody2D.transform.localScale = new Vector3(this.startScale.x * this.CalculateAnimationLMB(), this.startScale.y * this.CalculateAnimationLMB(), this.startScale.z);
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
        var objs = UnityEngine.Object.FindObjectsByType<TriangleEnemyScript>(FindObjectsSortMode.None);
        for (int i = 0; i < objs.Length; i++)
        {
            if (!objs[i].activated)
            {
                continue;
            }
            if ((objs[i].gameObject.transform.position - player.transform.position).magnitude <= attackDistance)
            {
                objs[i].TakeDamage(this.damage);
            }
        }
    }
}
