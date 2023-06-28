using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float timeRemaining = 30;
    private bool timerIsRunning = false;
    private int voltas = 0;
    private int voltas2 = 0;
    public Text timeText;
    public Text countdownDisplay;
    public Text volta;
    private int countdownTime = 3;
    private bool canMove = false;
    Collider colliderF;
    private int voltaV;

    void Start()
    {
        Debug.Log(voltas);
        Debug.Log(voltaV);
        colliderF = GetComponent<BoxCollider>();
        if(voltas < 3)
        {
            colliderF.isTrigger = true;
        }
        else if(voltas >= 3) 
        {
            colliderF.isTrigger = false;
        }
        
        timerIsRunning = true;

        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        if (cenaAtual == 1)
        {
            timeRemaining = 120;
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

        countdownDisplay.text = "GO!";
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
        float sec = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canMove && VoltaV.voltaV == 1)
        {
            voltas++;
            VoltaV.voltaV = 0;
            voltas2++;
            volta.text = (voltas2).ToString();
            int cenaAtual = SceneManager.GetActiveScene().buildIndex;
            if ((cenaAtual == 1) && voltas >= 3)
            {
                timerIsRunning = false;
                SceneManager.LoadScene(0);
            }
            Debug.Log(voltas);
            Debug.Log(voltaV);
        }
    }
}
