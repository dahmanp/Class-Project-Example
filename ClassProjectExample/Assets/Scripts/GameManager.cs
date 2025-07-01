using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public PlayerController player;
    public float time;
    private bool timeActive;

    [Header("Game UI")]
    public TMP_Text gameUI_score;
    public TMP_Text gameUI_health;
    public TMP_Text gameUI_time;

    [Header("Countdown UI")]
    public TMP_Text countdownText;
    public int countdown;

    [Header("End Screen UI")]
    public TMP_Text endUI_score;
    public TMP_Text endUI_time;

    [Header("Screens")]
    public GameObject countdownUI;
    public GameObject gameUI;
    public GameObject endUI;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        // make sure timer is set to 0
        time = 0;

        // disable player movement initially
        player.enabled = false;

        // set the screen to show the countdown
        SetScreen(countdownUI);

        // start countdown
        StartCoroutine(CountdownRoutine());
    }

    void Update()
    {
        // keep track of the time that goes by
        if (timeActive)
        {
            time = time + Time.deltaTime;
        }

        // set the UI to display your stats
        gameUI_score.text = "Coins: " + player.coinCount;
        gameUI_health.text = "Health: " + player.health;
        gameUI_time.text = "Time: " + (time * 10).ToString("F2");
    }

    IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        // enable player movement
        player.enabled = true;

        // start the game
        startGame();
    }

    void endGame()
    {
        // end the timer
        timeActive = false;

        // disable player movement
        player.enabled = false;

        // set the UI to display your stats
        endUI_score.text = "Score: " + player.coinCount;
        endUI_time.text = "Time: " + (time * 10).ToString("F2");
    }

    void startGame()
    {
        // set the screen to see your stats in game
        SetScreen(gameUI);

        // start timer
        timeActive = true;
    }

    public void OnRestartButton()
    {
        // restart the scene to play again
        SceneManager.LoadScene(0);
    }

    void SetScreen(GameObject screen)
    {
        // disable all other screens
        countdownUI.SetActive(false);
        gameUI.SetActive(false);
        endUI.SetActive(false);
        // activate the requested screen
        screen.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            // end the game
            endGame();

            // set the end screen
            SetScreen(endUI);
        }
    }
}
