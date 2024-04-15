using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBulletScript : MonoBehaviour
{
	public Rigidbody2D rigidBodyRef;
	public Rigidbody2D triangleEnemyPatternRef;
	private bool active = false;

    public float damageDistance;
    private float damage;
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
    public void SetDamage(float newDamage)
    {
        this.damage = newDamage;
    }
    public void DestroyAfter(float time)
	{
		Destroy(gameObject, time);
	}

	public void Activate()
	{
		this.active = true;
	}
}
