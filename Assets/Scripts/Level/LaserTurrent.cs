using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurrent : MonoBehaviour
{
    public GameObject spawningObject;
    public Transform target;
    public Transform firepoint;
    public Transform tracker;
    public GameObject pointer;
    public float lastWarningTime;
    public GameObject lastWarningPointer;
    public float rotationSpeed;
    public float startTimeBtwSpawns;

    private float timeBtwSpawns;
    private bool inRange = false;
    private bool coroutineStarted;

    // Zamien tracker na LineRenderer/raycast i mierz jak dlugi ma byc promien lasera

    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }
    void FixedUpdate()
    {
        if (inRange)
        {
            tracker.transform.LookAt(target);
            firepoint.rotation = Quaternion.Slerp(firepoint.rotation, tracker.rotation, rotationSpeed);

            
            if (timeBtwSpawns <= 0)
            {
                Instantiate(spawningObject, firepoint.position, firepoint.rotation);
                timeBtwSpawns = startTimeBtwSpawns;
                //zmiana na normalny pointer

            }
            else
            {
                timeBtwSpawns -= Time.fixedDeltaTime;

                if (timeBtwSpawns <= lastWarningTime && !coroutineStarted)
                {
                    StartCoroutine(PointerWarningEffect());
                }
            }
        }
        else
        {
           
            tracker.Rotate(0, 1, 0);
        }
        


    }

    private IEnumerator PointerWarningEffect()
    {
        coroutineStarted = true;
        for(int i =0; i < 2; i++)
        {
        lastWarningPointer.SetActive(true);
        pointer.SetActive(false);
        yield return new WaitForSeconds(lastWarningTime / 4);
        pointer.SetActive(true);
        lastWarningPointer.SetActive(false);
        yield return new WaitForSeconds(lastWarningTime / 4);
        }
        coroutineStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        timeBtwSpawns = startTimeBtwSpawns;
    }
}
