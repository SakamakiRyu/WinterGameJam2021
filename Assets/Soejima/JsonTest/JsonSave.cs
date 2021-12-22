using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;
using System;
using UniRx.Triggers;

public class JsonSave : MonoBehaviour
{
    [SerializeField] InputField textBox = default;
    [SerializeField] GameObject _sprite;
    [SerializeField] Button menu;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject nextPanel;
    [SerializeField] GameObject rankingPanel;
    [SerializeField] string userName;
    [SerializeField] Text[] rankingLIst = new Text[5];
    SaveDate _saveDate;
    JsonDate _jsonDate;
    GameManager gameManager;
    IObservable<Unit> clickEvent => this.UpdateAsObservable();
    bool isHighScore = true;

    void Start()
    {
        menu.gameObject.SetActive(false);
        nextPanel.SetActive(false);
        rankingPanel.SetActive(false);
        _jsonDate = JsonDate.Instance;
        _saveDate = SaveDate.Instance;
        gameManager = GameManager.Instance;
        //textBox = GetComponentInChildren<InputField>();
        scoreText.text = gameManager.GetCurrentScore.ToString();
        clickEvent.Where(_ => Input.GetMouseButtonDown(0) && isHighScore == true).FirstOrDefault().Subscribe(_ =>
        {
            scoreText.gameObject.transform.parent.gameObject.SetActive(false);
            nextPanel.SetActive(true);
        }).AddTo(gameObject);
        Load();
        isHighScore = gameManager.GetCurrentScore > _saveDate._datelist[_saveDate._datelist.Count - 1]._score ? true : false;
        if (isHighScore == false)
        {
            menu.gameObject.SetActive(true);
        }
        if (isHighScore == true)
        {
            _sprite.SetActive(false);
        }
    }

    public void NameSet()
    {
        userName = textBox.text;
        Save();
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
        _jsonDate.Save(_saveDate);
        RankingShow();
    }

    void RankingShow()
    {
        textBox.text = "";
        rankingPanel.SetActive(true);
        Load();
        menu.gameObject.SetActive(true);
        textBox.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
