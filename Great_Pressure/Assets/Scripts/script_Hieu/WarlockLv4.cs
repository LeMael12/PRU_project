using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockLv4 : EnemyLv4
{
    public GameObject fireball;
    public float timeBetweenShots;
    float nextShortTime;
    public Transform shotPoint;
    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > nextShortTime)
        {
            Instantiate(fireball,shotPoint.position,transform.rotation);
            nextShortTime = Time.time + timeBetweenShots;
        }
    }
}
