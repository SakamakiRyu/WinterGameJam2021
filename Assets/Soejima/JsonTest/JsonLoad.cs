using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonLoad : MonoBehaviour
{
    JsonDate _jsonDate;
    SaveDate _saveDate;

    void Start()
    {
        _jsonDate = JsonDate.Instance;
        _saveDate = SaveDate.Instance;
        Load();
    }

    public void Load()
    {
        _saveDate = _jsonDate.Load();
        foreach(var date in _saveDate._datelist)
        {
            Debug.Log(date._playerName.ToString() + date._score.ToString());
        }
    }
}
