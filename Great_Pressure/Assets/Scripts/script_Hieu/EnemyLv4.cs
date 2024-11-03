using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLv4 : MonoBehaviour
{
    public int damage;
    public int health;
    public GameObject blood;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerLv4>().TakeDamage(damage);
        }
    }
    public void TakeDmage(int damage) {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Instantiate(blood, transform.position, Quaternion.identity);
    }
}
