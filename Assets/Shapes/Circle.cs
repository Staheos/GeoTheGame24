using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public override void OnChange(SpriteRenderer spriteRenderer)
    {
        Debug.Log(Resources.Load<Sprite>("CircleShapeSprite"));

        //Texture2D tex = Resources.Load<Texture2D>("TrangleShapeSprite");
        //spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 128, 128), spriteRenderer.sprite.pivot);

        spriteRenderer.sprite = Resources.Load<Sprite>("CircleShapeSprite");
        //spriteRenderer.sprite = Sprite.Create()
        // dodaæ tekstury odpowiednich kszta³tów
        //spriteRenderer.sprite = Sprite.Create()
    }
}
