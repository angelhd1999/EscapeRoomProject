using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //Singleton

    public Text scoreText;
    public Text timer;
    public Text finalScore;
    public Text yesGesture;
    public Text noGesture;
    public Text retryQuestion;
    public float gameTime;
    public GameObject head;

    private int score = 0;
    private bool gameFinished = false;
    private float seconds;
    private float minutes;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameFinished) return;
        UpdateTimer();
    }
    public void AddScore(int points)
    {
        if (gameFinished) return;
        score += points;
        scoreText.text = "Score: " + score;
    }

    void UpdateTimer()
    {
        gameTime -= Time.deltaTime;
        minutes = Mathf.Floor(gameTime / 60);
        seconds = gameTime % 60;
        if (seconds > 59) seconds = 59;
        //timer.text = "Time: " + gameTime.ToString("F0");
        timer.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        if (gameTime <= 0.0f)
        {
            GameFinished();
        }
    }

    void GameFinished()
    {
        gameFinished = true;
        head.GetComponent<RetryGesture>().canRetry = true;
        timer.text = "Time: 00:00";
        finalScore.text = "Your final score is: " + score;
        yesGesture.text = "Nod to restart";
        noGesture.text = "Shake your head to quit the game";
        retryQuestion.text = "Retry?";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
