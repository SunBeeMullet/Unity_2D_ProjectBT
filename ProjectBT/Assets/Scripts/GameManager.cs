using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;
    public float maxMeltDelay;
    public float curMeltDelay;

    public GameObject player;
    public GameObject thermo;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;
    public ObjectManager objManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[]
        {
            "EnemyS", "EnemyM", "EnemyL", "EnemyB"
        };
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage_0") as TextAsset;
        StringReader strReader = new StringReader(textFile.text);
        Debug.Log("File Read?");
        
        while(strReader != null)
        {
            string line = strReader.ReadLine();
            Debug.Log(line);

            if(line == null)
            {
                break;
            }

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);

            spawnList.Add(spawnData);
        }

        strReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        Player playerLogic = player.GetComponent<Player>();

        curSpawnDelay += Time.deltaTime;
        curMeltDelay += Time.deltaTime;
    
        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            //nextSpawnDelay = Random.Range(0.5f, 3);
            curSpawnDelay = 0;
        }

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
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }

        //int ranEnemy = Random.Range(0, 3);
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objManager = objManager;

        if (enemyPoint ==  0)
        {
            enemy.transform.Rotate(Vector3.back * 330);
            rigid.velocity = new Vector2(enemyLogic.speed, -5);
        }else if (enemyPoint == 5)
        {
            enemy.transform.Rotate(Vector3.forward * 330);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -5);
        }else if (enemyPoint == 6 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 45);
            rigid.velocity = new Vector2(enemyLogic.speed, -1.5f);
        }else if (enemyPoint == 7 || enemyPoint == 9)
        {
            enemy.transform.Rotate(Vector3.back * 45);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1.5f);
        }else if (enemyPoint == 10)
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
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
        playerLogic.follower = 0;
        curMeltDelay = 0;
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
