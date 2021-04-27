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

    [Header("PLACEMENT")]
    [SerializeField] LayerMask _forbiddenLayers;
    [SerializeField] float _minDistanceFromLayers = 0.25f;

    [Header("GUI")]
    [SerializeField] Sprite defaultCursor;
    [SerializeField] GameObject ps;
    [SerializeField] GameObject test;
    [SerializeField] Camera mainCamera;

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
        GameObject go = Instantiate(obj);
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
            if (key == slotsKeys[slotsKeys.Count - 1] && !SearchAndReplaceTrap()) return;
            
            RaycastHit2D hit = Physics2D.CircleCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), _minDistanceFromLayers, Vector2.zero, 1f, _forbiddenLayers);
            if (hit.collider != null) return;
            //If object to spawn is available
            if (OnPlacementEvent.Invoke(slotsKeyValue[key]))
            {
                InstantiateObj(ps);
                //SPAWNA OGGETTO
                GameObject spawn = OnSpawningEvent.Invoke(slotsKeyValue[key]);
                Debug.Log(spawn);
                InstantiateObj(spawn);
            }
            else
            {
                OnResetEvent?.Invoke(slotsKeyValue[key]);
                Cursor.SetCursor(/*defaultCursor.texture*/null, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }

    private bool SearchAndReplaceTrap()
    {
        RaycastHit2D rayHit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (!rayHit2d.collider.gameObject.CompareTag("Traps") || !UIManager.UiMgrInstance.CheckAviability(slotsKeyValue[key])) return false;

        GameObject obj = rayHit2d.collider.gameObject;
        if (obj.transform.parent == null)
        {
            DestroyImmediate(obj);
        }
        else
        {
            Transform parent = obj.transform.parent;
            for (int i = 0; i < parent.childCount; i++)
            {
                DestroyImmediate(parent.GetChild(i).gameObject);
            }
            DestroyImmediate(parent.gameObject);
        }
        return true;
    }
}
