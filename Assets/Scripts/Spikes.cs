using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Spikes : MonoBehaviour
{
    
    

    [Header("Enter here the name of the game scene where this trap is located")]
    public string GameSceneName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            SceneManager.LoadScene(GameSceneName);
            
        }
    }


}
