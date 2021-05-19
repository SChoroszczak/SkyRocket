using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    float wpRadius = 10;

    void Update()
    {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < wpRadius)
        {
            current++;
            if (current >= waypoints.Length) //wraca po pirwszego po ostatnim WP
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
