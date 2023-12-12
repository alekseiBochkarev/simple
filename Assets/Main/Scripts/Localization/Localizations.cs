using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localizations : MonoBehaviour
{
    SystemLanguage _sl;
    Text _text;

    public string _rus;
    public string _eng;

    private void Start()
    {
        _text = GetComponent<Text>();

        _sl = Application.systemLanguage;
        if (_sl == SystemLanguage.Russian)
            _text.text = _rus;
        else
            _text.text = _eng;
    }


}
