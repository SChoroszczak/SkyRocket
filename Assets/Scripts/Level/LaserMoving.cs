using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMoving : MonoBehaviour
{
    public float speed;
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(0, 0, speed * Time.fixedDeltaTime);
        transform.localScale += new Vector3(0, speed * Time.fixedDeltaTime, 0);
    }
}
