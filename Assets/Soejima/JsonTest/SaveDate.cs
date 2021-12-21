using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class SaveDate : MonoBehaviour
{
    public static SaveDate Instance { get; private set; }
    public List<PlayerDate> _datelist = new List<PlayerDate>();

    void Awake()
    {
        Instance = this;
    }

    [Serializable]
    public class PlayerDate
    {
        public string _playerName;
        public int _score;
    }
}
