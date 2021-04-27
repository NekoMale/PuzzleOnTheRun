using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemValueTxt;
    [SerializeField] Image itemImage;
    public GameObject objContained;
    [SerializeField] int _startingItemAmount = 3;
    [Tooltip("Edit this at runtime only for debug purpose. This is equal to Starting Item Amount on start game")]
    [SerializeField] int _currentItemAmount;
    bool selected = false;


    public int ItemValue
    {

        get
        {
            return _currentItemAmount;
        }
        set
        {
            _currentItemAmount = value;
            itemValueTxt.text = _currentItemAmount.ToString();
        }
    }

    void Awake()
    {
        _currentItemAmount = _startingItemAmount;
        itemValueTxt.text = _currentItemAmount.ToString();
    }


    public void OnItemSelection()
    {
        if (CheckAvailability())
        {
            if (!selected)
            {
                itemImage.rectTransform.anchoredPosition += new Vector2(0, 3);
                selected = true;
            }
            Cursor.SetCursor(itemImage.sprite.texture,new Vector2(itemImage.sprite.texture.width*0.5f,itemImage.sprite.texture.height*0.5f), CursorMode.ForceSoftware);
        }
    }

    public bool CheckAvailability()
    {
        return ItemValue > 0;
    }

    public bool OnItemPlaced()
    {
        if (CheckAvailability())
        {
            ItemValue--;
            return true;
        }
        return false;

    }

    public GameObject GetObjToSpawn()
    {
        return objContained;
    }
    public void OnDeselection()
    {
            selected = false;
            itemImage.rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}




