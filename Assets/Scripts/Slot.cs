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

    int _itemValue = 3;
    bool selected = false;


    public int ItemValue
    {

        get
        {
            return _itemValue;
        }
        set
        {
            _itemValue = value;
            itemValueTxt.text = _itemValue.ToString();

        }
    }

    void Awake()
    {
        itemValueTxt.text = _itemValue.ToString();
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
            Cursor.SetCursor(itemImage.sprite.texture,Vector2.one, CursorMode.ForceSoftware);
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

    public void OnDeselection()
    {
            selected = false;
            itemImage.rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}




