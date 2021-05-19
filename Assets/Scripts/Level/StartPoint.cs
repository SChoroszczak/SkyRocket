using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Scene_Menager sceneMenager;

    void Start()
    {
        //getting refference to SceneMenager
        sceneMenager = GameObject.FindGameObjectWithTag("SceneMenager").GetComponent<Scene_Menager>();
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            sceneMenager.playerAtTheStart = false;
        }
    }
}
