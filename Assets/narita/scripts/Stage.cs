using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] Vector3 GeneratePosition;//移動させる座標
    [SerializeField] GameObject[] Object;

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsInGame) return;

        if (!GameManager.Instance.IsPause)
        {
            var speed = GameManager.Instance.GetGameSpeed;
            transform.Translate(speed, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            int rand = Random.Range(0, Object.Length);//0～gameobjectの配列ー１をランダムで渡す
            GameObject go = Instantiate(Object[rand]);
            go.transform.position = GeneratePosition;
            Destroy(this.gameObject);
        }
    }
}
