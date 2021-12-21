using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDelete : MonoBehaviour
{
    SaveDate _saveDate;
    JsonDate _JsonDate;

    void Start()
    {
        _JsonDate = JsonDate.Instance;
        _saveDate = SaveDate.Instance;
    }

    public void Delete()
    {
        _saveDate._datelist.Clear();
        _JsonDate.Save(_saveDate);
    }
}
