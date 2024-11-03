using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLV4 : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
