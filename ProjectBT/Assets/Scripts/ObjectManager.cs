using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemIcePrefab;
    public GameObject itemSugarPrefab;
    public GameObject itemGreenTeaPrefab;
    public GameObject itemMangoPrefab;
    public GameObject itemTaroPrefab;
    public GameObject itemTeaBagPrefab;
    public GameObject itemMilkPrefab;
    public GameObject bulletEnemy0Prefab;
    public GameObject bulletEnemy1Prefab;
    public GameObject bulletPlayerN0Prefab;
    public GameObject bulletPlayerN1Prefab;
    public GameObject bulletPlayerN2Prefab;
    public GameObject bulletPlayerN3Prefab;
    public GameObject bulletPlayerN4Prefab;


    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemIce;
    GameObject[] itemSugar;
    GameObject[] itemGreenTea;
    GameObject[] itemMango;
    GameObject[] itemTaro;
    GameObject[] itemTeaBag;
    GameObject[] itemMilk;

    GameObject[] bulletEnemy0;
    GameObject[] bulletEnemy1;
    GameObject[] bulletPlayerN0;
    GameObject[] bulletPlayerN1;
    GameObject[] bulletPlayerN2;
    GameObject[] bulletPlayerN3;
    GameObject[] bulletPlayerN4;

    GameObject[] targetPool;

    void Awake()
    {
        enemyL = new GameObject[20];
        enemyM = new GameObject[20];
        enemyS = new GameObject[30];

        itemIce = new GameObject[20];
        itemSugar = new GameObject[20];
        itemGreenTea = new GameObject[10];
        itemMango = new GameObject[10];
        itemTaro = new GameObject[10];
        itemTeaBag = new GameObject[10];
        itemMilk = new GameObject[10];

        bulletEnemy0 = new GameObject[100];
        bulletEnemy1 = new GameObject[100];
        bulletPlayerN0 = new GameObject[100];
        bulletPlayerN1 = new GameObject[100];
        bulletPlayerN2 = new GameObject[100];
        bulletPlayerN3 = new GameObject[100];
        bulletPlayerN4 = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        for(int index=0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }

        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }

        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        for (int index = 0; index < itemIce.Length; index++)
        {
            itemIce[index] = Instantiate(itemIcePrefab);
            itemIce[index].SetActive(false);
        }

        for (int index = 0; index < itemSugar.Length; index++)
        {
            itemSugar[index] = Instantiate(itemSugarPrefab);
            itemSugar[index].SetActive(false);
        }

        for (int index = 0; index < itemGreenTea.Length; index++)
        {
            itemGreenTea[index] = Instantiate(itemGreenTeaPrefab);
            itemGreenTea[index].SetActive(false);
        }

        for (int index = 0; index < itemMango.Length; index++)
        {
            itemMango[index] = Instantiate(itemMangoPrefab);
            itemMango[index].SetActive(false);
        }

        for (int index = 0; index < itemTaro.Length; index++)
        {
            itemTaro[index] = Instantiate(itemTaroPrefab);
            itemTaro[index].SetActive(false);
        }

        for (int index = 0; index < itemTeaBag.Length; index++)
        {
            itemTeaBag[index] = Instantiate(itemTeaBagPrefab);
            itemTeaBag[index].SetActive(false);
        }

        for (int index = 0; index < itemMilk.Length; index++)
        {
            itemMilk[index] = Instantiate(itemMilkPrefab);
            itemMilk[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemy0.Length; index++)
        {
            bulletEnemy0[index] = Instantiate(bulletEnemy0Prefab);
            bulletEnemy0[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemy1.Length; index++)
        {
            bulletEnemy1[index] = Instantiate(bulletEnemy1Prefab);
            bulletEnemy1[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerN0.Length; index++)
        {
            bulletPlayerN0[index] = Instantiate(bulletPlayerN0Prefab);
            bulletPlayerN0[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerN1.Length; index++)
        {
            bulletPlayerN1[index] = Instantiate(bulletPlayerN1Prefab);
            bulletPlayerN1[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerN2.Length; index++)
        {
            bulletPlayerN2[index] = Instantiate(bulletPlayerN2Prefab);
            bulletPlayerN2[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerN3.Length; index++)
        {
            bulletPlayerN3[index] = Instantiate(bulletPlayerN3Prefab);
            bulletPlayerN3[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerN4.Length; index++)
        {
            bulletPlayerN4[index] = Instantiate(bulletPlayerN4Prefab);
            bulletPlayerN4[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemIce":
                targetPool = itemIce;
                break;
            case "ItemSugar":
                targetPool = itemSugar;
                break;
            case "ItemGreenTea":
                targetPool = itemGreenTea;
                break;
            case "ItemMango":
                targetPool = itemMango;
                break;
            case "ItemTaro":
                targetPool = itemTaro;
                break;
            case "ItemTeaBag":
                targetPool = itemTeaBag;
                break;
            case "ItemMilk":
                targetPool = itemMilk;
                break;
            case "EnemyBullet0":
                targetPool = bulletEnemy0;
                break;
            case "EnemyBullet1":
                targetPool = bulletEnemy1;
                break;
            case "PlayerBulletN0":
                targetPool = bulletPlayerN0;
                break;
            case "PlayerBulletN1":
                targetPool = bulletPlayerN1;
                break;
            case "PlayerBulletN2":
                targetPool = bulletPlayerN2;
                break;
            case "PlayerBulletN3":
                targetPool = bulletPlayerN3;
                break;
            case "PlayerBulletN4":
                targetPool = bulletPlayerN4;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}
