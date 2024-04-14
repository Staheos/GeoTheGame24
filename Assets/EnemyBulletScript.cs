using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private bool activated = false;
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
