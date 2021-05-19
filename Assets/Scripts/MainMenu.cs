using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // funkcje dla przycisków
    public void SelectLevel1() { PlayGame(1); }
    public void SelectLevel2() { PlayGame(2); }
    public void SelectLevel3() { PlayGame(3); }
    public void SelectLevel4() { PlayGame(4); }
    public void SelectLevel5() { PlayGame(5); }
    public void SelectLevel6() { PlayGame(6); }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }


    public void PlayGame(int nr)
    {
        SceneManager.LoadScene(nr);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
