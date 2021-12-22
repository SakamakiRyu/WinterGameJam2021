using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] Vector3 GeneratePosition;//移動させる座標
    [SerializeField] GameObject[] Object;
    public static float speed = -0.01f;
    void Update()
    {
        if (!GameManager.Instance.IsInGame) return;
        transform.Translate(speed, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            int rand = Random.Range(0, Object.Length);//0～gameobjectの配列ー１をランダムで渡す
            Debug.Log(rand);
            GameObject go = Instantiate(Object[rand]);
            go.transform.position = GeneratePosition;
            Destroy(this.gameObject);
        }
    }
}
