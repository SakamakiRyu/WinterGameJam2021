using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] SpriteRenderer Houseimage;
    [SerializeField] int _score = 100;//加算されるスコア
    [SerializeField] int border = 3000;
    int borderscore = 0;

    private void Start()
    {
        borderscore += border;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Present")
        {
            //スコアを増やす
            GameManager.Instance.AddScore(_score);
            //窓の色（背景）を変える
            Houseimage.color = new Color(1f, 0.92f, 0.016f, 1f);
            if(GameManager.Instance.GetCurrentScore >= borderscore)
            {
              borderscore += border;
              Stage.speed = Stage.speed - 0.01f;
            }
        }
    }

}
