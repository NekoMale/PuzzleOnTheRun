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
    #region UnityEvent
    private void OnEnable()
    {
        GameManager.OnSelectionEvent += OnSelection;
    }
    void OnDisable()
    {
        GameManager.OnSelectionEvent -= OnSelection;
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

    void OnPlacement(int index)
    {
        slots[index].OnItemPlaced();
    }


    // Update is called once per frame
    void Update()
    {
        //foreach (KeyCode key in SlotKey.Keys)
        //{
        //    if (Input.GetKeyDown(key)){
        //        if(lastSelectedSlot != SlotKey[key])
        //        {
        //            OnDeSelectionEvent.Invoke(slots[lastSelectedSlot]);
        //        }
        //        lastSelectedSlot = SlotKey[key];
        //        Debug.Log(lastSelectedSlot);
        //        canBePlaced = OnSelectionCheck.Invoke(slots[lastSelectedSlot]);
        //    }
        //}


    }



}
