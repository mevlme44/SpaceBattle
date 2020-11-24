using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBoss : MonoBehaviour
{
    private float tumble;
    private int hp = 10;
    private Rigidbody2D rb;
    private GameManager manager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tumble = Random.Range(0.8f, 1.5f);
        rb.velocity = Random.insideUnitCircle * tumble;
        manager = RemoteManager.GetManager();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            manager.DestroyBullet(collision.gameObject);
            hp--;
            if (hp <= 0)
                manager.DestroyAsteroidByBullet(gameObject);
        }
        else if(collision.CompareTag("Bounds"))
        {
            rb.velocity = Vector2.Perpendicular(rb.velocity) - rb.velocity;
        }
    }

}
