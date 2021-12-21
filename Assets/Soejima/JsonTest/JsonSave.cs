using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class JsonSave : MonoBehaviour
{
    [SerializeField] string userName;
    [SerializeField] int score;
    SaveDate _saveDate;
    JsonDate _JsonDate;

    void Start()
    {
        _JsonDate = JsonDate.Instance;
        _saveDate = SaveDate.Instance;
    }

    public void Save()
    {
        SaveDate.PlayerDate playerDate = new SaveDate.PlayerDate();
        playerDate._playerName = userName;
        playerDate._score = score;
        _saveDate._datelist.Add(playerDate);
        var list = _saveDate._datelist.OrderByDescending(_ => _._score);
        _saveDate._datelist = list.ToList();
        _JsonDate.Save(_saveDate);
    }
}
