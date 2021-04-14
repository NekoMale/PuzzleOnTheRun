using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITexts : MonoBehaviour {
    [SerializeField] TMP_Text _uiText = null;
    [SerializeField] string _textLabel = "";
    [SerializeField] FloatVariable _varToShow = null;

    public void UpdateText() {
        _uiText.text = $"{_textLabel} {_varToShow.Value}";
    }
}
