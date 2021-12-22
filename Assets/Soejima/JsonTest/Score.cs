using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] Button menu;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject firstPanel;
    [SerializeField] GameObject nextPanel;
    IObservable<Unit> clickEvent => this.UpdateAsObservable();
    GameManager gameManager;
    SaveDate saveDate;
    bool isTop5In = true;
    void Start()
    {
        //menu.gameObject.SetActive(false);
        nextPanel.SetActive(false);
        gameManager = GameManager.Instance;
        saveDate = SaveDate.Instance;
        scoreText.text = gameManager.GetCurrentScore.ToString();
        //isTop5In = gameManager.GetCurrentScore > saveDate._datelist[saveDate._datelist.Count - 1]._score ? true : false;
        if (isTop5In == false)
        {
            //menu.gameObject.SetActive(true);
        }
        clickEvent.Where(_ => Input.GetMouseButtonDown(0)).First().Subscribe(_ =>
       {
           scoreText.gameObject.transform.parent.gameObject.SetActive(false);
           nextPanel.SetActive(true);
       }).AddTo(gameObject);
    }

    
}
