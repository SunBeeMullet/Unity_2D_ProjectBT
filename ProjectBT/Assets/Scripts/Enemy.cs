using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public int health;
    public int enemyScore;

    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bullet_0;
    public GameObject bullet_1;
    public GameObject player;

    //public GameObject[] dropItem;

    SpriteRenderer spriteRenderer;
    ObjectManager objManager;

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
            GameObject bullet = objManager.MakeObj("EnemyBullet0");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletL = objManager.MakeObj("EnemyBullet1");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            GameObject bulletR = objManager.MakeObj("EnemyBullet1");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
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
        if(health <= 0)
        {
            return;
        }

        health -= dmg;
        anim.SetInteger("Hit", 1);
        Invoke("ReturnSprite",0.3f);
        
        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            int ran = Random.Range(0, 10);
            int ranf = Random.Range(0, 5);
            if(ran < 5)
            {
                Debug.Log("50% : No Item Drop");
            }else if(5 <= ran && ran < 8)
            {
                GameObject itemSugar = objManager.MakeObj("itemSugar");
                itemSugar.transform.position = transform.position;
                //Instantiate(dropItem[6], transform.position, dropItem[6].transform.rotation);
            }else if(8 == ran)
            {
                GameObject itemIce = objManager.MakeObj("itemIce");
                itemIce.transform.position = transform.position;
                //Instantiate(dropItem[5], transform.position, dropItem[5].transform.rotation);
            }
            else if(9 == ran)
            {
                switch (ranf)
                {
                    case 0:
                        GameObject itemGreenTea = objManager.MakeObj("itemGreenTea");
                        itemGreenTea.transform.position = transform.position;
                        break;
                    case 1:
                        GameObject itemMango = objManager.MakeObj("itemMango");
                        itemMango.transform.position = transform.position;
                        break;
                    case 2:
                        GameObject itemMilk = objManager.MakeObj("itemMilk");
                        itemMilk.transform.position = transform.position;
                        break;
                    case 3:
                        GameObject itemTaro = objManager.MakeObj("itemTaro");
                        itemTaro.transform.position = transform.position;
                        break;
                    case 4:
                        GameObject itemTeaBag = objManager.MakeObj("itemTeaBag");
                        itemTeaBag.transform.position = transform.position;
                        break;
                }
                
                //Instantiate(dropItem[ranf], transform.position, dropItem[ranf].transform.rotation);
            }

            gameObject.SetActive(false);
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
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Bullets")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
        }
    }
}
