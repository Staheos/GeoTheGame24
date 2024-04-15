using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpBar : MonoBehaviour
{
    public GameObject player;
    public PlayerScript Ps;
    void Update()
    {
        this.gameObject.transform.SetPositionAndRotation(new Vector2(player.transform.position.x, player.transform.position.y - 7), Camera.main.transform.rotation);
        float scale = (Ps.hp) / Ps.MAX_HP;
        if (scale <= 0)
        {
            scale = 0f;
        }
        this.gameObject.transform.localScale = new Vector2(scale, 1);
    }
}
