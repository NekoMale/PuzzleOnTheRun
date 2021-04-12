using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnSelectionEvent;
    public static GameManager GetInstance;

    [SerializeField] List<KeyCode> slotsKeys;
    Dictionary<KeyCode, int> slotsKeyValue = new Dictionary<KeyCode, int>();
    // Start is called before the first frame update
    void OnEnable()
    {

        if (!GetInstance) GetInstance = this;
    }
    private void Start()
    {
        int counter = 0;
        foreach (KeyCode item in slotsKeys)
        {
            slotsKeyValue[item] = counter;
            counter++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode keyPressed in slotsKeyValue.Keys)
        {
            if (Input.GetKeyDown(keyPressed))
            {

                Debug.Log(keyPressed);
                OnSelectionEvent?.Invoke(slotsKeyValue[keyPressed]);
            }
        }
    }
}
