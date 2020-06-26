using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    
    public GameObject player;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 10);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

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

    public void PlayerCheck()
    {
        Invoke("RespawnPlayer", 2f);
    }

    void RespawnPlayer()
    {
        player.transform.position = Vector3.down * 3;
        player.SetActive(true);
    }
}
