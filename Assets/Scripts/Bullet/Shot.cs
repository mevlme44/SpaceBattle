using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private GameManager manager;
    private readonly float speed = 20.0f;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        manager = RemoteManager.GetManager();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bounds"))
            manager.DestroyBullet(gameObject);
    }
}
