using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay_Player : MonoBehaviour
{
    public float fadingTime;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time >= fadingTime)
        {
            Destroy(gameObject);
        }
        else
        {
            time+=Time.fixedDeltaTime;
        }
    }
}
