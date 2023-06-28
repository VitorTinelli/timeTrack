using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 

public class Player : MonoBehaviour
{
    // Contador
    private bool canMove = false;
    private int countdownTime = 3;

    Rigidbody rb;

    void Start()
    {
        StartCoroutine(CountdownToStart());
        rb = GetComponent<Rigidbody>();
    }

    void Update() 
    {
    }
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            Console.WriteLine(canMove);
        }
        canMove = true;
        rb.constraints = RigidbodyConstraints.None;
    }
}