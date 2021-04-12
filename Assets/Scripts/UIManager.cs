using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    Slot[] slots;
    int lastSelectedSlot = -1;
    [Header("SCORE & LIVES")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] List<int> slotsValue;
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        RestartUI();
    }

    void RestartUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ItemValue = slotsValue[i];
        }
    }

    #region UnityEvent CB
    private void OnEnable()
    {
        GameManager.OnSelectionEvent += OnSelection;
        GameManager.OnPlacementEvent += OnPlacement;
        GameManager.OnResetEvent += OnReset;
        GameManager.OnPointIncrease += IncreasePoints;
        GameManager.OnLivesIncrease += IncreaseLives;
    }
    void OnDisable()
    {
        GameManager.OnSelectionEvent -= OnSelection;
        GameManager.OnPlacementEvent -= OnPlacement;
        GameManager.OnResetEvent -= OnReset;
        GameManager.OnPointIncrease -= IncreasePoints;
        GameManager.OnLivesIncrease -= IncreaseLives;
    }
    #endregion

    void OnSelection(int index)
    {
        if (index < slots.Length)
        {
            if (lastSelectedSlot != index && lastSelectedSlot >= 0)
            {
                slots[lastSelectedSlot].OnDeselection();
            }
            lastSelectedSlot = index;
            slots[index].OnItemSelection();
        }
    }

    bool OnPlacement(int index)
    {
        if (index < slots.Length)
            return slots[index].OnItemPlaced();
        else return false;
    }

    void OnReset(int index)
    {
        if (index < slots.Length)
            slots[index].OnDeselection();
    }

    void IncreasePoints(int value)
    {
        scoreText.text = "Score: " + value.ToString();
    }

    void IncreaseLives(int value)
    {
        livesText.text = "Lives: " + value.ToString();

    }



}
