using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    SaveDate _saveDate;

    private void Start()
    {
        _saveDate = SaveDate.Instance;
    }
}
