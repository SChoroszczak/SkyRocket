using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject dropingObject;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public Transform[] spawnSpots;


    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            int randPos = Random.Range(0, spawnSpots.Length);
            Instantiate(dropingObject, spawnSpots[randPos].position, Quaternion.identity);

            timeBtwSpawns = startTimeBtwSpawns;


        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
