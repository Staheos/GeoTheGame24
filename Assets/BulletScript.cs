#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public Rigidbody2D rigidBodyRef;
    public SpriteRenderer spriteRendererRef;

	public Rigidbody2D triangleEnemyPatternRef;

	private bool active = false;
    public bool Active {  get { return this.active; } }

    public float damageDistance;
    private float damage;
    private BulletShooterType bulletShooterType;
    // Start is called before the first frame update
    void Start()
	{
		// aby nie zaśmiecały gry
	}

	// Update is called once per frame
	void Update()
	{
		if (this.active)
		{
            if (this.bulletShooterType == BulletShooterType.ENEMY)
            {
                var playersScripts = FindObjectsByType<PlayerScript>(FindObjectsSortMode.None);
                if (playersScripts.Length > 0)
                {
                    int min = 0;
                    for (int i = 0; i < playersScripts.Length; i++)
                    {
                        if ((playersScripts[i].gameObject.transform.position - this.transform.position).magnitude <= (playersScripts[min].gameObject.transform.position - this.transform.position).magnitude)
                        {
                            min = i;
                        }
                    }
                    if ((playersScripts[min].gameObject.transform.position - this.transform.position).magnitude <= this.damageDistance)
                    {
                        playersScripts[min].TakeDamage(this.damage);
                        Destroy(this.gameObject);
                    }
                }
            }
            else if (this.bulletShooterType == BulletShooterType.PLAYER)
            {
                var enemyScripts = FindObjectsByType<EnemyScript>(FindObjectsSortMode.None);
                if (enemyScripts.Length > 0)
                {
                    int min = 0;
                    for (int i = 0; i < enemyScripts.Length; i++)
                    {
                        if ((enemyScripts[i].gameObject.transform.position - this.transform.position).magnitude <= (enemyScripts[min].gameObject.transform.position - this.transform.position).magnitude)
                        {
                            min = i;
                        }
                    }
                    if ((enemyScripts[min].gameObject.transform.position - this.transform.position).magnitude <= this.damageDistance)
                    {
                        enemyScripts[min].TakeDamage(this.damage);
                        Destroy(this.gameObject);
                    }
                }
            }
            else
            {
                var objs = FindObjectsByType<TriangleEnemyScript>(FindObjectsSortMode.None);
                if (objs.Length > 1)
                {
                    int min = 1;
                    for (int i = 0; i < objs.Length; i++)
                    {
                        if (!objs[i].activated)
                        {
                            continue;
                        }
                        if ((objs[i].gameObject.transform.position - this.transform.position).magnitude <= (objs[min].gameObject.transform.position - this.transform.position).magnitude)
                        {
                            min = i;
                            Debug.Log($"found damage: {(objs[min].gameObject.transform.position - this.transform.position).magnitude}");
                        }
                    }
                    if ((objs[min].gameObject.transform.position - this.transform.position).magnitude <= this.damageDistance)
                    {
                        objs[min].TakeDamage(this.damage);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
	}
    public void SetDamage(float newDamage)
    {
        this.damage = newDamage;
    }

	public void DestroyAfter(float time)
	{
		Destroy(gameObject, time);
	}
	public void Activate(BulletShooterType shooterType = BulletShooterType.UNKNOWN, Sprite? sprite = null)
	{
		this.active = true;
        this.bulletShooterType = shooterType;
        if (sprite != null )
        {
            this.spriteRendererRef.sprite = sprite;
        }
	}
}
