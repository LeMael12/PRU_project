using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLv4 : MonoBehaviour
{
    public float turnSpeed;
    public int damage;
    public float attackRange;
    public Sprite GFX;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerLv4>().Equip(this);
        }

    }
}
