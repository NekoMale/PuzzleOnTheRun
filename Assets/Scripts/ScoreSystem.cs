using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{


    
    GameManager gm;

    private void Start()
    {
        
        if (gm==null) gm = GameManager.GetInstance;
    }
    // Start is called before the first frame update
    void IncreasePoint(int value) => gm.Score += value;

    void IncreaseLivesSpent() => gm.Lives++;

}
