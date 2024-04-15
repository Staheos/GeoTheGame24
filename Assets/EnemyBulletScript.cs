using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float distance;
    public float damage;
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.activated)
        {
            return;
        }
        var player = FindObjectOfType<PlayerScript>();
        if ((player.gameObject.transform.position - this.transform.position).magnitude <= this.distance)
        {
            player.TakeDamage(this.damage);
            Destroy(this.gameObject);
        }
    }
    public void DestroyAfter(float time)
    {
        Destroy(gameObject, time);
    }
    public void Activate()
    {
        this.activated = true;
    }
}
