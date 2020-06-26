using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int health;
    public int score;

    public float speed;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    Animator anim;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;

        anim = GetComponent<Animator>();
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        anim.SetInteger("Hit", 1);
        Invoke("ReturnSprite",0.3f);
        
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        anim.SetInteger("Hit", 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "OuterBorder")
        {
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "Bullets")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
    }
}
