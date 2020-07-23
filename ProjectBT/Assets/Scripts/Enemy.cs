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

    public GameObject player;
    public ObjectManager objManager;

    //public GameObject[] dropItem;

    SpriteRenderer spriteRenderer;
    
    Animator anim;

    public int patternIndex;
    public int curPatternCnt;
    public int[] maxPatternCnt;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                health = 30;
                break;
            case "M":
                health = 15;
                break;
            case "S":
                health = 5;
                break;
            case "B":
                health = 100;
                Invoke("Stop",4);
                break;
        }
    }

    void Stop()
    {
        if(!gameObject.activeSelf)
        {
            return;
        }
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCnt = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireSpread();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        GameObject bulletL1 = objManager.MakeObj("EnemyBullet2");
        bulletL1.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletL2 = objManager.MakeObj("EnemyBullet2");
        bulletL2.transform.position = transform.position + Vector3.left * 0.6f;
        GameObject bulletR1 = objManager.MakeObj("EnemyBullet2");
        bulletR1.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletR2 = objManager.MakeObj("EnemyBullet2");
        bulletR2.transform.position = transform.position + Vector3.right * 0.6f;

        Rigidbody2D rigidL1 = bulletL1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR1 = bulletR1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL2 = bulletL2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR2 = bulletR2.GetComponent<Rigidbody2D>();

        rigidL1.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidR1.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidL2.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidR2.AddForce(Vector2.down * 10, ForceMode2D.Impulse);

        curPatternCnt++;

        if(curPatternCnt < maxPatternCnt[patternIndex])
        {
            Invoke("FireForward", 2);
        }
        else
        {
            Invoke("Think", 3);
        }

    }

    void FireSpread()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject bullet = objManager.MakeObj("EnemyBullet3");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse);
        }
        
        curPatternCnt++; 

        if (curPatternCnt < maxPatternCnt[patternIndex])
        {
            Invoke("FireSpread", 3.5f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireArc()
    {
        GameObject bullet = objManager.MakeObj("EnemyBullet1");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(curPatternCnt), -1);
        rigid.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse);

        curPatternCnt++;

        if (curPatternCnt < maxPatternCnt[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireAround()
    {
        Debug.Log("FireAround");

        curPatternCnt++;

        if (curPatternCnt < maxPatternCnt[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void Update()
    {
        curPatternCnt++;

        if (enemyName == "B")
        {
            return;
        }
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
        //왜 만들었는지 잘 모르겠음
        //if(health <= 0)
        //{
        //    return;
        //}

        health -= dmg;

        anim.SetInteger("Hit", 1);
        Invoke("ReturnSprite", 0.3f);
        
        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            int ran = enemyName == "B" ? 0 : Random.Range(0, 10);
            int ranf = Random.Range(0, 5);
            if (ran < 3)
            {
                Debug.Log("No Item Drop");
            } else if (3 <= ran && ran < 5) {
                GameObject itemSugar = objManager.MakeObj("ItemPearl");
                itemSugar.transform.position = transform.position;
            } else if (5 <= ran && ran < 7)
            {
                GameObject itemSugar = objManager.MakeObj("ItemSugar");
                itemSugar.transform.position = transform.position;
                //Rigidbody2D rigid = itemSugar.GetComponent<Rigidbody2D>();
                //rigid.velocity = Vector2.down * 2;
                //Instantiate(dropItem[6], transform.position, dropItem[6].transform.rotation);
            }
            else if (7 <= ran && ran < 9)
            {
                GameObject itemIce = objManager.MakeObj("ItemIce");
                itemIce.transform.position = transform.position;
                //Rigidbody2D rigid = itemIce.GetComponent<Rigidbody2D>();
                //rigid.velocity = Vector2.down * 2;
                //Instantiate(dropItem[5], transform.position, dropItem[5].transform.rotation);
            }
            else if (9 == ran)
            {
                switch (ranf)
                {
                    case 0:
                        GameObject itemGreenTea = objManager.MakeObj("ItemGreenTea");
                        itemGreenTea.transform.position = transform.position;
                        //Rigidbody2D rigid0 = itemGreenTea.GetComponent<Rigidbody2D>();
                        //rigid0.velocity = Vector2.down * 2;
                        break;
                    case 1:
                        GameObject itemMango = objManager.MakeObj("ItemMango");
                        itemMango.transform.position = transform.position;
                        //Rigidbody2D rigid1 = itemMango.GetComponent<Rigidbody2D>();
                        //rigid1.velocity = Vector2.down * 2;
                        break;
                    case 2:
                        GameObject itemMilk = objManager.MakeObj("ItemMilk");
                        itemMilk.transform.position = transform.position;
                        //Rigidbody2D rigid2 = itemMilk.GetComponent<Rigidbody2D>();
                        //rigid2.velocity = Vector2.down * 2;
                        break;
                    case 3:
                        GameObject itemTaro = objManager.MakeObj("ItemTaro");
                        itemTaro.transform.position = transform.position;
                        //Rigidbody2D rigid3 = itemTaro.GetComponent<Rigidbody2D>();
                        //rigid3.velocity = Vector2.down * 2;
                        break;
                    case 4:
                        GameObject itemTeaBag = objManager.MakeObj("ItemTeaBag");
                        itemTeaBag.transform.position = transform.position;
                        //Rigidbody2D rigid4 = itemTeaBag.GetComponent<Rigidbody2D>();
                        //rigid4.velocity = Vector2.down * 2;
                        break;
                }

                //Instantiate(dropItem[ranf], transform.position, dropItem[ranf].transform.rotation);
            }

            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
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
            transform.rotation = Quaternion.identity;
        }
        else if(collision.gameObject.tag == "Bullets")
        {
            collision.gameObject.SetActive(false);
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
        }
    }
}
