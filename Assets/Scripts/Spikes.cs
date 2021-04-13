using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Spikes : MonoBehaviour
{
    string GameSceneName;

    private void Start()
    {
        GameSceneName = SceneManager.GetActiveScene().name;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }


}
