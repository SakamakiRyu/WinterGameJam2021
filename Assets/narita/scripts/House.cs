using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    [SerializeField] SpriteRenderer Houseimage;
    [SerializeField] int _score = 100;//加算されるスコア
    [SerializeField] int border = 3000;
    int borderscore = 0;

    /// <summary>プレゼントを持っているか</summary>
    private bool _hasPresent = false;

    private void Start()
    {
        borderscore += border;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasPresent) return;

        if (collision.gameObject.tag == "Present")
        {
            _hasPresent = true;
            //スコアを増やす
            GameManager.Instance.AddScore(_score);

            if (Houseimage)
            {
                //窓の色（背景）を変える
                Houseimage.color = new Color(1f, 0.92f, 0.016f, 1f);
            }

            if (GameManager.Instance.GetCurrentScore >= borderscore)
            {
                SoundManager.Instance.PlaySE(SoundManager.SE.Speedup);
                borderscore += border;
                Stage.speed = Stage.speed - 0.01f;
            }
        }
    }
}
