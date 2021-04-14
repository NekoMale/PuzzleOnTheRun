using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Events
    public static event Action<int> OnSelectionEvent, OnResetEvent;
    public static event Func<int, bool> OnPlacementEvent;
    public static event Action<int> OnPointIncrease;
    public static event Action<int> OnPointDecrease;
    public static event Action<int> OnLivesIncrease;
    public static event Func<int, GameObject> OnSpawningEvent;
    #endregion

    int _lives = 0;
    int _score = 0;

    public static GameManager GetInstance;

    [Header("INPUT")]
    [SerializeField] List<KeyCode> slotsKeys;

    [Header("GUI")]
    [SerializeField] Sprite defaultCursor;
    [SerializeField] GameObject ps;
    [SerializeField] GameObject test;





    Dictionary<KeyCode, int> slotsKeyValue = new Dictionary<KeyCode, int>();
    KeyCode key;

    public int Lives
    {
        get
        {
            return _lives;

        }
        set
        {
            _lives = value;
            OnLivesIncrease.Invoke(_lives);
        }
    }
    public int Score
    {
        get
        {
            return _score;

        }
        set
        {
            _score = value;
            OnPointIncrease.Invoke(_score);
        }
    }



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

    void InstantiateObj(GameObject obj)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        GameObject go =Instantiate(obj);
        go.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

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


                InstantiateObj(ps);
                //SPAWNA OGGETTO
                GameObject spawn = OnSpawningEvent.Invoke(slotsKeyValue[key]);
                InstantiateObj(spawn);


            }
            else
            {
                OnResetEvent?.Invoke(slotsKeyValue[key]);
                Cursor.SetCursor(/*defaultCursor.texture*/null, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }
}
