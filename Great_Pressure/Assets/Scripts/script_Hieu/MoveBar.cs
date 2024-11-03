using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float moveSpeed = 3f;
    private bool movingRight = true;
    private float minX = 5f;
    private float maxX = 20f;

    void Update()
    {
        // Di chuyển thanh
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= maxX)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= minX)
            {
                movingRight = true;
            }
        }
}
        private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Player")
            {
            collision.transform.parent =transform;
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent =transform;
        }
    }
}
