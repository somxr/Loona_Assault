using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{


    [Tooltip("In Seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX for player death")] [SerializeField] GameObject DeathFX;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        print("Controls disabled");
        SendMessage("OnPlayerDeath");

        DeathFX.SetActive(true);
        Invoke("RestartGame", levelLoadDelay);

    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
