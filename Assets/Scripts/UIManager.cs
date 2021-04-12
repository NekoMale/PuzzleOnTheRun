using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    Slot[] slots;
    int lastSelectedSlot = -1;

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }
    #region UnityEvent CB
    private void OnEnable()
    {
        GameManager.OnSelectionEvent += OnSelection;
        GameManager.OnPlacementEvent += OnPlacement;
        GameManager.OnResetEvent += OnReset;
    }
    void OnDisable()
    {
        GameManager.OnSelectionEvent -= OnSelection;
        GameManager.OnPlacementEvent -= OnPlacement;
        GameManager.OnResetEvent -= OnReset;
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



}
