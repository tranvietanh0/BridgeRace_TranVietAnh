using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guner : MonoBehaviour
{
    public Transform[] bulletPoints;

    public Bullet bulletPrefab;

    public float rateFire = 1f;

    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (time > rateFire)
        {
            time -= rateFire;

            Fire();
        }

        time += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    //de dung duoc pool thi object phai ke thua gameunit

    public void Fire()
    {
        for (int i = 0; i < bulletPoints.Length; i++)
        {
            //cach 1 lay prefab truc tiep
            //SimplePool.Spawn(bulletPrefab, bulletPoints[i].position, bulletPoints[i].rotation).OnInit();
            //cach 2 lay theo pool type voi dieu kien object do phat de trong folder Resources/Pool
            SimplePool.Spawn<Bullet>(PoolType.Bullet_2 , bulletPoints[i].position, bulletPoints[i].rotation).OnInit();
        }
    }
}
