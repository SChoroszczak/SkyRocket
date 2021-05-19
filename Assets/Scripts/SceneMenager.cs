using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenager : MonoBehaviour
{

    public bool playerAtTheStart = true;
    public bool playerAtTheEnd = false;
    public bool GamePaused = false;
    private float timer;
    private bool showed = false;
    private Force player;

    public Text velocity;
    //timer ingame
    public Text min;
    public Text sec;
    public Text mili;
    public GameObject pauseScreenUI;
    public GameObject endScreenUI;
    //endScreen time
    public Text endMin;
    public Text endSec;
    public Text endMili;
    

    void Start()
    {
        Time.timeScale = 1f;   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Force>(); //odwolanie do skryptu force w player
        timer = 0;
        SetTimerText(min, sec, mili);
        SetVelocity();
    }
    private void Update() //input w Update, fizyka w FixedUpdate
    {
        //reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetButtonDown("Cancel") && !playerAtTheEnd) //Esc
        {
            PauseMenu();
        }

    }

    void FixedUpdate()
    {
        //velocity
        SetVelocity();
        //timer
        if (!playerAtTheStart && !playerAtTheEnd)
        {
            timer += Time.fixedDeltaTime;
            SetTimerText(min, sec, mili);
        }
        if (!playerAtTheStart && playerAtTheEnd)
        {
            EndScreen();
        }

        
    }

    

    //ogarnia licznik predkosci
    void SetVelocity()
    {
        velocity.text = (int) player.rb.velocity.magnitude + " m/s";
    }

    //ogarnia timer, podaj pola na ktorych ma wyswietlac
    void SetTimerText(Text minT, Text secT, Text miliT)
    {
        int mins = 00;
        int secs = 00;
        int milis = 00;
        mins = (int) timer / 60;
        secs = (int) timer - mins * 60;
        milis = (int)((timer - (mins * 60 + secs)) * 100);

        if (mins < 10) { minT.text = "0" + mins.ToString(); }
        else { minT.text = mins.ToString(); }
        if (secs < 10) { secT.text = ": 0" + secs.ToString(); }
        else { secT.text = ": " + secs.ToString(); }
        if (milis < 10) { miliT.text = ": 0" + milis.ToString(); }
        else { miliT.text = ": " + milis.ToString(); }
    }


    public void MainMenu()
    {
        StartCoroutine(LoadLevelAsync(0));
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevelAsync(int nr)
    {
        AsyncOperation loadLevelAsync = SceneManager.LoadSceneAsync(nr);

        //wait until loaded
        while (!loadLevelAsync.isDone)
        {
            yield return null;
        }
    }

    private void PauseMenu()
    {
        if (GamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    private void EndScreen()
    {
        
        if (!showed)
        {
            SetTimerText(endMin, endSec, endMili);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endScreenUI.SetActive(true);
            showed = true;
        }
    }
    
}
