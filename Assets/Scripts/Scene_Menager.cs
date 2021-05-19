using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Menager : MonoBehaviour
{
    public bool dead = false;
    public bool playerAtTheStart = true;
    public bool playerAtTheEnd = false;
    public bool GamePaused = false;
    private float timer;
    private bool showed = false;
    private Force force;
    private PlayerMovement playerMovement;
    private PlayerLook playerLook;
    private Shooting shooting;

    public Text velocity;
    //timer ingame
    public Text min;
    public Text sec;
    public Text mili;
    public GameObject pauseScreenUI;
    public GameObject endScreenUI;
    public GameObject deathScreenUI;
    //endScreen time
    public Text endMin;
    public Text endSec;
    public Text endMili;
    //BoostLeft
    public Text boostLeft;
    
    

    void Start()
    {
        force = GameObject.FindGameObjectWithTag("Player").GetComponent<Force>(); //odwolanie do skryptu force w player
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerLook = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerLook>();
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shooting>();
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        playerMovement.enabled = true;
        playerLook.enabled = true;
        force.enabled = true;
        shooting.enabled = true;
        timer = 0;
        SetTimerText(min, sec, mili);
        SetVelocity();
        SetBoostLeft();
    }
    private void Update() //input w Update, fizyka w FixedUpdate
    {
        //reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetButtonDown("Cancel") && !playerAtTheEnd && !dead) //Esc
        {
            PauseMenu();
        }

        //boostLeft
        SetBoostLeft();
        //ekran smierci
        if (dead)
        {

            deathScreen();
        }
    }

    void FixedUpdate()
    {
        //velocity
        SetVelocity();
        //timer
        if (!playerAtTheStart && !playerAtTheEnd && !dead)
        {
            timer += Time.deltaTime;
            SetTimerText(min, sec, mili);
        }
        if (!playerAtTheStart && playerAtTheEnd && !dead)
        {
            EndScreen();
        }
        
    }

    //boost remaining
    public void SetBoostLeft()
    {
        boostLeft.text = "" + force.boostLeft;
    }
    //ogarnia licznik predkosci
    void SetVelocity()
    {
        velocity.text = (int) force.rb.velocity.magnitude + " m/s";
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
            freezeCam();
            SetTimerText(endMin, endSec, endMili);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endScreenUI.SetActive(true);
            showed = true;
        }
    }
    //ekran smierci
    void deathScreen()
    {
        deathScreenUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        freezeCam();
    }

    void freezeCam()
    {
        playerMovement.enabled = false;
        playerLook.enabled = false;
        force.enabled = false;
        shooting.enabled = false;
    }

}
