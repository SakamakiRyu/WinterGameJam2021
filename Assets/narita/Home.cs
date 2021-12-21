using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] Image Houseimage;
    [SerializeField] int _score = 100;//加算されるスコア
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "プレゼント名")
        {
            //スコアを増やす
            GameManager.Instance.AddScore(_score);
            //窓の色（背景）を変える
            Houseimage.color = new Color(1f, 0.92f, 0.016f, 1f);
        }
    }

}
