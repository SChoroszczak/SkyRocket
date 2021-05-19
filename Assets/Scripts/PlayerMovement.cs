using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    public float movementSpeed;
    public float slowMovementSpeed;
    public Rigidbody rb;
    public float jumpMultiplier;
    public float jumps;
    public bool slow = false;
    public Slider slowSlider;
    public GameObject SlowEffect;
    public float slowMultiplier;
    public float slowDuration;
    public bool alternativeSlowDirections = false;

    private PlayerLook playerLook;
    private Camera fpsCam;
    private float jumpCounter = 0;
    private float slowTimer = 0;
    
    //private Vector3 tempVelocity;

    //input
    private bool jump = false;
    private bool SlowButton = false;
    private bool SlowButtonUp = false;
    private bool SlowButtonDown = false;

    private void Start()
    {
        playerLook = GetComponentInChildren<PlayerLook>();
        fpsCam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        slowSlider.value = 0;
        SlowEffect.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) { jump = true; }
        if (Input.GetButtonUp("SlowButton")) { SlowButtonUp = true; }
        if (Input.GetButtonDown("SlowButton")) { SlowButtonDown = true; }
        SlowButton = Input.GetButton("SlowButton");

    }

    void FixedUpdate()
    {
        float horizInput = Input.GetAxisRaw("Horizontal"); // od 0 do 1 w zaleznosci czy idzie w przod czy w tyl, (klika W albo S)
        float verInput = Input.GetAxisRaw("Vertical");

        Vector3 forwardMovement = transform.forward * verInput;
        Vector3 rightMovement = transform.right * horizInput;
        //moving
        //transform.position += (forwardMovement + rightMovement) * movementSpeed * Time.deltaTime; //przycianane sterowanie w locie
        Vector3 moveVector = (forwardMovement + rightMovement) * movementSpeed;
        //kerunki wzgl kamery
        Vector3 slowMoveVector = (fpsCam.transform.up * verInput + fpsCam.transform.right * horizInput) * slowMovementSpeed; 
        if (!alternativeSlowDirections) { slowMoveVector = moveVector * (slowMovementSpeed / movementSpeed); }
        if (slow)
        {
            rb.MovePosition(transform.position + (slowMoveVector * Time.deltaTime)); //ZAJEBISTE, ale nie zmienia kierunku lotu
        }
        else
        {
            rb.AddForce(moveVector, ForceMode.Force); //rozpedza sie w nieskonczonosc
        }

        //jumping
        if (jump == true && jumpCounter <= jumps)
        {
         
            rb.AddForce(Vector3.up * jumpMultiplier, ForceMode.Impulse);
            jumpCounter++;
            jump = false;
        }

        //slow
        slowSlider.value = slowTimer / slowDuration;

        if (!slow && slowTimer <= slowDuration)
        {
            slowTimer += Time.fixedDeltaTime/10;
        }
        if (SlowButtonDown) //slow po kliknieciu
        {
            SlowButtonDown = false;
            //tempVelocity = rb.velocity;
            //Debug.Log(tempVelocity);
            slow = true;
            SlowEffect.SetActive(true);
            //rb.velocity = rb.velocity * slowMultiplier;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            playerLook.mouseSensivity =  playerLook.mouseSensivityOriginal / 0.4f;
        }
        else if (SlowButton && slowTimer > 0 && (horizInput == 0 && verInput == 0)) //jak nie trzyma klawiszy kierunkowych
        {
            slowTimer-=Time.deltaTime/40;
        }
        else if (SlowButton && slowTimer > 0 && (horizInput != 0 || verInput != 0))  //jak wcisnie
        {
            slowTimer -= Time.fixedDeltaTime;
        }
        else if (SlowButtonUp || slowTimer <= 0) //po ralase shift || koniec slow
        {
            SlowButtonUp = false;
            slow = false;
            SlowEffect.SetActive(false);
            //rb.velocity = tempVelocity;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F;
            playerLook.mouseSensivity = playerLook.mouseSensivityOriginal;
        }
    }
}
