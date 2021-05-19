using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Force : MonoBehaviour
{

    public Transform playerCam;
    public Rigidbody rb;
    public Slider power;
    public int boostLeft;
    public AudioSource jumpFX;

    public float multiplier;
    public float timeBTWBoost;
    public bool advancedPhysics = false;

    private float time;

    //input
    private bool Fire1 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        power.value = 0;
        time = 0;
    }
    void Update()  //input w zwykłym Update, bo nie wykrywa w FixedUpdate
    {
        Fire1 = Input.GetButton("Fire2");

        if (Input.GetKeyDown(KeyCode.P)) { advancedPhysics = !advancedPhysics; Debug.Log("AdwPhysic: " + advancedPhysics); }
    }

    void FixedUpdate()  //fizyka w FixedUpdate
    {
            power.value = time / timeBTWBoost;

        if (Fire1 && time == timeBTWBoost && boostLeft > 0)
        {
            Vector3 kierunek = playerCam.transform.forward;
            jumpFX.Play();
            if (!advancedPhysics)
            {
                rb.AddForce(-rb.velocity + kierunek * multiplier, ForceMode.Impulse);
                boostLeft--;
            }
            else
            {
                rb.AddForce(kierunek * multiplier, ForceMode.Impulse);
                boostLeft--;
            }
            time = 0;
        }
        else if(time < timeBTWBoost && boostLeft > 0)
        {
            time += Time.fixedDeltaTime;
            if (time > timeBTWBoost)
            {
                time = timeBTWBoost;
            }
        }
    }
    public void SetTimeMax()
    {
        time = timeBTWBoost;
    }

}
