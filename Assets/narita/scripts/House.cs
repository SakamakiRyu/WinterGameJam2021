using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] SpriteRenderer Houseimage;
    [SerializeField] int _score = 100;//加算されるスコア,加算自体はGameManager内

    private void Start()
    {
        if (Houseimage)
        {
            Houseimage.color = new Color(0.3f, 0.24f, 0.24f, 1.0f);
        }
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
        }
    }
}
