using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float tumble;
    public GameManager manager;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * tumble;
        manager = RemoteManager.GetManager();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.DestroyAsteroidbyPlayer(gameObject);
        }
        else if (collision.CompareTag("Bounds"))
        {
            rb.velocity = Vector2.Reflect(rb.velocity,collision.transform.position).normalized*tumble;
        }
        else if(collision.CompareTag("Bullet"))
        {
            manager.DestroyBullet(collision.gameObject);
            manager.DestroyAsteroidByBullet(gameObject);
        }
        
        
    }
}

