using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float timeToDestroy;
    private float timer;



    void Update()
    {
        if (timer > timeToDestroy)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
