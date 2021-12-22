using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
public class Ranking : MonoBehaviour
{
    SaveDate _saveDate;

    private void Start()
    {
        _saveDate = SaveDate.Instance;
    }
}
