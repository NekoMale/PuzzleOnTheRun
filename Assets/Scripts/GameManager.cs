using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Events
    public static event Action<int> OnSelectionEvent,OnResetEvent;
    public static event Func<int, bool> OnPlacementEvent;
    #endregion

    public static GameManager GetInstance;

    [Header("INPUT")]
    [SerializeField] List<KeyCode> slotsKeys;

    [Header("GUI")]
    [SerializeField] Sprite defaultCursor;



    Dictionary<KeyCode, int> slotsKeyValue = new Dictionary<KeyCode, int>();
    KeyCode key;


    void OnEnable()
    {
        //qualora ci servissero cose del gamemanager
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
                key = keyPressed;
                OnSelectionEvent?.Invoke(slotsKeyValue[keyPressed]);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //If object to spawn is available
            if (OnPlacementEvent.Invoke(slotsKeyValue[key]))
            {       
                //SPAWNA OGGETTO
            }
            else
            {
                OnResetEvent.Invoke(slotsKeyValue[key]);
                Cursor.SetCursor(defaultCursor.texture, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }
}
