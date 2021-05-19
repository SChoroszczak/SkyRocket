using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public float fireRate;
    public float range;
    public AudioSource shootingFX;
    public AudioSource boostUpFX;

    // public GameObject LaserRay;

    private Force force;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(3f);
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        force = GameObject.FindGameObjectWithTag("Player").GetComponent<Force>(); //odwolanie do skryptu force w player
        laserLine = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShootEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));  //convert centre of camera to world point
            RaycastHit hit;

            laserLine.SetPosition(0, firePoint.position);
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
            {
                laserLine.SetPosition(1, hit.point);

                Target target = hit.collider.GetComponent<Target>();  //jasli trafi w obiekt ze skryptem target to przypisuje
                if (target != null)  //jesli nie est pusty, czyli przypisał, czyli trafił w target
                {
                    boostUpFX.Play();
                    force.boostLeft++;
                    force.SetTimeMax();
                    target.Die();

                } 
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * range));
            }

        }
    }

    private IEnumerator ShootEffect()
    {
        shootingFX.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        // Instantiate(LaserRay, playerCam.position, Quaternion.LookRotation(playerCam.transform.up));  poprzednie z cylindrami
    }

}
