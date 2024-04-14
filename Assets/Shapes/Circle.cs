using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public Circle() : base()
    {
        this.attackDistance = 1f;
        this.damage = 1f;
    }
    public override void OnChange(SpriteRenderer spriteRenderer)
    {
        Debug.Log(Resources.Load<Sprite>("CircleShapeSprite"));

        //Texture2D tex = Resources.Load<Texture2D>("TrangleShapeSprite");
        //spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 128, 128), spriteRenderer.sprite.pivot);

        spriteRenderer.sprite = Resources.Load<Sprite>("CircleShapeSprite");
        //spriteRenderer.sprite = Sprite.Create()
        // doda� tekstury odpowiednich kszta�t�w
        //spriteRenderer.sprite = Sprite.Create()
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
    public override void ActionLeftMouseButton(Rigidbody2D player, Rigidbody2D bulletPattern)
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
