using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;
    public float curShotDelay;
    public ObjectManager objManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    void Watch()
    {
        if (!parentPos.Contains(parent.position))
        {
            parentPos.Enqueue(parent.position);
        }

        if(parentPos.Count > followDelay)
        {
            followPos = parentPos.Dequeue();
        }
        else if(parentPos.Count < followDelay)
        {
            followPos = parent.position;
        }
    }

    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
        {
            return;
        }

        maxShotDelay = 0.5f;
        GameObject bullet0 = objManager.MakeObj("FollowerBullet");
        bullet0.transform.position = transform.position;
        Rigidbody2D rigidB0 = bullet0.GetComponent<Rigidbody2D>();
        rigidB0.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

}