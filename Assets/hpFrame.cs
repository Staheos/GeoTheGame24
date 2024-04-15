using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpFrame : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        this.gameObject.transform.SetPositionAndRotation(new Vector2(player.transform.position.x, player.transform.position.y-7), Camera.main.transform.rotation);
    }
}
