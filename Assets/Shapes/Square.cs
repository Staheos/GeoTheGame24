using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    public Square() : base()
    {

    }
    public override void OnChange(SpriteRenderer spriteRenderer)
    {
        Debug.Log(Resources.Load<Sprite>("SquareShapeSprite"));

        //Texture2D tex = Resources.Load<Texture2D>("TrangleShapeSprite");
        //spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 128, 128), spriteRenderer.sprite.pivot);

        spriteRenderer.sprite = Resources.Load<Sprite>("SquareShapeSprite");
        //spriteRenderer.sprite = Sprite.Create()
        // doda� tekstury odpowiednich kszta�t�w
        //spriteRenderer.sprite = Sprite.Create()
    }
}
