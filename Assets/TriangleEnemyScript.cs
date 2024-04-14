using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriangleEnemyScript : MonoBehaviour
{
    public bool activated = false;
    public Rigidbody2D playerRigitbodyRef;
    public Rigidbody2D rigidbodyRef;
    public float bulletVelocity;
    public float moveDistance;
    public const float STANDARD_ROTATION = -90f;
    public float MAX_HP;
    public float hp;

    // Start is called before the first frame update
    void Start()
    {
        this.hp = this.MAX_HP;   
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.activated)
        {
            return;
        }
        if (this.hp <= 0)
        {
            Destroy(this.gameObject);
        }

        Transform playerTransform = this.playerRigitbodyRef.transform;

        Vector2 vel = playerTransform.localPosition - this.transform.position;
        if (vel.magnitude > this.moveDistance)
        {
            vel.Normalize();
            vel *= this.bulletVelocity;
            this.rigidbodyRef.velocity = vel;
        }
        else
        {
            this.rigidbodyRef.velocity = Vector2.zero;
        }
        float angle = Mathf.Atan2(vel.y, vel.x) * 180f / Mathf.PI;
        angle += STANDARD_ROTATION;
        if (angle < 0)
        {
            angle += 360f;
        }
        this.rigidbodyRef.rotation = angle;
    }
    public void TakeDamage(float amount)
    {
        this.hp -= amount;
    }

    // aby pattern nie działał
    public void Activate()
    {
        this.activated = true;
    }
}
