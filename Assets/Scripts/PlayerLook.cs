using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    
    public float mouseSensivity;
    public float mouseSensivityOriginal;
    public float fovBoost;

    public Transform playerBody;
    private Force player;

    private float xAxisClamp;

    private void Awake()
    {
        mouseSensivity = mouseSensivityOriginal;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Force>();
        LockCursor();
        xAxisClamp = 0.0f;
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Camera.main.fieldOfView < 89 + player.rb.velocity.magnitude * fovBoost)
        {
            Camera.main.fieldOfView++;
        }
        else if (Camera.main.fieldOfView > 91 + player.rb.velocity.magnitude * fovBoost)
        {
            Camera.main.fieldOfView--;
        }
        //Camera.main.fieldOfView = 90 + player.rb.velocity.magnitude * fovBoost; 
    }
    private void FixedUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        //pobranie wartosci ruchu kursora
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensivity * Time.deltaTime;

        //lock obracania kamery
        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0;
            ClampXAxisRotationToValue(90.0f);    
        }
        //obrot
        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 euerRotation = transform.eulerAngles;
        euerRotation.x = value;
        transform.eulerAngles = euerRotation;
    }
}
