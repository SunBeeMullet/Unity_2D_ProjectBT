using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public int iceLv;
    public int maxIce;
    public int life;
    public int score;
    public int flavor;
    public int follower;

    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;
    public bool isHit;
    public bool isRespawnTime;

    public bool[] joyControl;
    public bool isControl;

    public GameObject bullet_0;
    public GameObject bullet_1;
    public GameObject bullet_2;
    public GameObject bullet_3;
    public GameObject bullet_4;
    public GameObject[] followers;
    public GameObject joyPanel;
    public GameObject stick;

    public GameManager gameManager;
    public ObjectManager objManager;

    public Vector2 touchPos;
    public Vector2 camTPos;
    public Vector2 joyPos;

    public Camera cam;
    
    Animator anim;
    SpriteRenderer sprRnd;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sprRnd = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void OnEnable()
    {
        iceLv = 1;
        flavor = 0;
        Invincible();
        Invoke("Invincible", 1.5f);
    }

    void Invincible()
    {
        isRespawnTime = !isRespawnTime;
        if (isRespawnTime)
        {
            sprRnd.color = new Color(1, 1, 1, 0.5f);

            for(int i = 0; i < followers.Length; i++)
            {
                followers[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            sprRnd.color = new Color(1, 1, 1, 1);

            for (int i = 0; i < followers.Length; i++)
            {
                followers[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    void Update()
    {
        Movement();
        Fire();
        Reload();
    }

    public void JoyPanel(int type)
    {
        for(int i = 0; i < 9; i++)
        {
            joyControl[i] = i == type;
        }
    }

    public void PanelDown()
    {
        if(life != 0)
        {
            touchPos = Input.mousePosition;
            touchPos = cam.ScreenToWorldPoint(touchPos);
            joyPanel.transform.position = touchPos;
            joyPanel.SetActive(true);
            isControl = true;
        }
    }

    public void PanelUp()
    {
        joyPanel.SetActive(false);
        isControl = false;
        stick.transform.position = touchPos;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointEventData = baseEventData as PointerEventData;
        Vector2 dragPos = cam.ScreenToWorldPoint(pointEventData.position);
        joyPos = (dragPos - touchPos).normalized;
        float stickDistance = Vector2.Distance(dragPos, touchPos);
        if (stickDistance < 0.6f)
        {
            stick.transform.position = touchPos + joyPos * stickDistance;
        }
        else
        {
            stick.transform.position = touchPos + joyPos * 0.6f;
        }
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }

        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl)
        {
            h = 0;
        }

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
        {
            v = 0;
        }

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }

    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
        {
            return;
        }
        switch (flavor)
        {
            case 0:
                switch (iceLv)
                {
                    case 0:
                        maxShotDelay = 0.5f;
                        GameObject bullet0 = objManager.MakeObj("PlayerBulletN0");
                        bullet0.transform.position = transform.position;
                        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
                        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                        break;
                    case 1:
                        maxShotDelay = 0.6f;
                        GameObject bullet1 = objManager.MakeObj("PlayerBulletN1");
                        bullet1.transform.position = transform.position;
                        Rigidbody2D rigidB1 = bullet1.GetComponent<Rigidbody2D>();
                        rigidB1.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                        break;
                    case 2:
                        maxShotDelay = 0.65f;
                        GameObject bullet2 = objManager.MakeObj("PlayerBulletN2");
                        bullet2.transform.position = transform.position;
                        Rigidbody2D rigidB2 = bullet2.GetComponent<Rigidbody2D>();
                        rigidB2.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                        break;
                    case 3:
                        maxShotDelay = 0.7f;
                        GameObject bullet3 = objManager.MakeObj("PlayerBulletN3");
                        bullet3.transform.position = transform.position;
                        Rigidbody2D rigidB3 = bullet3.GetComponent<Rigidbody2D>();
                        rigidB3.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                        break;
                    case 4:
                        maxShotDelay = 0.8f;
                        GameObject bullet4 = objManager.MakeObj("PlayerBulletN4");
                        bullet4.transform.position = transform.position;
                        Rigidbody2D rigidB4 = bullet4.GetComponent<Rigidbody2D>();
                        rigidB4.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                        break;
                }
                break;
            case 1:
                switch (iceLv)
                {
                    case 0:
                        maxShotDelay = 0.5f;
                        GameObject bullet0 = objManager.MakeObj("PlayerBulletN0");
                        bullet0.transform.position = transform.position;
                        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
                        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                        break;
                    case 1:
                        maxShotDelay = 0.6f;
                        GameObject bullet1 = objManager.MakeObj("PlayerBulletN1");
                        bullet1.transform.position = transform.position;
                        Rigidbody2D rigidB1 = bullet1.GetComponent<Rigidbody2D>();
                        rigidB1.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                        break;
                    case 2:
                        maxShotDelay = 0.65f;
                        GameObject bullet2 = objManager.MakeObj("PlayerBulletN2");
                        bullet2.transform.position = transform.position;
                        Rigidbody2D rigidB2 = bullet2.GetComponent<Rigidbody2D>();
                        rigidB2.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                        break;
                    case 3:
                        maxShotDelay = 0.7f;
                        GameObject bullet3 = objManager.MakeObj("PlayerBulletN3");
                        bullet3.transform.position = transform.position;
                        Rigidbody2D rigidB3 = bullet3.GetComponent<Rigidbody2D>();
                        rigidB3.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                        break;
                    case 4:
                        maxShotDelay = 0.8f;
                        GameObject bullet4 = objManager.MakeObj("PlayerBulletN4");
                        bullet4.transform.position = transform.position;
                        Rigidbody2D rigidB4 = bullet4.GetComponent<Rigidbody2D>();
                        rigidB4.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                        break;
                }
                break;
            case 2:
                switch (iceLv)
                {
                    case 0:
                        maxShotDelay = 0.5f;
                        GameObject bullet0 = objManager.MakeObj("PlayerBulletN0");
                        bullet0.transform.position = transform.position;
                        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
                        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                        break;
                    case 1:
                        maxShotDelay = 0.6f;
                        GameObject bullet1 = objManager.MakeObj("PlayerBulletN1");
                        bullet1.transform.position = transform.position;
                        Rigidbody2D rigidB1 = bullet1.GetComponent<Rigidbody2D>();
                        rigidB1.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                        break;
                    case 2:
                        maxShotDelay = 0.65f;
                        GameObject bullet2 = objManager.MakeObj("PlayerBulletN2");
                        bullet2.transform.position = transform.position;
                        Rigidbody2D rigidB2 = bullet2.GetComponent<Rigidbody2D>();
                        rigidB2.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                        break;
                    case 3:
                        maxShotDelay = 0.7f;
                        GameObject bullet3 = objManager.MakeObj("PlayerBulletN3");
                        bullet3.transform.position = transform.position;
                        Rigidbody2D rigidB3 = bullet3.GetComponent<Rigidbody2D>();
                        rigidB3.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                        break;
                    case 4:
                        maxShotDelay = 0.8f;
                        GameObject bullet4 = objManager.MakeObj("PlayerBulletN4");
                        bullet4.transform.position = transform.position;
                        Rigidbody2D rigidB4 = bullet4.GetComponent<Rigidbody2D>();
                        rigidB4.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                        break;
                }
                break;
            case 3:
                switch (iceLv)
                {
                    case 0:
                        maxShotDelay = 0.5f;
                        GameObject bullet0 = objManager.MakeObj("PlayerBulletN0");
                        bullet0.transform.position = transform.position;
                        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
                        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                        break;
                    case 1:
                        maxShotDelay = 0.6f;
                        GameObject bullet1 = objManager.MakeObj("PlayerBulletN1");
                        bullet1.transform.position = transform.position;
                        Rigidbody2D rigidB1 = bullet1.GetComponent<Rigidbody2D>();
                        rigidB1.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                        break;
                    case 2:
                        maxShotDelay = 0.65f;
                        GameObject bullet2 = objManager.MakeObj("PlayerBulletN2");
                        bullet2.transform.position = transform.position;
                        Rigidbody2D rigidB2 = bullet2.GetComponent<Rigidbody2D>();
                        rigidB2.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                        break;
                    case 3:
                        maxShotDelay = 0.7f;
                        GameObject bullet3 = objManager.MakeObj("PlayerBulletN3");
                        bullet3.transform.position = transform.position;
                        Rigidbody2D rigidB3 = bullet3.GetComponent<Rigidbody2D>();
                        rigidB3.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                        break;
                    case 4:
                        maxShotDelay = 0.8f;
                        GameObject bullet4 = objManager.MakeObj("PlayerBulletN4");
                        bullet4.transform.position = transform.position;
                        Rigidbody2D rigidB4 = bullet4.GetComponent<Rigidbody2D>();
                        rigidB4.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                        break;
                }
                break;
            case 4:
                switch (iceLv)
                {
                    case 0:
                        maxShotDelay = 0.5f;
                        GameObject bullet0 = objManager.MakeObj("PlayerBulletN0");
                        bullet0.transform.position = transform.position;
                        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
                        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                        break;
                    case 1:
                        maxShotDelay = 0.6f;
                        GameObject bullet1 = objManager.MakeObj("PlayerBulletN1");
                        bullet1.transform.position = transform.position;
                        Rigidbody2D rigidB1 = bullet1.GetComponent<Rigidbody2D>();
                        rigidB1.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                        break;
                    case 2:
                        maxShotDelay = 0.65f;
                        GameObject bullet2 = objManager.MakeObj("PlayerBulletN2");
                        bullet2.transform.position = transform.position;
                        Rigidbody2D rigidB2 = bullet2.GetComponent<Rigidbody2D>();
                        rigidB2.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                        break;
                    case 3:
                        maxShotDelay = 0.7f;
                        GameObject bullet3 = objManager.MakeObj("PlayerBulletN3");
                        bullet3.transform.position = transform.position;
                        Rigidbody2D rigidB3 = bullet3.GetComponent<Rigidbody2D>();
                        rigidB3.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                        break;
                    case 4:
                        maxShotDelay = 0.8f;
                        GameObject bullet4 = objManager.MakeObj("PlayerBulletN4");
                        bullet4.transform.position = transform.position;
                        Rigidbody2D rigidB4 = bullet4.GetComponent<Rigidbody2D>();
                        rigidB4.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                        break;
                }
                break;
            case 5:
                switch (iceLv)
                {
                    case 0:
                        maxShotDelay = 0.5f;
                        GameObject bullet0 = objManager.MakeObj("PlayerBulletN0");
                        bullet0.transform.position = transform.position;
                        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
                        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                        break;
                    case 1:
                        maxShotDelay = 0.6f;
                        GameObject bullet1 = objManager.MakeObj("PlayerBulletN1");
                        bullet1.transform.position = transform.position;
                        Rigidbody2D rigidB1 = bullet1.GetComponent<Rigidbody2D>();
                        rigidB1.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                        break;
                    case 2:
                        maxShotDelay = 0.65f;
                        GameObject bullet2 = objManager.MakeObj("PlayerBulletN2");
                        bullet2.transform.position = transform.position;
                        Rigidbody2D rigidB2 = bullet2.GetComponent<Rigidbody2D>();
                        rigidB2.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                        break;
                    case 3:
                        maxShotDelay = 0.7f;
                        GameObject bullet3 = objManager.MakeObj("PlayerBulletN3");
                        bullet3.transform.position = transform.position;
                        Rigidbody2D rigidB3 = bullet3.GetComponent<Rigidbody2D>();
                        rigidB3.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                        break;
                    case 4:
                        maxShotDelay = 0.8f;
                        GameObject bullet4 = objManager.MakeObj("PlayerBulletN4");
                        bullet4.transform.position = transform.position;
                        Rigidbody2D rigidB4 = bullet4.GetComponent<Rigidbody2D>();
                        rigidB4.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                        break;
                }
                break;
        }
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        } else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullets")
        {
            if (isHit || isRespawnTime)
            {
                return;
            }

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");
            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.PlayerCheck();
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        } else if (collision.gameObject.tag == "Boss")
        {
            if (isHit || isRespawnTime)
            {
                return;
            }

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");
            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.PlayerCheck();
            }
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Sugar":
                    score += 100;
                    break;
                case "Ice":
                    if(iceLv < maxIce)
                    {
                        iceLv++;
                        gameManager.IceChk(iceLv);
                    }
                    else
                    {
                        score += 10;
                    }
                    break;
                case "Green":
                    flavor = 1;
                    anim.SetInteger("Flavor", flavor);
                    break;
                case "Tea":
                    flavor = 2;
                    anim.SetInteger("Flavor", flavor);
                    break;
                case "Mango":
                    flavor = 3;
                    anim.SetInteger("Flavor", flavor);
                    break;
                case "Taro":
                    flavor = 4;
                    anim.SetInteger("Flavor", flavor);
                    break;
                case "Milk":
                    flavor = 5;
                    anim.SetInteger("Flavor", flavor);
                    break;
                case "Pearl":
                    follower++;
                    AddFollower();
                    break;

            }
            collision.gameObject.SetActive(false);
        }
    }

    void AddFollower()
    {
        for(int i = 0; i>3; i++)
        {
            followers[i].SetActive(false);
        }
        
        if(follower == 1)
        {
            followers[0].SetActive(true);
        }else if(follower == 2)
        {
            followers[1].SetActive(true);
        }else if (follower == 3)
        {
            followers[2].SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
