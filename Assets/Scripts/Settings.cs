using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private PlayerLook playerLook;

    private static float mouseSensitivity = 10;



    // Start is called before the first frame update
    void Start()
    {
        playerLook = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLook>();

        //playerLook.mouseSensivity = mouseSensitivity;

    }


}
