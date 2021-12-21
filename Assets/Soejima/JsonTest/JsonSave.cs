using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class JsonSave : MonoBehaviour
{
    [SerializeField] InputField textBox = default;
    [SerializeField] string userName;
    [SerializeField] int score;
    SaveDate _saveDate;
    JsonDate _JsonDate;

    void Start()
    {
        _JsonDate = JsonDate.Instance;
        _saveDate = SaveDate.Instance;
        textBox = GetComponentInChildren<InputField>();
        //textBox.gameObject.SetActive(false);
    }

    public void NameSet()
    {
        userName = textBox.text;
        Save();
    }

    public void Save()
    {
        SaveDate.PlayerDate playerDate = new SaveDate.PlayerDate();
        playerDate._playerName = userName;
        playerDate._score = GameManager.Instance.GetCurrentScore;
        _saveDate._datelist.Add(playerDate);
        var list = _saveDate._datelist.OrderByDescending(_ => _._score);
        _saveDate._datelist = list.ToList();
        if (_saveDate._datelist.Count > 5)
        {
            _saveDate._datelist.RemoveAt(5);
        }
        _JsonDate.Save(_saveDate);
        textBox.text = "";
    }

    
}
