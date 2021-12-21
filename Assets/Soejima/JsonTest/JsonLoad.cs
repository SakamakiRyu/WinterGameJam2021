using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonLoad : MonoBehaviour
{
    [SerializeField] Text[] rankingLIst = new Text[5];
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
        for (int i = 0; i < _saveDate._datelist.Count; i++)
        {
            rankingLIst[i].text = (i + 1).ToString() + ": " + _saveDate._datelist[i]._score.ToString() + "\n" + _saveDate._datelist[i]._playerName.ToString();
        }
    }
}
