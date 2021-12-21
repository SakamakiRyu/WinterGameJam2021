using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    [SerializeField] AudioClip SpeedUp;
    [SerializeField] SpriteRenderer Houseimage;
    [SerializeField] int _score = 100;//加算されるスコア
    [SerializeField] int border = 3000;
    int borderscore = 0;

    private void Start()
    {
        borderscore += border;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Present")
        {
            //スコアを増やす
            GameManager.Instance.AddScore(_score);

            if (Houseimage)
            {
                //窓の色（背景）を変える
                Houseimage.color = new Color(1f, 0.92f, 0.016f, 1f);
            }

            if (GameManager.Instance.GetCurrentScore >= borderscore)
            {
                borderscore += border;
                AudioSource.PlayClipAtPoint(SpeedUp, transform.position);
                Stage.speed = Stage.speed - 0.01f;
            }
        }
    }
}
