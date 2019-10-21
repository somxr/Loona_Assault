using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("startGame", 4f);
    }

    private void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
