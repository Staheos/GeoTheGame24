using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public Rigidbody2D rigidBodyRef;
	public Rigidbody2D triangleEnemyPatternRef;
	public float damageDistance;
	public float damage;
	// Start is called before the first frame update
	void Start()
	{
		// aby nie zaśmiecały gry
	}

	// Update is called once per frame
	void Update()
	{
		var objs = FindObjectsByType<TriangleEnemyScript>(FindObjectsSortMode.None);
		int min = 0;
		for (int i = 0; i < objs.Length; i++)
		{
			if ((objs[i].gameObject.transform.position - this.transform.position).magnitude < (objs[i].gameObject.transform.position - this.transform.position).magnitude)
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

	public void DestroyAfter(float time)
	{
		Destroy(gameObject, time);
	}
}
