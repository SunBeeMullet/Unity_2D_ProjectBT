using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public int health;
    public int score;

    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bullet_0;
    public GameObject bullet_1;
    public GameObject player;

    SpriteRenderer spriteRenderer;

    Animator anim;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
        {
            return;
        }

        if(enemyName == "M")
        {
            GameObject bullet = Instantiate(bullet_0, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletL = Instantiate(bullet_1, transform.position + Vector3.left * 0.3f, transform.rotation);
            GameObject bulletR = Instantiate(bullet_1, transform.position + Vector3.right * 0.3f, transform.rotation);
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            rigidL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
            rigidR.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
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
