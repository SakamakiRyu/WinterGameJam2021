﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class JsonDate : MonoBehaviour
{
    //[SerializeField] TextAsset _date;
    SaveDate _saveDate;
    public static JsonDate Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _saveDate = SaveDate.Instance;
        Debug.Log(_saveDate);
    }
    private static string GetFilePath()
    {
        return Application.dataPath +"/Resources" + "/SaveDate" + ".Json";
    }
   

    public void Save(SaveDate saveDate)
    {
        string jsonDate = JsonUtility.ToJson(saveDate);
        Debug.Log(jsonDate);
        File.WriteAllText(GetFilePath(), jsonDate);
    }

    public SaveDate Load()
    {
        string jsonDate = File.ReadAllText(GetFilePath());
        //Debug.Log(_saveDate);
        JsonUtility.FromJsonOverwrite(jsonDate, _saveDate);
        _saveDate._datelist = _saveDate._datelist.OrderByDescending(x => x._score).ToList();
        
        return _saveDate;
    }
}
