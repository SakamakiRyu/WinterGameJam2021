using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using UniRx.Triggers;


public class JsonLoad : MonoBehaviour
{
    [SerializeField] GameObject rankingPanel;
    [SerializeField] Text[] rankingLIst = new Text[5];
    [SerializeField] Button ranking;
    [SerializeField] Button rankingClose;
    JsonDate _jsonDate;
    SaveDate _saveDate;

    void Start()
    {
        rankingPanel.SetActive(false);
        _jsonDate = JsonDate.Instance;
        _saveDate = SaveDate.Instance;
        ranking.OnClickAsObservable().Subscribe(_ =>
        {
            Load();
            rankingPanel.SetActive(true);
        });
        rankingClose.OnClickAsObservable().Subscribe(_ => { rankingPanel.SetActive(false); });
    }

    public void Load()
    {
        _saveDate = _jsonDate.Load();
        for (int i = 0; i < _saveDate._datelist.Count; i++)
        {
            if (i < 3)
            {
                rankingLIst[i].text = " 　  " + _saveDate._datelist[i]._playerName.ToString() + "  " + _saveDate._datelist[i]._score.ToString();
            }
            else
            {
                rankingLIst[i].text = (i + 1).ToString() + "位: " + _saveDate._datelist[i]._playerName.ToString() + "  " + _saveDate._datelist[i]._score.ToString();
            }

        }
    }
}
