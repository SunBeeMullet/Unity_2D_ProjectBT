using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public float maxMeltDelay;
    public float curMeltDelay;

    public GameObject player;
    public GameObject thermo;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;
    public ObjectManager objManager;

    void Awake()
    {
        enemyObjs = new string[]
        {
            "EnemyS", "EnemyM", "EnemyL"
        };
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        curMeltDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3);
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

        Thermo thermoLogic = thermo.GetComponent<Thermo>();

        if (curMeltDelay > maxMeltDelay && playerLogic.iceLv > 0)
        {
            playerLogic.iceLv--;
            curMeltDelay = 0;
            IceChk(playerLogic.iceLv);
        }
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 10);
        GameObject enemy = objManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objManager = objManager;

        if (ranPoint ==  0)
        {
            enemy.transform.Rotate(Vector3.back * 330);
            rigid.velocity = new Vector2(enemyLogic.speed, -5);
        }else if (ranPoint == 5)
        {
            enemy.transform.Rotate(Vector3.forward * 330);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -5);
        }else if (ranPoint == 6 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 45);
            rigid.velocity = new Vector2(enemyLogic.speed, -1.5f);
        }else if (ranPoint == 7 || ranPoint == 9)
        {
            enemy.transform.Rotate(Vector3.back * 45);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1.5f);
        }else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }

    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index=0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void PlayerCheck()
    {
        Invoke("RespawnPlayer", 2f);
    }

    public void IceChk(int IceLv)
    {
        Thermo thermoLogic = thermo.GetComponent<Thermo>();
        thermoLogic.image.sprite = thermoLogic.sprites[IceLv];
    }

    void RespawnPlayer()
    {
        player.transform.position = Vector3.down * 3;
        player.transform.localScale = new Vector3(0.7f,0.7f,0);
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.speed += 0.5f;
        playerLogic.isHit = false;

        IceChk(playerLogic.iceLv);
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
