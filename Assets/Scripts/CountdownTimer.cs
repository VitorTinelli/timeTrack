using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float timeRemaining = 30;
    private bool timerIsRunning = false;
    public Text timeText;
    public Text countdownDisplay;
    private int countdownTime = 3;
    private bool canMove = false;

    void Start()
    {
        timerIsRunning = true;
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;

        if (cenaAtual == 0)
        {
            timeRemaining = 10;
        }
        if (cenaAtual == 1)
        {
            timeRemaining = 20;
        }

        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "COMECE!";
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
        canMove = true; 
    }

    void Update()
    {
        if (timerIsRunning && canMove) 
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Tempo esgotado!");
                timeRemaining = 0;
                timerIsRunning = false;
                int cenaAtual = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(cenaAtual);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60 + 1);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canMove) 
        {
            timerIsRunning = false;
            int cenaAtual = SceneManager.GetActiveScene().buildIndex;
            if (cenaAtual == 0)
            {
                SceneManager.LoadScene(1);
            }
            if (cenaAtual == 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
